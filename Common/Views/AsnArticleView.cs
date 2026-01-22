using Common.Enumerations;
using Common.Helper;
using Common.Models;
using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Drawing;

namespace Common.Views
{
    [Serializable]
    [DataContract]
    public class AsnArticleView
    {
        [DataMember]
        [JsonProperty("LfdNr")]
        public int LfdNr { get; set; } = 0;

        [DataMember]
        [JsonProperty("AsnId")]
        public int AsnId { get; set; } = 0;

        [DataMember]
        [JsonProperty("netto")]
        public decimal Netto { get; set; } = 0;

        [DataMember]
        [JsonProperty("brutto")]
        public decimal Brutto { get; set; } = 0;

        [DataMember]
        [JsonProperty("dicke")]
        public decimal Dicke { get; set; } = 0;

        [DataMember]
        [JsonProperty("breite")]
        public decimal Breite { get; set; } = 0;

        [DataMember]
        [JsonProperty("laenge")]
        public decimal Laenge { get; set; } = 0;

        [DataMember]
        [JsonProperty("Werksnummer")]
        public string Werksnummer { get; set; } = string.Empty;

        [DataMember]
        [JsonProperty("Produktionsnummer")]
        public string Produktionsnummer { get; set; } = string.Empty;

        [DataMember]
        [JsonProperty("charge")]
        public string Charge { get; set; } = string.Empty;

        [DataMember]
        [JsonProperty("bestellnummer")]
        public string Bestellnummer { get; set; } = string.Empty;

        [DataMember]
        [JsonProperty("exMaterialnummer")]
        public string exMaterialnummer { get; set; } = string.Empty;

        [DataMember]
        [JsonProperty("position")]
        public string Position { get; set; } = string.Empty;

        [DataMember]
        [JsonProperty("Gut")]
        public string Gut { get; set; } = string.Empty;

        [DataMember]
        [JsonProperty("LfsNr")]
        public string LfsNr { get; set; } = string.Empty;

        [DataMember]
        [JsonProperty("VehicleNo")]
        public string VehicleNo { get; set; } = string.Empty;


        [DataMember]
        [JsonProperty("WorkspaceId")]
        public int WorkspaceId { get; set; } = 0;

        [DataMember]
        [JsonProperty("Workspace")]
        public Workspaces Workspace { get; set; } = new Workspaces();


        [DataMember]
        [JsonProperty("IsSearchResult")]
        public bool IsSearchResult { get; set; } = false;


        [JsonIgnore]
        public Color ViewBackgroundcolor
        {
            get
            {
                Color tmpBC = ValueToColorConverter.ViewBackgroundColorWorkspace_IdConvert(WorkspaceId);
                return tmpBC;
            }
        }


        public AsnArticleView Copy()
        {
            return (AsnArticleView)this.MemberwiseClone();
        }
    }
}
