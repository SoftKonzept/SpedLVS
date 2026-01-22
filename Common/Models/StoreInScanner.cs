using Common.Enumerations;
using Common.Helper;
using Common.Models;
using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Numerics;

namespace Common.Models
{
    public class StoreInScanner
    {
        [DataMember]
        [JsonProperty("id")]
        public int Id { get; set; }

        [DataMember]
        [JsonProperty("ProcessId")]
        public BigInteger ProcessId { get; set; }

        [DataMember]
        [JsonProperty("DataField")]
        public string DataField { get; set; }

        [DataMember]
        [JsonProperty("FieldValue")]
        public string FieldValue { get; set; }

        [DataMember]
        [JsonProperty("UserId")]
        public int UserId { get; set; }



        [DataMember]
        [JsonProperty("Created")]
        public DateTime Created { get; set; }



        public StoreInScanner Copy()
        {
            return (StoreInScanner)this.MemberwiseClone();
        }
    }
}
