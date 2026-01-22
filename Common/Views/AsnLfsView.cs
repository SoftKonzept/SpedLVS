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
    public class AsnLfsView
    {
        [DataMember]
        [JsonProperty("LfdNr")]
        public int LfdNr { get; set; } = 0;

        [DataMember]
        [JsonProperty("AsnId")]
        public int AsnId { get; set; } = 0;

        [DataMember]
        [JsonProperty("AsnDatum")]
        public DateTime AsnDatum { get; set; }

        [DataMember]
        [JsonProperty("VsDatum")]
        public DateTime VsDatum { get; set; }

        [DataMember]
        [JsonProperty("LfsNr")]
        public string LfsNr { get; set; } = string.Empty;

        [DataMember]
        [JsonProperty("TransportNr")]
        public string TransportNr { get; set; } = string.Empty;

        [DataMember]
        [JsonProperty("Auftraggeber")]
        public int Auftraggeber { get; set; } = 0;

        //[DataMember]
        //[JsonProperty("AuftraggeberString")]
        //public string AuftraggeberString { get; set; } = string.Empty;

        [DataMember]
        [JsonProperty("AuftraggeberAdr")]
        public Addresses AuftraggeberAdr { get; set; }

        [DataMember]
        [JsonProperty("Empfaenger")]
        public int Empfaenger { get; set; } = 0;

        //[DataMember]
        //[JsonProperty("EmpfaengerString")]
        //public string EmpfaengerString { get; set; } = string.Empty;

        [DataMember]
        [JsonProperty("EmpfaengerAdr")]
        public Addresses EmpfaengerAdr { get; set; }

        [DataMember]
        [JsonProperty("Transportmittel")]
        public string Transportmittel { get; set; } = string.Empty;

        [DataMember]
        [JsonProperty("Lieferantennummer")]
        public string Lieferantennummer { get; set; } = string.Empty;

        [DataMember]
        [JsonProperty("ExTransportRef")]
        public string ExTransportRef { get; set; } = string.Empty;

        [DataMember]
        [JsonProperty("ASNRef")]
        public string ASNRef { get; set; } = string.Empty;


        [DataMember]
        [JsonProperty("WorkspaceId")]
        public int WorkspaceId { get; set; } = 0;

        [DataMember]
        [JsonProperty("Workspace")]
        public Workspaces Workspace { get; set; } = new Workspaces();


        [JsonIgnore]
        public Color ViewBackgroundcolor
        {
            get
            {
                Color tmpBC = ValueToColorConverter.ViewBackgroundColorWorkspace_IdConvert(WorkspaceId);
                return tmpBC;
            }
        }



        public AsnLfsView Copy()
        {
            return (AsnLfsView)this.MemberwiseClone();
        }

    }
}
