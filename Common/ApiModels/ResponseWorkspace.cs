using Common.Enumerations;
using Common.Models;
using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Runtime.Serialization;


namespace Common.ApiModels
{
    [Serializable]
    public class ResponseWorkspace
    {
        [DataMember]
        [JsonProperty("success")]
        public bool Success { get; set; } = false;

        [DataMember]
        [JsonProperty("workspace")]
        public Workspaces Workspace { get; set; }


        [DataMember]
        [JsonProperty("error")]
        public string Error { get; set; } = string.Empty;

        [DataMember]
        [JsonProperty("info")]
        public string Info { get; set; } = string.Empty;

        [DataMember]
        [JsonProperty("listWorkspaces")]
        public List<Workspaces> ListWorkspaces { get; set; } = new List<Workspaces>();


        public ResponseWorkspace Copy()
        {
            return (ResponseWorkspace)this.MemberwiseClone();
        }

    }
}
