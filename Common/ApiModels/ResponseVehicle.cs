using Common.Enumerations;
using Common.Models;
using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Runtime.Serialization;


namespace Common.ApiModels
{
    [Serializable]
    public class ResponseVehicle
    {
        [DataMember]
        [JsonProperty("success")]
        public bool Success { get; set; } = false;

        [DataMember]
        [JsonProperty("Vehicle")]
        public Vehicles Vehicle { get; set; }


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
        [JsonProperty("ListVehicles")]
        public List<Vehicles> ListVehicles { get; set; } = new List<Vehicles>();

        public ResponseVehicle Copy()
        {
            return (ResponseVehicle)this.MemberwiseClone();
        }

    }
}
