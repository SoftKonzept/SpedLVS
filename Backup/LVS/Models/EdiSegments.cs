using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace LVS.Models
{
    [Serializable]
    [DataContract]
    public class EdiSegments
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
        [JsonProperty("AsnArtId")]
        private int _AsnArtId;
        public int AsnArtId
        {
            get { return _AsnArtId; }
            set { _AsnArtId = value; }
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
        [JsonProperty("Name")]
        private string _Name;
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
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
        [JsonProperty("RepeatCount")]
        private int _RepeatCount;
        public int RepeatCount
        {
            get { return _RepeatCount; }
            set { _RepeatCount = value; }
        }

        [DataMember]
        [JsonProperty("Ebene")]
        private int _Ebene;
        public int Ebene
        {
            get { return _Ebene; }
            set { _Ebene = value; }
        }

        [DataMember]
        [JsonProperty("Description")]
        private string _Description;
        public string Description
        {
            get { return _Description; }
            set { _Description = value; }
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
        [JsonProperty("Storable")]
        private bool _Storable;
        public bool Storable
        {
            get { return _Storable; }
            set { _Storable = value; }
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
        [JsonProperty("SortId")]
        private int _SortId;
        public int SortId
        {
            get { return _SortId; }
            set { _SortId = value; }
        }

        [DataMember]
        [JsonProperty("IsActive")]
        private bool _IsActive;
        public bool IsActive
        {
            get { return _IsActive; }
            set { _IsActive = value; }
        }

        [DataMember]
        [JsonProperty("EdiSegmentCheckFunction")]
        private string _EdiSegmentCheckFunction;
        public string EdiSegmentCheckFunction
        {
            get { return _EdiSegmentCheckFunction; }
            set { _EdiSegmentCheckFunction = value; }
        }

        [DataMember]
        [JsonProperty("ListEdiSegmentElements")]
        public List<EdiSegmentElements> ListEdiSegmentElements { get; set; } = new List<EdiSegmentElements>();

        public EdiSegments Copy()
        {
            return (EdiSegments)this.MemberwiseClone();
        }
    }
}
