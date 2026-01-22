using Common.Enumerations;
using Common.Models;
using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Runtime.Serialization;
using Common.Helper;
using System.Drawing;

namespace Common.Models
{
    [Serializable]
    [DataContract]
    public class Calls
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
        [JsonProperty("isRead")]
        private bool _IsRead = false;
        public bool IsRead
        {
            get { return _IsRead; }
            set { _IsRead = value; }
        }

        [DataMember]
        [JsonProperty("artikelId")]
        private int _ArtikelId = 0;
        public int ArtikelId
        {
            get { return _ArtikelId; }
            set { _ArtikelId = value; }
        }

        [DataMember]
        [JsonProperty("Artikel")]
        private Articles _Artikel;
        public Articles Artikel
        {
            get { return _Artikel; }
            set { _Artikel = value; }
        }


        [DataMember]
        [JsonProperty("LVSNr")]
        private int _LVSNr = 0;
        public int LVSNr
        {
            get { return _LVSNr; }
            set { _LVSNr = value; }
        }

        [DataMember]
        [JsonProperty("werksnummer")]
        private string _Werksnummer = string.Empty;
        public string Werksnummer
        {
            get { return _Werksnummer; }
            set { _Werksnummer = value; }
        }

        [DataMember]
        [JsonProperty("produktionsnummer")]
        private string _Produktionsnummer = string.Empty;
        public string Produktionsnummer
        {
            get { return _Produktionsnummer; }
            set { _Produktionsnummer = value; }
        }

        [DataMember]
        [JsonProperty("charge")]
        private string _Charge = string.Empty;
        public string Charge
        {
            get { return _Charge; }
            set { _Charge = value; }
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
        [JsonProperty("datum")]
        private DateTime _Datum;
        public DateTime Datum
        {
            get { return _Datum; }
            set { _Datum = value; }
        }

        [DataMember]
        [JsonProperty("eintreffDatum")]
        private DateTime _EintreffDatum;
        public DateTime EintreffDatum
        {
            get { return _EintreffDatum; }
            set { _EintreffDatum = value; }
        }

        [DataMember]
        [JsonProperty("eintreffZeit")]
        private DateTime _EintreffZeit;
        public DateTime EintreffZeit
        {
            get { return _EintreffZeit; }
            set { _EintreffZeit = value; }
        }

        [DataMember]
        [JsonProperty("referenz")]
        private string _Referenz = string.Empty;
        public string Referenz
        {
            get { return _Referenz; }
            set { _Referenz = value; }
        }

        [DataMember]
        [JsonProperty("schicht")]
        private string _Schicht = string.Empty;
        public string Schicht
        {
            get { return _Schicht; }
            set { _Schicht = value; }
        }

        [DataMember]
        [JsonProperty("abladestelle")]
        private string _Abladestelle = string.Empty;
        public string Abladestelle
        {
            get { return _Abladestelle; }
            set { _Abladestelle = value; }
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
        [JsonProperty("aktion")]
        private string _Aktion = string.Empty;
        public string Aktion
        {
            get { return _Aktion; }
            set { _Aktion = value; }
        }

        [DataMember]
        [JsonProperty("benutzerID")]
        private int _BenutzerID = 0;
        public int BenutzerID
        {
            get { return _BenutzerID; }
            set { _BenutzerID = value; }
        }

        [DataMember]
        [JsonProperty("companyId")]
        private int _CompanyId = 0;
        public int CompanyId
        {
            get { return _CompanyId; }
            set { _CompanyId = value; }
        }

        [DataMember]
        [JsonProperty("CompanyName")]
        private string _CompanyName = string.Empty;
        public string CompanyName
        {
            get { return _CompanyName; }
            set { _CompanyName = value; }
        }


        [DataMember]
        [JsonProperty("benutzername")]
        private string _Benutzername = string.Empty;
        public string Benutzername
        {
            get { return _Benutzername; }
            set { _Benutzername = value; }
        }

        [DataMember]
        [JsonProperty("erstellt")]
        private DateTime _Erstellt;
        public DateTime Erstellt
        {
            get { return _Erstellt; }
            set { _Erstellt = value; }
        }

        [DataMember]
        [JsonProperty("status")]
        private enumCallStatus _Status;
        public enumCallStatus Status
        {
            get { return _Status; }
            set { _Status = value; }
        }

        [DataMember]
        [JsonProperty("liefAdrId")]
        private int _LiefAdrId = 0;
        public int LiefAdrId
        {
            get { return _LiefAdrId; }
            set { _LiefAdrId = value; }
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
        [JsonProperty("empAdrId")]
        private int _EmpAdrId = 0;
        public int EmpAdrId
        {
            get { return _EmpAdrId; }
            set { _EmpAdrId = value; }
        }

        [DataMember]
        [JsonProperty("emfaenger")]
        private string _Emfaenger = string.Empty;
        public string Emfaenger
        {
            get { return _Emfaenger; }
            set { _Emfaenger = value; }
        }

        [DataMember]
        [JsonProperty("spedAdrId")]
        private int _SpedAdrId = 0;
        public int SpedAdrId
        {
            get { return _SpedAdrId; }
            set { _SpedAdrId = value; }
        }

        [DataMember]
        [JsonProperty("spediteur")]
        private string _Spediteur = string.Empty;
        public string Spediteur
        {
            get { return _Spediteur; }
            set { _Spediteur = value; }
        }

        [DataMember]
        [JsonProperty("ASNFile")]
        private string _ASNFile = string.Empty;
        public string ASNFile
        {
            get { return _ASNFile; }
            set { _ASNFile = value; }
        }

        [DataMember]
        [JsonProperty("ASNLieferant")]
        private string _ASNLieferant = string.Empty;
        public string ASNLieferant
        {
            get { return _ASNLieferant; }
            set { _ASNLieferant = value; }
        }

        [DataMember]
        [JsonProperty("ASNQuantity")]
        private int _ASNQuantity = 0;
        public int ASNQuantity
        {
            get { return _ASNQuantity; }
            set { _ASNQuantity = value; }
        }

        [DataMember]
        [JsonProperty("ASNUnit")]
        private string _ASNUnit = string.Empty;
        public string ASNUnit
        {
            get { return _ASNUnit; }
            set { _ASNUnit = value; }
        }

        [DataMember]
        [JsonProperty("Description")]
        private string _Description = string.Empty;
        public string Description
        {
            get { return _Description; }
            set { _Description = value; }
        }

        [DataMember]
        [JsonProperty("ScanCheckForStoreOut")]
        public DateTime ScanCheckForStoreOut { get; set; } = new DateTime(1900, 1, 1);

        [DataMember]
        [JsonProperty("ScanUserId")]
        public int ScanUserId { get; set; } = 0;

        [DataMember]
        [JsonProperty("Workspace")]
        public Workspaces Workspace { get; set; } = new Workspaces();

        [DataMember]
        [JsonProperty("EdiDelforD97AValueId")]
        private int _EdiDelforD97AValueId = 0;
        public int EdiDelforD97AValueId
        {
            get { return _EdiDelforD97AValueId; }
            set { _EdiDelforD97AValueId = value; }
        }


        public Color ViewBackgroundColor
        {
            get
            {
                Color tmpBC = ValueToColorConverter.ViewBackgroundColorWorkspace_IdConvert(this.ArbeitsbereichId);
                return tmpBC;
            }
        }
        public bool IsScanned
        {
            get
            {
                bool bReturn = false;
                bReturn = (ScanCheckForStoreOut > new DateTime(1900, 1, 1));
                return bReturn;
            }
        }

        public Calls Copy()
        {
            return (Calls)this.MemberwiseClone();
        }
    }
}
