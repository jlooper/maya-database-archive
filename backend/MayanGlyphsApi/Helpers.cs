using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Reflection;
using X.PagedList;

namespace MayanGlyphsApi
{
    public static class HelperExtensions
    {
        public static bool HasProperties(this Type source, string fields)
        {
            if (string.IsNullOrWhiteSpace(fields)) return true;

            // fields are expected to be separated by ","
            var splitFields = fields.Split(',');

            foreach (var field in splitFields)
            {
                var propertyName = field.Trim();

                var propertyInfo = source
                    .GetProperty(propertyName,
                    BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

                if (propertyInfo == null) return false;
            }
            return true;
        }

        public static IEnumerable<ExpandoObject> ChooseFields<TSource>(this IPagedList<TSource> source, string fields)
        {
            return (source as IEnumerable<TSource>).ChooseFields(fields);
        }

        public static IEnumerable<ExpandoObject> ChooseFields<TSource>(this IEnumerable<TSource> source, string fields)
        {
            if (source == null) throw new ArgumentNullException(nameof(source));

            var expandos = new List<ExpandoObject>();

            // propertyinfo cache            
            var chosenProperties = new List<PropertyInfo>();

            if (string.IsNullOrWhiteSpace(fields))
            {
                var props = typeof(TSource)
                        .GetProperties(BindingFlags.IgnoreCase
                        | BindingFlags.Public | BindingFlags.Instance);

                chosenProperties.AddRange(props);
            }
            else
            {
                var splitFields = fields.Split(',');

                splitFields.ForEach(field =>
                {
                    var propertyName = field.Trim();

                    var propertyInfo = typeof(TSource)
                        .GetProperty(propertyName, BindingFlags.IgnoreCase |
                        BindingFlags.Public | BindingFlags.Instance);

                    if (propertyInfo != null) chosenProperties.Add(propertyInfo);
                });
            }

            source.ForEach(item =>
            {
                var shapedObject = new ExpandoObject();

                foreach (var propertyInfo in chosenProperties)
                {
                    var ignoreProperty = propertyInfo.GetCustomAttributes()
                        .Any(a => a.GetType().Name.Contains("JsonIgnore", StringComparison.InvariantCulture));

                    if (ignoreProperty) continue;

                    var propertyValue = propertyInfo.GetValue(item);

                    ((IDictionary<string, object>)shapedObject)
                        .Add(propertyInfo.Name, propertyValue);
                }

                expandos.Add(shapedObject);
            });

            return expandos;
        }

        public static ExpandoObject ChooseFields<TSource>(this TSource source, string fields) where TSource : class
        {
            if (source == null) throw new ArgumentNullException(nameof(source));

            var shapedObject = new ExpandoObject();

            if (string.IsNullOrWhiteSpace(fields))
            {
                var props = typeof(TSource)
                        .GetProperties(BindingFlags.IgnoreCase |
                        BindingFlags.Public | BindingFlags.Instance);

                props.ForEach(p =>
                {
                    var propertyValue = p.GetValue(source);

                    ((IDictionary<string, object>)shapedObject)
                        .Add(p.Name, propertyValue);
                });

                return shapedObject;
            }

            // field separated by ","
            var splitFields = fields.Split(',');

            foreach (var field in splitFields)
            {
                var propertyName = field.Trim();

                var propertyInfo = typeof(TSource)
                    .GetProperty(propertyName,
                    BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance);

                if (propertyInfo == null) continue;

                var ignore = propertyInfo.GetCustomAttributes()
                    .Any(a => a.GetType().Name.Contains("JsonIgnore", StringComparison.InvariantCulture));

                if (ignore) continue;

                var propertyValue = propertyInfo.GetValue(source);
                
                ((IDictionary<string, object>)shapedObject)
                    .Add(propertyInfo.Name, propertyValue);
            }

            return shapedObject;
        }

        public static IQueryable<T> ApplySort<T>(this IQueryable<T> source, string orderBy)
        {
            if (source == null)    throw new ArgumentNullException(nameof(source));
            
            if (string.IsNullOrWhiteSpace(orderBy))    return source;            

            var orderByString = string.Empty;

            var orderBySplit = orderBy.Split(',');
            
            foreach (var orderClause in orderBySplit)
            {                
                var trimmedOrder = orderClause.Trim();
             
                var isDesc = trimmedOrder.EndsWith(" desc");

                var spaceIndeex = trimmedOrder.IndexOf(" ");
                var propertyName = spaceIndeex == -1 ?
                    trimmedOrder : trimmedOrder.Remove(spaceIndeex);

                orderByString = orderByString +
                    (string.IsNullOrWhiteSpace(orderByString) ? string.Empty : ", ")
                    + propertyName
                    + (isDesc ? " descending" : " ascending");
            }

            return source.OrderBy(orderByString);
        }

    }
}