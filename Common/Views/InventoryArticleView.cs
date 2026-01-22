using Common.Enumerations;
using Common.Helper;
using Common.Models;
using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Common.Views
{
    [Serializable]
    [DataContract]
    public class InventoryArticleView
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
        [JsonProperty("lvsNo")]
        public int LvsNr { get; set; } = 0;

        [DataMember]
        [JsonProperty("lvsNo")]
        public string Produktionsnummer { get; set; } = String.Empty;

        [DataMember]
        [JsonProperty("werksnummer")]
        public string Werksnummer { get; set; } = String.Empty;

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
        [JsonProperty("user")]
        public string User { get; set; } = String.Empty;
    }
}
