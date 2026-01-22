using Common.Enumerations;
using Common.Models;
using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Runtime.Serialization;


namespace Common.ApiModels
{
    [Serializable]
    public class ResponseEingang
    {

        [DataMember]
        [JsonProperty("success")]
        public bool Success { get; set; } = false;

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
        [JsonProperty("userId")]
        public int UserId { get; set; } = 0;

        [DataMember]
        [JsonProperty("PrintCount")]
        public int PrintCount { get; set; } = 1;

        [DataMember]
        [JsonProperty("PrinterName")]
        public string PrinterName { get; set; } = string.Empty;

        public enumStoreInArt StoreInArt { get; set; } = enumStoreInArt.NotSet;
        public enumStoreInArt_Steps StoreInArt_Steps { get; set; } = enumStoreInArt_Steps.NotSet;

        [DataMember]
        [JsonProperty("Eingang")]
        public Eingaenge Eingang { get; set; } = new Eingaenge();


        [DataMember]
        [JsonProperty("ListEingangArticle")]
        public List<Articles> ListEingangArticle { get; set; } = new List<Articles>();

        [DataMember]
        [JsonProperty("ListEingaengeOpen")]
        public List<Eingaenge> ListEingaengeOpen { get; set; } = new List<Eingaenge>();

        public ResponseEingang Copy()
        {
            return (ResponseEingang)this.MemberwiseClone();
        }

    }
}
