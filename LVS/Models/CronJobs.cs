using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;


namespace LVS.Models
{
    [Serializable]
    [DataContract]
    public class CronJobs
    {
        [DataMember]
        [JsonProperty("Id")]
        private int _Id;
        public int Id
        {
            get { return _Id; }
            set { _Id = value; }
        }

        [DataMember]
        [JsonProperty("Aktion")]
        private enumCronJobAction _Aktion;
        public enumCronJobAction Aktion
        {
            get { return _Aktion; }
            set { _Aktion = value; }
        }

        [DataMember]
        [JsonProperty("Aktionsdatum")]
        private DateTime _Aktionsdatum;
        public DateTime Aktionsdatum
        {
            get { return _Aktionsdatum; }
            set { _Aktionsdatum = value; }
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
        [JsonProperty("Periode")]
        private string _Periode;
        public string Periode
        {
            get { return _Periode; }
            set { _Periode = value; }
        }

        [DataMember]
        [JsonProperty("vZeitraum")]
        private DateTime _vZeitraum;
        public DateTime vZeitraum
        {
            get { return _vZeitraum; }
            set { _vZeitraum = value; }
        }

        [DataMember]
        [JsonProperty("bZeitraum")]
        private DateTime _bZeitraum;
        public DateTime bZeitraum
        {
            get { return _bZeitraum; }
            set { _bZeitraum = value; }
        }

        [DataMember]
        [JsonProperty("aktiv")]
        private bool _aktiv;
        public bool aktiv
        {
            get { return _aktiv; }
            set { _aktiv = value; }
        }

        [DataMember]
        [JsonProperty("AdrId")]
        private int _AdrId;
        public int AdrId
        {
            get { return _AdrId; }
            set { _AdrId = value; }
        }

        //[DataMember]
        //[JsonProperty("ListEdiSegments")]
        //public List<EdiSegments> ListEdiSegments { get; set; } = new List<EdiSegments>();

        public CronJobs Copy()
        {
            return (CronJobs)this.MemberwiseClone();
        }
    }
}
