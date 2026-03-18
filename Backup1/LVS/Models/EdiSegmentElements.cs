using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace LVS.Models
{
    [Serializable]
    [DataContract]
    public class EdiSegmentElements
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
        [JsonProperty("EdiSegmentId")]
        private int _EdiSegmentId;
        public int EdiSegmentId
        {
            get { return _EdiSegmentId; }
            set { _EdiSegmentId = value; }
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
        [JsonProperty("Description")]
        private string _Description;
        public string Description
        {
            get { return _Description; }
            set { _Description = value; }
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
        [JsonProperty("tmpId")]
        private int _tmpId;
        public int tmpId
        {
            get { return _tmpId; }
            set { _tmpId = value; }
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
        [JsonProperty("Kennung")]
        private string _Kennung;
        public string Kennung
        {
            get { return _Kennung; }
            set { _Kennung = value; }
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
        [JsonProperty("ListEdiSegmentElementFields")]
        public List<EdiSegmentElementFields> ListEdiSegmentElementFields { get; set; } = new List<EdiSegmentElementFields>();

        public EdiSegmentElements Copy()
        {
            return (EdiSegmentElements)this.MemberwiseClone();
        }

    }
}
