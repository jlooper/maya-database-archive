using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace MayanGlyphsApi.Models
{
    public class Artifact
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("_id")]
        [JsonIgnore]
        public ObjectId Id { get; set; }

        [BsonElement("id")]
        public int RecId { get; set; }

        [BsonElement("objclass")]
        public string Class { get; set; }

        [BsonElement("objmaterial")]
        public string Material { get; set; }

        [BsonElement("objtechnique")]
        public string Technique { get; set; }

        [BsonElement("objregionorigin")]
        public string RegionOrigin { get; set; }

        [BsonElement("objregiondestination")]
        public string RegionDestination { get; set; }

        [BsonElement("blosort")]
        public string BlockSort { get; set; }

        [BsonElement("objabbr1")]
        public string Jabbr1 { get; set; }

        [BsonElement("objsiteorigin")]
        public string SiteOrigin { get; set; }

        [BsonElement("objsitecodeorigin")]
        public string SiteCodeOrigin { get; set; }

        [BsonElement("objsitecodedestination")]
        public string SiteCodeDestination { get; set; }

        [BsonElement("objimage1")]
        public string OImage1 { get; set; }

        [BsonElement("objimage2")]
        public string OImage2 { get; set; }

        [BsonElement("objimage3")]
        public string OImage3 { get; set; }

        [BsonElement("objimage4")]
        public string OImage4 { get; set; }

        [BsonElement("objMayaartist")]
        public string MayanArtist { get; set; }

        [BsonElement("objcal")]
        public string Cal { get; set; }

        [BsonElement("objlc")]
        public string LC { get; set; }

        [BsonElement("obj260")]
        public string Cycle260 { get; set; }

        [BsonElement("obj365")]
        public string Cycl365 { get; set; }
        
        [BsonElement("objHellmuthNum")]
        public string HellmuthNum { get; set; }

        [BsonElement("objKerrNum")]
        public string KerrNum { get; set; }

        [BsonElement("objMSNum")]
        public string MSNum { get; set; }

        //[BsonElement("blcoordinate")]
        //public string Coordinate { get; set; }

        [BsonElement("blsurface")]
        public string Surface { get; set; }

        [BsonExtraElements]
        public IDictionary<string, object> Extras { get; set; }

        [BsonElement("blimage1")]
        public string BImage1 { get; set; }

        [BsonElement("blimage2")]
        public string BImage2 { get; set; }

        [BsonElement("blimagenotes")]
        public string ImageNotes { get; set; }

    }
}
