using Common.Enumerations;
using Common.Models;
using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Common.ApiModels
{
    [Serializable]
    public class UserLogin
    {
        [DataMember]
        [JsonProperty("username")]
        public string Username { get; set; } = string.Empty;

        [DataMember]
        [JsonProperty("password")]
        public string Password { get; set; } = string.Empty;
    }
}
