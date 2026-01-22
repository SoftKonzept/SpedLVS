using Common.Enumerations;
using Common.Models;
using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Runtime.Serialization;
namespace Common.ApiModels
{
    [Serializable]
    public class ResponseImage
    {
        [DataMember]
        [JsonProperty("success")]
        public bool Success { get; set; } = false;

        [DataMember]
        [JsonProperty("Image")]
        public Images Image { get; set; } = new Images();

        [DataMember]
        [JsonProperty("Info")]
        public string Info { get; set; } = string.Empty;

        [DataMember]
        [JsonProperty("Error")]
        public string Error { get; set; } = string.Empty;

        [DataMember]
        [JsonProperty("userId")]
        public int UserId { get; set; } = 0;

        //public enumStoreOutArt StoreOutArt { get; set; } = enumStoreOutArt.NotSet;
        //public enumStoreOutArt StoreOutArt { get; set; } = enumStoreOutArt.NotSet;
        //public enumStoreOutArt_Steps StoreOutArt_Steps { get; set; } = enumStoreOutArt_Steps.NotSet;

        //[DataMember]
        //[JsonProperty("ListDamages")]
        //public List<Damages> ListDamages { get; set; } = new List<Damages>();

        //[DataMember]
        //[JsonProperty("ListArticleDamages")]
        //public List<Damages> ListArticleDamages { get; set; } = new List<Damages>();

        //[DataMember]
        //[JsonProperty("ListDamagesArticleAssignments")]
        //public List<DamageArticleAssignmentView> ListDamagesArticleAssignments { get; set; } = new List<DamageArticleAssignmentView>();

        public ResponseImage Copy()
        {
            return (ResponseImage)this.MemberwiseClone();
        }

    }
}
