using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;

namespace LVS.Models
{
    [Serializable]
    [DataContract]
    public class EdiSegmentElementFields
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
        [JsonProperty("EdiSemgentElementId")]
        private int _EdiSemgentElementId;
        public int EdiSemgentElementId
        {
            get { return _EdiSemgentElementId; }
            set { _EdiSemgentElementId = value; }
        }

        [DataMember]
        [JsonProperty("Shortcut")]
        private string _Shortcut;
        public string Shortcut
        {
            get { return _Shortcut; }
            set { _Shortcut = value; }
        }

        [DataMember]
        [JsonProperty("Name")]
        private string _Name;
        public string Name
        {
            get
            {
                return _Name;
            }
            set
            {
                string str = value;
                if (str.Contains("'"))
                {
                    str = str.Replace("'", "");
                }
                _Name = str;
                //_Name = value; 
            }
        }

        [DataMember]
        [JsonProperty("Status")]
        private string _Status;
        public string Status
        {
            get { return _Status; }
            set { _Status = value; }
        }

        [DataMember]
        [JsonProperty("Format")]
        private string _Format;
        public string Format
        {
            get { return _Format; }
            set { _Format = value; }
        }

        [DataMember]
        [JsonProperty("Description")]
        private string _Description;
        public string Description
        {
            get { return _Description; }
            set
            {
                string str = value;
                if (str.Contains("'"))
                {
                    str = str.Replace("'", "");
                }
                _Description = str;
                //_Description = value; 
            }
        }

        [DataMember]
        [JsonProperty("constValue")]
        private string _constValue;
        public string constValue
        {
            get { return _constValue; }
            set { _constValue = value; }
        }

        [DataMember]
        [JsonProperty("Position")]
        private int _Position;
        public int Position
        {
            get { return _Position; }
            set { _Position = value; }
        }

        [DataMember]
        [JsonProperty("Created")]
        private DateTime _Created;
        public DateTime Created
        {
            get { return _Created; }
            set { _Created = value; }
        }

        [DataMember]
        [JsonProperty("FormatString")]
        private string _FormatString;
        public string FormatString
        {
            get { return _FormatString; }
            set { _FormatString = value; }
        }

        [DataMember]
        [JsonProperty("Code")]
        private string _Code;
        public string Code
        {
            get { return _Code; }
            set { _Code = value; }
        }

        [DataMember]
        [JsonProperty("tmpId")]
        private int _tmpId;
        public int tmpId
        {
            get { return _tmpId; }
            set { _tmpId = value; }
        }

        [DataMember]
        [JsonProperty("SortId")]
        private int _SortId;
        public int SortId
        {
            get { return _SortId; }
            set { _SortId = value; }
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
        [JsonProperty("EdiSegmentId")]
        private int _EdiSegmentId;
        public int EdiSegmentId
        {
            get { return _EdiSegmentId; }
            set { _EdiSegmentId = value; }
        }

        [DataMember]
        [JsonProperty("AsnArtId")]
        private int _AsnArtId;
        public int AsnArtId
        {
            get { return _AsnArtId; }
            set { _AsnArtId = value; }
        }

        public EdiSegmentElementFields Copy()
        {
            return (EdiSegmentElementFields)this.MemberwiseClone();
        }
    }
}
