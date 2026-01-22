using Common.Enumerations;
using Common.Models;
using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Runtime.Serialization;


namespace Common.ApiModels
{
    [Serializable]
    public class ResponseLogin
    {
        [DataMember]
        [JsonProperty("accessToken")]
        public string AccessToken { get; set; } = string.Empty;


        [DataMember]
        [JsonProperty("accessGranted")]
        public bool AccessGranted { get; set; } = false;

        [DataMember]
        [JsonProperty("error")]
        public string Error { get; set; } = string.Empty;

        [DataMember]
        [JsonProperty("loggedUser")]
        public Users LoggedUser { get; set; }

        public ResponseLogin Copy()
        {
            return (ResponseLogin)this.MemberwiseClone();
        }

    }
}
