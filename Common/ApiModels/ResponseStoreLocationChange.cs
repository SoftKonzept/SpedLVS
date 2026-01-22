using Common.Enumerations;
using Common.Models;
using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Runtime.Serialization;


namespace Common.ApiModels
{
    [Serializable]
    public class ResponseStoreLocationChange
    {
        [DataMember]
        [JsonProperty("successStoreLocationChange")]
        public bool SuccessStoreLocationChange { get; set; } = false;


        [DataMember]
        [JsonProperty("UserId")]
        public int UserId { get; set; } = 0;

        [DataMember]
        [JsonProperty("error")]
        public string Error { get; set; } = string.Empty;


        [DataMember]
        [JsonProperty("articleId")]
        public int ArticleId { get; set; } = 0;

        [DataMember]
        [JsonProperty("info")]
        public string Info { get; set; } = string.Empty;

        [DataMember]
        [JsonProperty("Article")]
        public Articles Article { get; set; }

        //[DataMember]
        //[JsonProperty("ArtStoreLocation")]
        //public ArticleStoreLocation ArtStoreLocation { get; set; }

        public ResponseStoreLocationChange Copy()
        {
            return (ResponseStoreLocationChange)this.MemberwiseClone();
        }

    }
}
