using Common.Enumerations;
using Common.Models;
using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Runtime.Serialization;


namespace Common.ApiModels
{
    [Serializable]
    public class ResponseAusgang
    {
        [DataMember]
        [JsonProperty("success")]
        public bool Success { get; set; } = false;

        [DataMember]
        [JsonProperty("ausgang")]
        public Ausgaenge Ausgang { get; set; } = new Ausgaenge();

        [DataMember]
        [JsonProperty("error")]
        public string Error { get; set; } = string.Empty;

        [DataMember]
        [JsonProperty("info")]
        public string Info { get; set; } = string.Empty;

        [DataMember]
        [JsonProperty("PrintDocuments")]
        public bool PrintDocuments { get; set; } = false;

        [DataMember]
        [JsonProperty("PrintCount")]
        public int PrintCount { get; set; } = 1;

        [DataMember]
        [JsonProperty("PrinterName")]
        public string PrinterName { get; set; } = string.Empty;

        [DataMember]
        [JsonProperty("userId")]
        public int UserId { get; set; } = 0;

        public enumStoreOutArt StoreOutArt { get; set; } = enumStoreOutArt.NotSet;
        public enumStoreOutArt_Steps StoreOutArt_Steps { get; set; } = enumStoreOutArt_Steps.NotSet;

        [DataMember]
        [JsonProperty("listAusgangArticle")]
        public List<Articles> ListAusgangArticle { get; set; } = new List<Articles>();

        [DataMember]
        [JsonProperty("listAusgaengeOpen")]
        public List<Ausgaenge> ListAusgaengeOpen { get; set; } = new List<Ausgaenge>();

        public ResponseAusgang Copy()
        {
            return (ResponseAusgang)this.MemberwiseClone();
        }

    }
}
