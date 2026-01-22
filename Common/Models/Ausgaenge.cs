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
    public class Ausgaenge
    {
        [DataMember]
        [JsonProperty("id")]
        private int _Id = 0;
        public int Id
        {
            get { return _Id; }
            set { _Id = value; }
        }

        [DataMember]
        [JsonProperty("LAusgangID")]
        private int _LAusgangID = 0;
        public int LAusgangID
        {
            get { return _LAusgangID; }
            set { _LAusgangID = value; }
        }

        [DataMember]
        [JsonProperty("datum")]
        private DateTime _Datum;
        public DateTime Datum
        {
            get { return _Datum; }
            set { _Datum = value; }
        }

        [DataMember]
        [JsonProperty("brutto")]
        private decimal _Brutto = 0;
        public decimal Brutto
        {
            get { return _Brutto; }
            set { _Brutto = value; }
        }

        [DataMember]
        [JsonProperty("netto")]
        private decimal _Netto = 0;
        public decimal Netto
        {
            get { return _Netto; }
            set { _Netto = value; }
        }

        [DataMember]
        [JsonProperty("auftraggeber")]
        private int _Auftraggeber = 0;
        public int Auftraggeber
        {
            get { return _Auftraggeber; }
            set { _Auftraggeber = value; }
        }

        [DataMember]
        [JsonProperty("auftraggeberString")]
        private string _AuftraggeberString = string.Empty;
        public string AuftraggeberString
        {
            get { return _AuftraggeberString; }
            set { _AuftraggeberString = value; }
        }

        [DataMember]
        [JsonProperty("Versender")]
        private int _Versender = 0;
        public int Versender
        {
            get { return _Versender; }
            set { _Versender = value; }
        }

        [DataMember]
        [JsonProperty("versenderString")]
        private string _VersenderString = string.Empty;
        public string VersenderString
        {
            get { return _VersenderString; }
            set { _VersenderString = value; }
        }

        [DataMember]
        [JsonProperty("Empfaenger")]
        private int _Empfaenger = 0;
        public int Empfaenger
        {
            get { return _Empfaenger; }
            set { _Empfaenger = value; }
        }

        [DataMember]
        [JsonProperty("empfaengerString")]
        private string _EmpfaengerString = string.Empty;
        public string EmpfaengerString
        {
            get { return _EmpfaengerString; }
            set { _EmpfaengerString = value; }
        }

        [DataMember]
        [JsonProperty("Entladestelle")]
        private int _Entladestelle = 0;
        public int Entladestelle
        {
            get { return _Entladestelle; }
            set { _Entladestelle = value; }
        }

        [DataMember]
        [JsonProperty("entladestelleString")]
        private string _EntladestelleString = string.Empty;
        public string EntladestelleString
        {
            get { return _EntladestelleString; }
            set { _EntladestelleString = value; }
        }

        [DataMember]
        [JsonProperty("lieferant")]
        private string _Lieferant = string.Empty;
        public string Lieferant
        {
            get { return _Lieferant; }
            set { _Lieferant = value; }
        }

        [DataMember]
        [JsonProperty("lfsNr")]
        private string _LfsNr = string.Empty;
        public string LfsNr
        {
            get { return _LfsNr; }
            set { _LfsNr = value; }
        }

        [DataMember]
        [JsonProperty("slb")]
        private int _SLB = 0;
        public int SLB
        {
            get { return _SLB; }
            set { _SLB = value; }
        }

        [DataMember]
        [JsonProperty("mat")]
        private string _MAT;
        public string MAT
        {
            get { return _MAT; }
            set { _MAT = value; }
        }

        [DataMember]
        [JsonProperty("checked")]
        private bool _Checked = false;
        public bool Checked
        {
            get { return _Checked; }
            set { _Checked = value; }
        }

        [DataMember]
        [JsonProperty("spedId")]
        private int _SpedId = 0;
        public int SpedId
        {
            get { return _SpedId; }
            set { _SpedId = value; }
        }

        [DataMember]
        [JsonProperty("Spediteur")]
        private string _Spediteur = string.Empty;
        public string Spediteur
        {
            get { return _Spediteur; }
            set { _Spediteur = value; }
        }

        [DataMember]
        [JsonProperty("KFZ")]
        private string _KFZ = string.Empty;
        public string KFZ
        {
            get { return _KFZ; }
            set { _KFZ = value; }
        }

        [DataMember]
        [JsonProperty("user")]
        private int _User = 0;
        public int User
        {
            get { return _User; }
            set { _User = value; }
        }

        [DataMember]
        [JsonProperty("ASN")]
        private int _ASN = 0;
        public int ASN
        {
            get { return _ASN; }
            set { _ASN = value; }
        }

        [DataMember]
        [JsonProperty("info")]
        private string _Info = string.Empty;
        public string Info
        {
            get { return _Info; }
            set { _Info = value; }
        }

        [DataMember]
        [JsonProperty("arbeitsbereichId")]
        private int _ArbeitsbereichId = 0;
        public int ArbeitsbereichId
        {
            get { return _ArbeitsbereichId; }
            set { _ArbeitsbereichId = value; }
        }

        [DataMember]
        [JsonProperty("WorkspaceName")]
        private string _WorkspaceName = string.Empty;
        public string WorkspaceName
        {
            get { return _WorkspaceName; }
            set { _WorkspaceName = value; }
        }

        [DataMember]
        [JsonProperty("mandantenID")]
        private int _MandantenID = 0;
        public int MandantenID
        {
            get { return _MandantenID; }
            set { _MandantenID = value; }
        }

        [DataMember]
        [JsonProperty("Termin")]
        private DateTime _Termin;
        public DateTime Termin
        {
            get { return _Termin; }
            set { _Termin = value; }
        }

        [DataMember]
        [JsonProperty("DirectDelivery")]
        private bool _DirectDelivery = false;
        public bool DirectDelivery
        {
            get { return _DirectDelivery; }
            set { _DirectDelivery = value; }
        }

        [DataMember]
        [JsonProperty("neutrAuftraggeber")]
        private int _neutrAuftraggeber = 0;
        public int neutrAuftraggeber
        {
            get { return _neutrAuftraggeber; }
            set { _neutrAuftraggeber = value; }
        }

        [DataMember]
        [JsonProperty("neutrEmpfaenger")]
        private int _neutrEmpfaenger = 0;
        public int neutrEmpfaenger
        {
            get { return _neutrEmpfaenger; }
            set { _neutrEmpfaenger = value; }
        }

        [DataMember]
        [JsonProperty("lagerTransport")]
        private bool _LagerTransport = false;
        public bool LagerTransport
        {
            get { return _LagerTransport; }
            set { _LagerTransport = value; }
        }

        [DataMember]
        [JsonProperty("waggonNo")]
        private string _WaggonNo = string.Empty;
        public string WaggonNo
        {
            get { return _WaggonNo; }
            set { _WaggonNo = value; }
        }

        [DataMember]
        [JsonProperty("BeladeId")]
        private int _BeladeId = 0;
        public int BeladeId
        {
            get { return _BeladeId; }
            set { _BeladeId = value; }
        }

        [DataMember]
        [JsonProperty("isPrintDoc")]
        private bool _IsPrintDoc = false;
        public bool IsPrintDoc
        {
            get { return _IsPrintDoc; }
            set { _IsPrintDoc = value; }
        }
        [DataMember]
        [JsonProperty("isPrintAnzeige")]
        private bool _IsPrintAnzeige = false;
        public bool IsPrintAnzeige
        {
            get { return _IsPrintAnzeige; }
            set { _IsPrintAnzeige = value; }
        }

        [DataMember]
        [JsonProperty("isPrintLfs")]
        private bool _IsPrintLfs = false;
        public bool IsPrintLfs
        {
            get { return _IsPrintLfs; }
            set { _IsPrintLfs = value; }
        }

        [DataMember]
        [JsonProperty("lockedBy")]
        private int _LockedBy = 0;
        public int LockedBy
        {
            get { return _LockedBy; }
            set { _LockedBy = value; }
        }

        [DataMember]
        [JsonProperty("isWaggon")]
        private bool _IsWaggon = false;
        public bool IsWaggon
        {
            get { return _IsWaggon; }
            set { _IsWaggon = value; }
        }

        [DataMember]
        [JsonProperty("exTransportRef")]
        private string _ExTransportRef = string.Empty;
        public string ExTransportRef
        {
            get { return _ExTransportRef; }
            set { _ExTransportRef = value; }
        }

        [DataMember]
        [JsonProperty("fahrer")]
        private string _Fahrer = string.Empty;
        public string Fahrer
        {
            get { return _Fahrer; }
            set { _Fahrer = value; }
        }

        [DataMember]
        [JsonProperty("isRL")]
        private bool _IsRL = false;
        public bool IsRL
        {
            get { return _IsRL; }
            set { _IsRL = value; }
        }

        [DataMember]
        [JsonProperty("isPrintList")]
        private bool _IsPrintList = false;
        public bool IsPrintList
        {
            get { return _IsPrintList; }
            set { _IsPrintList = value; }
        }

        [DataMember]
        [JsonProperty("trailer")]
        private string _Trailer = string.Empty;
        public string Trailer
        {
            get { return _Trailer; }
            set { _Trailer = value; }
        }


        [DataMember]
        [JsonProperty("ArticleCount")]
        private int _ArticleCount = 0;
        public int ArticleCount
        {
            get { return _ArticleCount; }
            set { _ArticleCount = value; }
        }

        [DataMember]
        [JsonProperty("ArticleCheckedCountStoreOut")]
        public int ArticleCheckedCountStoreOut { get; set; } = 0;


        [DataMember]
        [JsonProperty("PrintActionScannerLfs")]
        public bool PrintActionScannerLfs { get; set; } = false;

        [DataMember]
        [JsonProperty("PrintActionScannerKVOFrachtbrief")]
        public bool PrintActionScannerKVOFrachtbrief { get; set; } = false;

        [DataMember]
        [JsonProperty("PrintActionScannerAusgangsliste")]
        public bool PrintActionScannerAusgangsliste { get; set; } = false;

        [DataMember]
        [JsonProperty("PrintDocumentStoreOutStatus_Frachtbrief")]
        public enumPrintDocumentStoreOutStatus_Frachtbrief PrintDocumentStoreOutStatus_Frachtbrief { get; set; } = enumPrintDocumentStoreOutStatus_Frachtbrief.NotSet;

        [DataMember]
        [JsonProperty("PrintDocumentStoreOutStatus_Lfs")]
        public enumPrintDocumentStoreOutStatus_Lfs PrintDocumentStoreOutStatus_Lfs { get; set; } = enumPrintDocumentStoreOutStatus_Lfs.NotSet;

        [DataMember]
        [JsonProperty("PrintDocumentStoreOutStatus_List")]
        public enumPrintDocumentStoreOutStatus_List PrintDocumentStoreOutStatus_List { get; set; } = enumPrintDocumentStoreOutStatus_List.NotSet;



        [DataMember]
        [JsonProperty("Workspace")]
        private Workspaces _Workspace;
        public Workspaces Workspace
        {
            get { return _Workspace; }
            set { _Workspace = value; }
        }

        public enumEAStatus Status { get; set; }

        public Color BadgeBackgroundColorStoreOut
        {
            get
            {
                bool bTmp = (ArticleCheckedCountStoreOut == ArticleCount);
                //Color tmpBC = ValueToColorConverter.BadgeBackgroundColor_BooleanConvert(bTmp);

                System.Drawing.Color colorReturn = Color.Red;
                if (bTmp)
                {
                    colorReturn = Color.LimeGreen;
                }
                else
                {
                    colorReturn = Color.Gray;
                }

                return colorReturn;
            }
        }

        public Color ViewBackgroundcolor
        {
            get
            {
                Color tmpBC = ValueToColorConverter.ViewBackgroundColorWorkspace_IdConvert(this.ArbeitsbereichId);
                return tmpBC;
            }
        }

        public Ausgaenge Copy()
        {
            return (Ausgaenge)this.MemberwiseClone();
        }
    }
}
