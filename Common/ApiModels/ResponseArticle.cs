using Common.Enumerations;
using Common.Models;
using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Runtime.Serialization;


namespace Common.ApiModels
{
    [Serializable]
    public class ResponseArticle
    {
        [DataMember]
        [JsonProperty("success")]
        public bool Success { get; set; } = false;

        [DataMember]
        [JsonProperty("UserId")]
        public int UserId { get; set; } = 0;

        [DataMember]
        [JsonProperty("error")]
        public string Error { get; set; } = string.Empty;

        [DataMember]
        [JsonProperty("info")]
        public string Info { get; set; } = string.Empty;


        [DataMember]
        [JsonProperty("AppProcess")]
        public enumAppProcess AppProcess { get; set; } = enumAppProcess.NotSet;

        [DataMember]
        [JsonProperty("ArticleEdit_Step")]
        public enumArticleEdit_Steps ArticleEdit_Step { get; set; } = enumArticleEdit_Steps.NotSet;

        [DataMember]
        [JsonProperty("article")]
        public Articles Article { get; set; }

        public ResponseArticle Copy()
        {
            return (ResponseArticle)this.MemberwiseClone();
        }

    }
}
