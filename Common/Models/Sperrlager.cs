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
    public class Sperrlager
    {
        [DataMember]
        [JsonProperty("id")]
        private int _Id;
        public int Id
        {
            get { return _Id; }
            set { _Id = value; }
        }

        [DataMember]
        [JsonProperty("ArtikelID")]
        private int _ArtikelID;
        public int ArtikelID
        {
            get { return _ArtikelID; }
            set { _ArtikelID = value; }
        }

        [DataMember]
        [JsonProperty("UserID")]
        private int _UserID;
        public int UserID
        {
            get { return _UserID; }
            set { _UserID = value; }
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
        [JsonProperty("BKZ")]
        private string _BKZ;
        public string BKZ
        {
            get { return _BKZ; }
            set { _BKZ = value; }
        }

        [DataMember]
        [JsonProperty("SPLIDIn")]
        private int _SPLIDIn;
        public int SPLIDIn
        {
            get { return _SPLIDIn; }
            set { _SPLIDIn = value; }
        }

        [DataMember]
        [JsonProperty("DefWindungen")]
        private int _DefWindungen;
        public int DefWindungen
        {
            get { return _DefWindungen; }
            set { _DefWindungen = value; }
        }

        [DataMember]
        [JsonProperty("Sperrgrund")]
        private string _Sperrgrund;
        public string Sperrgrund
        {
            get { return _Sperrgrund; }
            set { _Sperrgrund = value; }
        }

        [DataMember]
        [JsonProperty("Vermerk")]
        private string _Vermerk;
        public string Vermerk
        {
            get { return _Vermerk; }
            set { _Vermerk = value; }
        }

        [DataMember]
        [JsonProperty("IsCustomCertificateMissing")]
        private bool _IsCustomCertificateMissing;
        public bool IsCustomCertificateMissing
        {
            get { return _IsCustomCertificateMissing; }
            set { _IsCustomCertificateMissing = value; }
        }

        public Sperrlager Copy()
        {
            return (Sperrlager)this.MemberwiseClone();
        }
    }
}
