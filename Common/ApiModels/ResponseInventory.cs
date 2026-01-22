using Common.Enumerations;
using Common.Models;
using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Runtime.Serialization;


namespace Common.ApiModels
{
    [Serializable]
    public class ResponseInventory
    {
        [DataMember]
        [JsonProperty("success")]
        public bool Success { get; set; } = false;

        [DataMember]
        [JsonProperty("inventory")]
        public Inventories Inventory { get; set; }


        [DataMember]
        [JsonProperty("error")]
        public string Error { get; set; } = string.Empty;

        [DataMember]
        [JsonProperty("info")]
        public string Info { get; set; } = string.Empty;

        [DataMember]
        [JsonProperty("listInventories")]
        public List<Inventories> ListInventories { get; set; } = new List<Inventories>();

        public ResponseInventory Copy()
        {
            return (ResponseInventory)this.MemberwiseClone();
        }

    }
}
