using System.Collections.Generic;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;

namespace MayanGlyphsApi.Models
{
      public class Artifact
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        [BsonElement("_id")]
        [JsonIgnore]
        [System.Text.Json.Serialization.JsonIgnore]
        public string Id { get; set; }

        [BsonElement("objclass")]
        [JsonProperty("objclass")]
        public string Class { get; set; }

        [BsonElement("objmaterial")]
        [JsonProperty("objmaterial")]
        public string Material { get; set; }

        [BsonElement("objtechnique")]
        public string Technique { get; set; }

        [BsonElement("objregionorigin")]
        public string RegionOrigin { get; set; }

        [BsonElement("objregiondestination")]
        public string RegionDestination { get; set; }

        [BsonElement("blosort")]
        [JsonProperty("blosort")]
        public string BlockSort { get; set; }

        [BsonElement("objabbr1")]
        public string Jabbr1 { get; set; }

        [BsonElement("objsiteorigin")]
        public string SiteOrigin { get; set; }

        [BsonElement("objsitecodeorigin")]
        public string SiteCodeOrigin { get; set; }

        [BsonElement("objsitecodedestination")]
        public string SiteCodeDestination { get; set; }

        [BsonElement("obj_almanpgs")]
        public string Almanpgs { get; set; }

        [BsonElement("objstructure_almanac")]
        public string StructureAlmanac { get; set; }

        [BsonElement("objorientation_frame")]
        public string OrientationFrame { get; set; }

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

        [BsonElement("blcoordinate")]
        [JsonProperty("blcoordinate")]
        public string Coordinate { get; set; }

        [BsonElement("blsurface")]
        [JsonProperty("blsurface")]
        public string Surface { get; set; }

        [BsonElement("blimage1")]
        public string BImage1 { get; set; }

        [BsonElement("blimage2")]
        public string BImage2 { get; set; }

        [BsonElement("blimagenotes")]
        public string ImageNotes { get; set; }

        [BsonElement("blocklogosyllabic")]
        public string Blocklogosyllabic { get; set; }

        [BsonElement("Hyphenated")]
        public string Hyphenated { get; set; }

        [BsonElement("bleng")]
        public string BlEng { get; set; }

        [BsonElement("blspan")]
        public string BlSpan { get; set; }

        [BsonElement("blgraphcodes")]
        public string GraphCodes { get; set; }

        [BsonElement("blepigraphicreading")]
        public string EpigraphicReading { get; set; }

        [BsonElement("Evidence")]
        public string Evidence { get; set; }

        [BsonElement("substitution")]
        public string Substitution { get; set; }

        [BsonElement("blsem")]
        public string Sem { get; set; }

        [BsonElement("blevlc")]
        public string Evlc { get; set; }

        [BsonElement("blevcal")]
        public string Evcal { get; set; }

        [BsonElement("blev260")]
        public string Ev260 { get; set; }
        
        [BsonElement("blev365")]
        public string Ev365 { get; set; }

        [BsonElement("locabbr")]
        public string LocAbbr { get; set; }

        [BsonElement("locaccessionNum")]
        public string LocAccessionNum { get; set; }

        [BsonElement("locmu")]
        public string Locmu { get; set; }
        
        [BsonElement("loccitystate")]
        public string Loccitystate { get; set; }

        [BsonElement("blblocknotes")]
        public string BlockNotes { get; set; }

        [BsonElement("loccountry")]
        public string LocCountry { get; set; }

        [BsonElement("clauseID")]
        public string ClauseID { get; set; }

        [BsonElement("Clause_Engl")]
        public string ClauseEngl { get; set; }

        [BsonElement("Clause_Span")]
        public string ClauseSpan { get; set; }

        [BsonExtraElements]
        public IDictionary<string, object> Extras { get; set; }
    }
}