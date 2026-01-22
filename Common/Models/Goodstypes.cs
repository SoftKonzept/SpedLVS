using Common.Enumerations;
using Common.Helper;
using Common.Models;
using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Common.Models
{
    [Serializable]
    [DataContract]
    public class Goodstypes
    {
        [DataMember]
        [JsonProperty("Id")]
        public int Id { get; set; }

        [DataMember]
        [JsonProperty("ViewID")]
        public string ViewID { get; set; }

        [DataMember]
        [JsonProperty("Menge")]
        public int Menge { get; set; }

        [DataMember]
        [JsonProperty("Bezeichnung")]
        public string Bezeichnung { get; set; }

        [DataMember]
        [JsonProperty("Dicke")]
        public decimal Dicke { get; set; }
        [DataMember]
        [JsonProperty("Hoehe")]
        public decimal Hoehe { get; set; }
        [DataMember]
        [JsonProperty("Laenge")]
        public decimal Laenge { get; set; }
        [DataMember]
        [JsonProperty("Breite")]
        public decimal Breite { get; set; }

        [DataMember]
        [JsonProperty("MassAnzahl")]
        public int MassAnzahl { get; set; }

        [DataMember]
        [JsonProperty("Netto")]
        public decimal Netto { get; set; }

        [DataMember]
        [JsonProperty("Brutto")]
        public decimal Brutto { get; set; }

        [DataMember]
        [JsonProperty("ArtikelArt")]
        public string ArtikelArt { get; set; }

        [DataMember]
        [JsonProperty("Besonderheit")]
        public string Besonderheit { get; set; }

        [DataMember]
        [JsonProperty("Verpackung")]
        public string Verpackung { get; set; }
        [DataMember]
        [JsonProperty("AbsteckBolzenNr")]
        public string AbsteckBolzenNr { get; set; }

        [DataMember]
        [JsonProperty("MEAbsteckBolzen")]
        public int MEAbsteckBolzen { get; set; }

        [DataMember]
        [JsonProperty("ArbeitsbereichId")]
        public int ArbeitsbereichId { get; set; }

        [DataMember]
        [JsonProperty("LieferantenId")]
        public int LieferantenId { get; set; }
        public bool Aktiv { get; set; }

        [DataMember]
        [JsonProperty("MindestBestand")]
        public int MindestBestand { get; set; }

        [DataMember]
        [JsonProperty("BestellNr")]
        public string BestellNr { get; set; }

        [DataMember]
        [JsonProperty("Einheit")]
        public string Einheit { get; set; }

        [DataMember]
        [JsonProperty("Zusatz")]
        public string Zusatz { get; set; }

        [DataMember]
        [JsonProperty("Verweis")]
        public string Verweis { get; set; }

        [DataMember]
        [JsonProperty("Werksnummer")]
        public string Werksnummer { get; set; }

        [DataMember]
        [JsonProperty("VDA4905LieferantenInfo")]
        public string VDA4905LieferantenInfo { get; set; }

        [DataMember]
        [JsonProperty("IsStackable")]
        public bool IsStackable { get; set; }

        [DataMember]
        [JsonProperty("UseProdNrCheck")]
        public bool UseProdNrCheck { get; set; }

        [DataMember]
        [JsonProperty("tmpLiefVerweis")]
        public string tmpLiefVerweis { get; set; }

        [DataMember]
        [JsonProperty("[DelforVerweis]")]
        public string DelforVerweis { get; set; }

        [DataMember]
        [JsonProperty("IgnoreEdi")]
        public bool IgnoreEdi { get; set; }


        public string GoodstypeString
        {
            get
            {
                string strReturn = string.Empty;
                if ((this is Goodstypes) && (this.Id > 0))
                {
                    strReturn = ViewID.ToString() + " - " + this.Bezeichnung;
                }
                return strReturn;
            }

        }

        public Goodstypes Copy()
        {
            return (Goodstypes)this.MemberwiseClone();
        }
    }
}
