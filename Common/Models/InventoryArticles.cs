using Common.Enumerations;
using Common.Helper;
using Common.Models;
using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Common.Views;

namespace Common.Models
{
    [Serializable]
    [DataContract]
    public class InventoryArticles
    {
        [DataMember]
        [JsonProperty("id")]
        public int Id { get; set; } = 0;

        [DataMember]
        [JsonProperty("inventoryId")]
        public int InventoryId { get; set; } = 0;

        [DataMember]
        [JsonProperty("description")]
        public string Description { get; set; } = string.Empty;


        [DataMember]
        [JsonProperty("artikelId")]
        public int ArtikelId { get; set; } = 0;

        [DataMember]
        [JsonProperty("lvsNummer")]
        public int LvsNummer { get; set; } = 0;

        [DataMember]
        [JsonProperty("status")]
        public enumInventoryArticleStatus Status { get; set; } = enumInventoryArticleStatus.NotSet;

        [DataMember]
        [JsonProperty("text")]
        public string Text { get; set; } = string.Empty;

        [DataMember]
        [JsonProperty("created")]
        public DateTime Created { get; set; } = new DateTime(1900, 1, 1);

        [DataMember]
        [JsonProperty("scanned")]
        public DateTime Scanned { get; set; } = new DateTime(1900, 1, 1);

        [DataMember]
        [JsonProperty("scannedUserId")]
        public int ScannedUserId { get; set; } = 0;


        [DataMember]
        [JsonProperty("artikel")]
        public Articles Artikel { get; set; } = new Articles();

        //[DataMember]
        //[JsonProperty("view")]
        public InventoryArticleView View { get; set; } = new InventoryArticleView();

        public InventoryArticles Copy()
        {
            return (InventoryArticles)this.MemberwiseClone();
        }




    }
}
