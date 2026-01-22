using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;


namespace LVS.Models
{
    [Serializable]
    [DataContract]
    public class AsnArt
    {
        [DataMember]
        [JsonProperty("Id")]
        private decimal _Id;
        public decimal Id
        {
            get { return _Id; }
            set { _Id = value; }
        }

        [DataMember]
        [JsonProperty("Typ")]
        private string _Typ;
        public string Typ
        {
            get { return _Typ; }
            set { _Typ = value; }
        }

        [DataMember]
        [JsonProperty("Datum")]
        private DateTime _Datum;
        public DateTime Datum
        {
            get { return _Datum; }
            set { _Datum = value; }
        }

        [DataMember]
        [JsonProperty("Bezeichnung")]
        private string _Bezeichnung;
        public string Bezeichnung
        {
            get { return _Bezeichnung; }
            set { _Bezeichnung = value; }
        }

        [DataMember]
        [JsonProperty("Beschreibung")]
        private string _Beschreibung;
        public string Beschreibung
        {
            get { return _Beschreibung; }
            set { _Beschreibung = value; }
        }

        [DataMember]
        [JsonProperty("ListEdiSegments")]
        public List<EdiSegments> ListEdiSegments { get; set; } = new List<EdiSegments>();

        public AsnArt Copy()
        {
            return (AsnArt)this.MemberwiseClone();
        }
    }
}
