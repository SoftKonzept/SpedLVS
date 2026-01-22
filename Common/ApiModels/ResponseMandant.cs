using Common.Enumerations;
using Common.Models;
using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Runtime.Serialization;


namespace Common.ApiModels
{
    [Serializable]
    public class ResponseMandant
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


        //[DataMember]
        //[JsonProperty("AppProcess")]
        //public enumAppProcess AppProcess { get; set; } = enumAppProcess.NotSet;


        [DataMember]
        [JsonProperty("mandant")]
        public Mandanten Mandant { get; set; }

        [DataMember]
        [JsonProperty("mandantlist")]
        public List<Mandanten> Mandantlist { get; set; } = new List<Mandanten>();



        public ResponseMandant Copy()
        {
            return (ResponseMandant)this.MemberwiseClone();
        }

    }
}
