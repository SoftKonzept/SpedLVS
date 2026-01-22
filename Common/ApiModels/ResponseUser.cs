using Common.Enumerations;
using Common.Models;
using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Common.ApiModels
{
    [Serializable]
    public class ResponseUser
    {
        [DataMember]
        [JsonProperty("error")]
        public string Error { get; set; } = string.Empty;

        [DataMember]
        [JsonProperty("user")]
        public Users User { get; set; }

        [DataMember]
        [JsonProperty("isSuccess")]
        public bool IsSuccess { get; set; }
    }
}
