using Common.Enumerations;
using Common.Models;
using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Runtime.Serialization;


namespace Common.ApiModels
{
    [Serializable]
    public class ResponseCall
    {
        [DataMember]
        [JsonProperty("success")]
        public bool Success { get; set; } = false;

        [DataMember]
        [JsonProperty("call")]
        public Calls Call { get; set; }


        [DataMember]
        [JsonProperty("error")]
        public string Error { get; set; } = string.Empty;

        [DataMember]
        [JsonProperty("info")]
        public string Info { get; set; } = string.Empty;

        [DataMember]
        [JsonProperty("userId")]
        public int UserId { get; set; } = 0;

        [DataMember]
        [JsonProperty("listCallOpen")]
        public List<Calls> ListCallOpen { get; set; } = new List<Calls>();

        [DataMember]
        [JsonProperty("ListCallForStoreOut")]
        public List<int> ListCallForStoreOut { get; set; } = new List<int>();

        //[DataMember]
        //[JsonProperty("CreatedAusgang")]
        //public Ausgaenge CreatedAusgang { get; set; } = new Ausgaenge();


        [DataMember]
        [JsonProperty("StoreOutArt")]
        public enumStoreOutArt StoreOutArt { get; set; } = enumStoreOutArt.NotSet;



        [DataMember]
        [JsonProperty("StoreOutArt_Steps")]
        public enumStoreOutArt_Steps StoreOutArt_Steps { get; set; } = enumStoreOutArt_Steps.NotSet;

        public enumAppProcess AppProcess { get; set; } = enumAppProcess.NotSet;

        [DataMember]
        [JsonProperty("CreatedAusgang")]
        public Ausgaenge CreatedAusgang { get; set; } = new Ausgaenge();




        public ResponseCall Copy()
        {
            return (ResponseCall)this.MemberwiseClone();
        }

    }
}
