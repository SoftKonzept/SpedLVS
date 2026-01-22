using Common.Enumerations;
using Common.Helper;
using Common.Models;
using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Common.Models
{
    [Serializable]
    [DataContract]
    public class Units
    {
        [DataMember]
        [JsonProperty("Id")]
        public int Id { get; set; }

        [DataMember]
        [JsonProperty("Bezeichnung")]
        public string Bezeichnung { get; set; }


        public Units Copy()
        {
            return (Units)this.MemberwiseClone();
        }
    }
}
