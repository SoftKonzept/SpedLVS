using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;

namespace LVS.Models
{
    [Serializable]
    [DataContract]
    public class VDAClientValues
    {
        [DataMember]
        [JsonProperty("Id")]
        private decimal _Id = 0;
        public decimal Id
        {
            get { return _Id; }
            set { _Id = value; }
        }

        [DataMember]
        [JsonProperty("AdrId")]
        private int _AdrId = 0;
        public int AdrId
        {
            get { return _AdrId; }
            set { _AdrId = value; }
        }

        [DataMember]
        [JsonProperty("AsnFieldId")]
        private int _AsnFieldId = 0;
        public int AsnFieldId
        {
            get { return _AsnFieldId; }
            set { _AsnFieldId = value; }
        }

        [DataMember]
        [JsonProperty("ValueArt")]
        private string _ValueArt = string.Empty;
        public string ValueArt
        {
            get { return _ValueArt; }
            set { _ValueArt = value; }
        }

        [DataMember]
        [JsonProperty("Value")]
        private string _Value = string.Empty;
        public string Value
        {
            get { return _Value; }
            set { _Value = value; }
        }

        [DataMember]
        [JsonProperty("Fill")]
        private bool _Fill = false;
        public bool Fill
        {
            get { return _Fill; }
            set { _Fill = value; }
        }

        [DataMember]
        [JsonProperty("Activ")]
        private bool _Activ = true;
        public bool Activ
        {
            get { return _Activ; }
            set { _Activ = value; }
        }

        [DataMember]
        [JsonProperty("NextSatz")]
        private int _NextSatz = 0;
        public int NextSatz
        {
            get { return _NextSatz; }
            set { _NextSatz = value; }
        }

        [DataMember]
        [JsonProperty("IsArtSatz")]
        private bool _IsArtSatz = false;
        public bool IsArtSatz
        {
            get { return _IsArtSatz; }
            set { _IsArtSatz = value; }
        }

        [DataMember]
        [JsonProperty("FillValue")]
        private string _FillValue = string.Empty;
        public string FillValue
        {
            get { return _FillValue; }
            set { _FillValue = value; }
        }

        [DataMember]
        [JsonProperty("FillLeft")]
        private bool _FillLeft = true;
        public bool FillLeft
        {
            get { return _FillLeft; }
            set { _FillLeft = value; }
        }

        [DataMember]
        [JsonProperty("ASNArtId")]
        private int _ASNArtId = 0;
        public int ASNArtId
        {
            get { return _ASNArtId; }
            set { _ASNArtId = value; }
        }

        [DataMember]
        [JsonProperty("Kennung")]
        private string _Kennung;
        public string Kennung
        {
            get { return _Kennung; }
            set { _Kennung = value; }
        }
        [DataMember]
        [JsonProperty("Description")]
        private string _Description;
        public string Description
        {
            get { return _Description; }
            set { _Description = value; }
        }

        public VDAClientValues Copy()
        {
            return (VDAClientValues)this.MemberwiseClone();
        }
    }
}
