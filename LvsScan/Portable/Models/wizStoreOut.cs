using Common.Enumerations;
using Common.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace LvsScan.Portable.Models
{
    public class wizStoreOut
    {

        [DataMember]
        [JsonProperty("StoreOutArt")]
        public enumStoreOutArt StoreOutArt { get; set; } = enumStoreOutArt.NotSet;

        [DataMember]
        [JsonProperty("SelectedAusgang")]
        public Ausgaenge SelectedAusgang { get; set; } = new Ausgaenge();

        [DataMember]
        [JsonProperty("ArticleToCheck")]
        public Articles ArticleToCheck { get; set; } = new Articles();

        [DataMember]
        [JsonProperty("ListArticleInAusgang")]
        public List<Articles> ListArticleInAusgang { get; set; } = new List<Articles>();

        [DataMember]
        [JsonProperty("CallsList")]
        public List<Calls> CallsList { get; set; } = new List<Calls>();

        [DataMember]
        [JsonProperty("ErrorMessage")]
        public string ErrorMessage { get; set; } = string.Empty;

        [DataMember]
        [JsonProperty("ImageOwner")]
        public enumImageOwner ImageOwner { get; set; } = enumImageOwner.NotSet;


        [DataMember]
        [JsonProperty("workspace")]
        public Workspaces Workspace { get; set; } = new Workspaces();

        [DataMember]
        [JsonProperty("PrintCount")]
        public int PrintCount { get; set; } = 1;

        [DataMember]
        [JsonProperty("PrinterName")]
        public string PrinterName { get; set; } = string.Empty;

        [DataMember]
        [JsonProperty("WorkingInProcess")]
        public bool WorkingInProcess { get; set; } = false;

        public wizDamage WizDamage { get; set; }

        public wizStoreOut Copy()
        {
            return (wizStoreOut)this.MemberwiseClone();
        }
    }
}
