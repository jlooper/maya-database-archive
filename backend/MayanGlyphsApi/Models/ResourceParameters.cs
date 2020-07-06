namespace MayanGlyphsApi.Models
{
    public class ResourceQueryParameters
    {
        const int maxLimit = 30;        
        public virtual int Page { get; set; } = 1;

        private int _limit = 10;
        public virtual int Limit
        {
            get => _limit;
            set => _limit = (value > maxLimit) ? maxLimit : value;
        }

        public virtual string OrderBy { get; set; }
        public virtual string Fields { get; set; }
    }

    public class ArtifactResourceQueryParameters : ResourceQueryParameters
    {
        public string Class { get; set; }
        public string Material { get; set; }
    }
}
