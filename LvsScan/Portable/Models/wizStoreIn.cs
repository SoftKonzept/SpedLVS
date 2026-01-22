using Common.Enumerations;
using Common.Models;
using Common.Views;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Runtime.Serialization;

namespace LvsScan.Portable.Models
{
    public class wizStoreIn
    {

        [DataMember]
        [JsonProperty("StoreInArt")]
        public enumStoreInArt StoreInArt { get; set; } = enumStoreInArt.NotSet;

        [DataMember]
        [JsonProperty("StoreInArtSteps")]
        public enumStoreInArt_Steps StoreInArtSteps { get; set; } = enumStoreInArt_Steps.NotSet;

        [DataMember]
        [JsonProperty("SelectedEingang")]
        public Eingaenge SelectedEingang { get; set; } = new Eingaenge();

        [DataMember]
        [JsonProperty("ArticleToCheck")]
        public Articles ArticleToCheck { get; set; } = new Articles();

        [DataMember]
        [JsonProperty("ArticlesToCheck")]
        public List<Articles> ArticlesToCheck { get; set; } = new List<Articles>();

        [DataMember]
        [JsonProperty("ArticleInEingang")]
        public List<Articles> ArticleInEingang { get; set; } = new List<Articles>();

        [DataMember]
        [JsonProperty("ErrorMessage")]
        public string ErrorMessage { get; set; } = string.Empty;

        [DataMember]
        [JsonProperty("ImageOwner")]
        public enumImageOwner ImageOwner { get; set; } = enumImageOwner.NotSet;

        [DataMember]
        [JsonProperty("Workspace")]
        public Workspaces Workspace { get; set; } = new Workspaces();

        [DataMember]
        [JsonProperty("PrintCount")]
        public int PrintCount { get; set; } = 1;

        [DataMember]
        [JsonProperty("PrinterName")]
        public string PrinterName { get; set; } = string.Empty;

        [DataMember]
        [JsonProperty("AsnArticleList")]
        public ObservableCollection<AsnArticleView> AsnArticleList { get; set; } = new ObservableCollection<AsnArticleView>();

        [DataMember]
        [JsonProperty("AsnLfsList")]
        public ObservableCollection<AsnLfsView> AsnLfsList { get; set; } = new ObservableCollection<AsnLfsView>();

        [DataMember]
        [JsonProperty("IsSearchProcess")]
        public bool IsSearchProcess { get; set; } = false;
        //[DataMember]
        //[JsonProperty("AsnListTimeStamp")]
        //public DateTime AsnListTimeStamp { get; set; } = new DateTime(1900, 1, 1);


        //[DataMember]
        //[JsonProperty("SelectedLfsView")]
        //public AsnLfsView SelectedLfsView { get; set; } = new AsnLfsView();

        [DataMember]
        [JsonProperty("SelectedArticleView")]
        public AsnArticleView SelectedArticleView { get; set; } = new AsnArticleView();

        public wizStoreLocationChanged WizStoreLocationChange { get; set; } = new wizStoreLocationChanged();
        public wizDamage WizDamage { get; set; } = new wizDamage();

        public wizStoreIn Copy()
        {
            return (wizStoreIn)this.MemberwiseClone();
        }
    }
}
