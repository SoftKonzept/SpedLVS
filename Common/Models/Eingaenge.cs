using Common.Enumerations;
using Common.Helper;
using Common.Models;
using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Drawing;

namespace Common.Models
{
    [Serializable]
    [DataContract]
    public class Eingaenge
    {
        [DataMember]
        [JsonProperty("id")]
        public int Id { get; set; } = 0;

        [DataMember]
        [JsonProperty("LEingangID")]
        public int LEingangID { get; set; } = 0;

        [DataMember]
        [JsonProperty("eingangsdatum")]
        public DateTime Eingangsdatum { get; set; } = new DateTime(1900, 1, 1);

        [DataMember]
        [JsonProperty("auftraggeber")]
        public int Auftraggeber { get; set; } = 0;
        [DataMember]
        [JsonProperty("auftraggeberString")]
        public string AuftraggeberString { get; set; } = string.Empty;

        [DataMember]
        [JsonProperty("Empfaenger")]
        public int Empfaenger { get; set; } = 0;

        [DataMember]
        [JsonProperty("empfaengerString")]
        public string EmpfaengerString { get; set; } = string.Empty;

        [DataMember]
        [JsonProperty("lieferant")]
        public string Lieferant { get; set; } = string.Empty;

        [DataMember]
        [JsonProperty("arbeitsbereichId")]
        public int ArbeitsbereichId { get; set; } = 0;

        [DataMember]
        [JsonProperty("mandantenId")]
        public int MandantenId { get; set; } = 0;

        [DataMember]
        [JsonProperty("lfsNr")]
        public string LfsNr { get; set; } = string.Empty;

        [DataMember]
        [JsonProperty("ASN")]
        public int ASN { get; set; } = 0;

        [DataMember]
        [JsonProperty("Check")]
        public bool Check { get; set; } = false;

        [DataMember]
        [JsonProperty("Versender")]
        public int Versender { get; set; } = 0;

        [DataMember]
        [JsonProperty("versenderString")]
        public string VersenderString { get; set; } = string.Empty;

        [DataMember]
        [JsonProperty("spedId")]
        public int SpedId { get; set; } = 0;

        [DataMember]
        [JsonProperty("Spediteur")]
        public string Spediteur { get; set; } = string.Empty;

        [DataMember]
        [JsonProperty("ExTransportRef")]
        public string ExTransportRef { get; set; } = string.Empty;

        [DataMember]
        [JsonProperty("ExAuftragRef")]
        public string ExAuftragRef { get; set; } = string.Empty;

        [DataMember]
        [JsonProperty("KFZ")]
        public string KFZ { get; set; } = string.Empty;
        [DataMember]
        [JsonProperty("DirektDelivery")]
        public bool DirektDelivery { get; set; } = false;
        [DataMember]
        [JsonProperty("Retoure")]
        public bool Retoure { get; set; } = false;
        [DataMember]
        [JsonProperty("Vorfracht")]
        public bool Vorfracht { get; set; } = false;
        [DataMember]
        [JsonProperty("LagerTransport")]
        public bool LagerTransport { get; set; } = false;
        [DataMember]
        [JsonProperty("WaggonNr")]
        public string WaggonNr { get; set; } = string.Empty;
        [DataMember]
        [JsonProperty("BeladeID")]
        public int BeladeID { get; set; } = 0;

        [DataMember]
        [JsonProperty("beladestelleString")]
        public string BeladestelleString { get; set; } = string.Empty;

        [DataMember]
        [JsonProperty("EntladeID")]
        public int EntladeID { get; set; } = 0;

        [DataMember]
        [JsonProperty("entladestelleString")]
        public string EntladestelleString { get; set; } = string.Empty;

        [DataMember]
        [JsonProperty("IsPrintDoc")]
        public bool IsPrintDoc { get; set; } = false;
        [DataMember]
        [JsonProperty("IsPrintAnzeige")]
        public bool IsPrintAnzeige { get; set; } = false;
        [DataMember]
        [JsonProperty("IsPrintLfs")]
        public bool IsPrintLfs { get; set; } = false;
        [DataMember]
        [JsonProperty("ASNRef")]
        public string ASNRef { get; set; } = string.Empty;
        [DataMember]
        [JsonProperty("LockedBy")]
        public int LockedBy { get; set; } = 0;
        [DataMember]
        [JsonProperty("IsWaggon")]
        public bool IsWaggon { get; set; } = false;
        [DataMember]
        [JsonProperty("Fahrer")]
        public string Fahrer { get; set; } = string.Empty;
        [DataMember]
        [JsonProperty("IsPrintList")]
        public bool IsPrintList { get; set; } = false;
        [DataMember]
        [JsonProperty("Ship")]
        public string Ship { get; set; } = string.Empty;

        [DataMember]
        [JsonProperty("IsShip")]
        public bool IsShip { get; set; } = false;
        [DataMember]
        [JsonProperty("Verlagerung")]
        public bool Verlagerung { get; set; } = false;

        [DataMember]
        [JsonProperty("Umbuchung")]
        public bool Umbuchung { get; set; } = false;

        [DataMember]
        [JsonProperty("PrintActionByScanner")]
        public bool PrintActionByScanner { get; set; } = false;

        [DataMember]
        [JsonProperty("PrintActionScannerAllLable")]
        public bool PrintActionScannerAllLable { get; set; } = false;

        [DataMember]
        [JsonProperty("PrintActionScannerEingangsliste")]
        public bool PrintActionScannerEingangsliste { get; set; } = false;


        [DataMember]
        [JsonProperty("ArticleCount")]
        public int ArticleCount { get; set; } = 0;


        [DataMember]
        [JsonProperty("WorkspaceName")]
        private string _WorkspaceName = string.Empty;
        public string WorkspaceName
        {
            get { return _WorkspaceName; }
            set { _WorkspaceName = value; }
        }

        [DataMember]
        [JsonProperty("ArticleCheckedCountStoreIn")]
        public int ArticleCheckedCountStoreIn { get; set; }

        [DataMember]
        [JsonProperty("CreatedByScanner")]
        public bool CreatedByScanner { get; set; } = false;


        [DataMember]
        [JsonProperty("Workspace")]
        private Workspaces _Workspace;
        public Workspaces Workspace
        {
            get { return _Workspace; }
            set
            {
                _Workspace = value;
                if (_Workspace != null)
                {
                    if (!_Workspace.Name.Equals(WorkspaceName))
                    {
                        WorkspaceName = _Workspace.Name;
                    }
                    if (!_Workspace.Id.Equals(ArbeitsbereichId))
                    {
                        ArbeitsbereichId = _Workspace.Id;
                    }
                    if (!_Workspace.MandantId.Equals(MandantenId))
                    {
                        MandantenId = _Workspace.MandantId;
                    }
                }

            }
        }


        [JsonIgnore]
        public enumEAStatus Status { get; set; }

        [JsonIgnore]
        public Color BadgeBackgroundColorStoreIn
        {
            get
            {
                bool bTmp = (ArticleCheckedCountStoreIn == ArticleCount);
                System.Drawing.Color colorReturn = System.Drawing.Color.Red;
                if (ArticleCount > 0)
                {
                    colorReturn = ValueToColorConverter.BadgeBackgroundColor_BooleanConvert(bTmp);
                }
                return colorReturn;
            }
        }

        [JsonIgnore]
        public Color ViewBackgroundcolor
        {
            get
            {
                Color tmpBC = ValueToColorConverter.ViewBackgroundColorWorkspace_IdConvert(ArbeitsbereichId);
                return tmpBC;
            }
        }


        public Eingaenge Copy()
        {
            return (Eingaenge)this.MemberwiseClone();
        }
    }
}
