using Common.Enumerations;
using Common.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;


namespace Common.ApiModels
{
    [Serializable]
    public class ResponseAddress
    {
        [DataMember]
        [JsonProperty("success")]
        public bool Success { get; set; } = false;

        [DataMember]
        [JsonProperty("address")]
        public Addresses Address { get; set; }


        [DataMember]
        [JsonProperty("error")]
        public string Error { get; set; } = string.Empty;

        [DataMember]
        [JsonProperty("info")]
        public string Info { get; set; } = string.Empty;

        [DataMember]
        [JsonProperty("listAddresses")]
        public List<Addresses> ListAddresses { get; set; } = new List<Addresses>();

        [DataMember]
        [JsonProperty("AppProcess")]
        public enumAppProcess AppProcess { get; set; } = enumAppProcess.NotSet;

        [DataMember]
        [JsonProperty("WorkspaceId")]
        public int WorkspaceId { get; set; } = 0;

        [DataMember]
        [JsonProperty("UserId")]
        public int UserId { get; set; } = 0;

        [DataMember]
        [JsonProperty("AddressReferenzes")]
        public AddressReferences AdrReferenz { get; set; } = new AddressReferences();

        public ResponseAddress Copy()
        {
            return (ResponseAddress)this.MemberwiseClone();
        }

    }
}
