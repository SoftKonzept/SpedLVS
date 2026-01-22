using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;

namespace LVS.Models
{
    [Serializable]
    [DataContract]
    public class _EdiQalityD96AValue
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
        [JsonProperty("EdiQalityId")]
        private int _EdiQalityId;
        public int EdiQalityId
        {
            get { return _EdiQalityId; }
            set { _EdiQalityId = value; }
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

    }
}
