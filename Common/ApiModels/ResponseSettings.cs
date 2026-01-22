using Common.Enumerations;
using Common.Models;
using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Common.ApiModels
{
    [Serializable]
    public class ResponseSettings
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
        [JsonProperty("ListPrinters")]
        public List<string> ListPrinters { get; set; } = new List<string>();


        public ResponseSettings Copy()
        {
            return (ResponseSettings)this.MemberwiseClone();
        }

    }
}
