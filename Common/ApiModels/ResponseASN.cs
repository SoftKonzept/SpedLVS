using Common.Enumerations;
using Common.Models;
using Common.Views;
using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Runtime.Serialization;


namespace Common.ApiModels
{
    [Serializable]
    public class ResponseASN
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
        [JsonProperty("infoList")]
        public List<string> InfoList { get; set; } = new List<string>();

        [DataMember]
        [JsonProperty("AsnArticle")]
        public AsnArticleView AsnArticle { get; set; }

        [DataMember]
        [JsonProperty("userId")]
        public int UserId { get; set; } = 0;

        [DataMember]
        [JsonProperty("ListAsnArticleView")]
        public List<AsnArticleView> ListAsnArticleView { get; set; } = new List<AsnArticleView>();

        [DataMember]
        [JsonProperty("ListAsnLfsView")]
        public List<AsnLfsView> ListAsnLfsView { get; set; } = new List<AsnLfsView>();

        [DataMember]
        [JsonProperty("StoreOutArt")]
        public enumStoreInArt StoreOutArt { get; set; } = enumStoreInArt.NotSet;
        [DataMember]
        [JsonProperty("StoreInArt_Steps")]
        public enumStoreInArt_Steps StoreInArt_Steps { get; set; } = enumStoreInArt_Steps.NotSet;

        [DataMember]
        [JsonProperty("AppProcess")]
        public enumAppProcess AppProcess { get; set; } = enumAppProcess.NotSet;

        [DataMember]
        [JsonProperty("Eingang")]
        public Eingaenge Eingang { get; set; }

        [DataMember]
        [JsonProperty("ArticlesInEingang")]
        public List<Articles> ArticlesInEingang { get; set; } = new List<Articles>();

        public ResponseASN Copy()
        {
            return (ResponseASN)this.MemberwiseClone();
        }

    }
}
