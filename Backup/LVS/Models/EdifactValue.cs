using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;

namespace LVS.Models
{
    [Serializable]
    [DataContract]
    public class EdifactValue
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
        [JsonProperty("AsnId")]
        private int _AsnId;
        public int AsnId
        {
            get { return _AsnId; }
            set { _AsnId = value; }
        }

        [DataMember]
        [JsonProperty("EdiSegmentElement")]
        private string _EdiSegmentElement;
        public string EdiSegmentElement
        {
            get { return _EdiSegmentElement; }
            set { _EdiSegmentElement = value; }
        }

        [DataMember]
        [JsonProperty("EdiSegmentElementValue")]
        private string _EdiSegmentElementValue;
        public string EdiSegmentElementValue
        {
            get { return _EdiSegmentElementValue; }
            set { _EdiSegmentElementValue = value; }
        }

        [DataMember]
        [JsonProperty("Property")]
        private string _Property;
        public string Property
        {
            get { return _Property; }
            set { _Property = value; }
        }

        [DataMember]
        [JsonProperty("OrderId")]
        private int _OrderId = 0;
        public int OrderId
        {
            get { return _OrderId; }
            set { _OrderId = value; }
        }

        public EdifactValue Copy()
        {
            return (EdifactValue)this.MemberwiseClone();
        }

    }
}
