using Common.Enumerations;
using Common.Models;
using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Runtime.Serialization;


namespace Common.ApiModels
{
    [Serializable]
    public class ResponseInventoryArticle
    {
        [DataMember]
        [JsonProperty("success")]
        public bool Success { get; set; } = false;

        [DataMember]
        [JsonProperty("inventoryArticle")]
        public InventoryArticles InventoryArticle { get; set; }

        [DataMember]
        [JsonProperty("error")]
        public string Error { get; set; } = string.Empty;

        [DataMember]
        [JsonProperty("info")]
        public string Info { get; set; } = string.Empty;

        [DataMember]
        [JsonProperty("listInventoryArticle")]
        public List<InventoryArticles> ListInventoryArticle { get; set; } = new List<InventoryArticles>();


        public ResponseInventoryArticle Copy()
        {
            return (ResponseInventoryArticle)this.MemberwiseClone();
        }

    }
}
