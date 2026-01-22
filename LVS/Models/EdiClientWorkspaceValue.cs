using Common.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;


namespace LVS.Models
{
    [Serializable]
    [DataContract]
    public class EdiClientWorkspaceValue
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
        [JsonProperty("AdrId")]
        private int _AdrId;
        public int AdrId
        {
            get { return _AdrId; }
            set { _AdrId = value; }
        }

        [DataMember]
        [JsonProperty("Address")]
        private Addresses _Address;
        public Addresses Address
        {
            get { return _Address; }
            set { _Address = value; }
        }

        [DataMember]
        [JsonProperty("_WorkspaceId")]
        private int _WorkspaceId;
        public int WorkspaceId
        {
            get { return _WorkspaceId; }
            set { _WorkspaceId = value; }
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
        [JsonProperty("Property")]
        private string _Property;
        public string Property
        {
            get { return _Property; }
            set { _Property = value; }
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
        [JsonProperty("Created")]
        private DateTime _Created;
        public DateTime Created
        {
            get { return _Created; }
            set { _Created = value; }
        }

        [DataMember]
        [JsonProperty("Direction")]
        private string _Direction;
        public string Direction
        {
            get { return _Direction; }
            set { _Direction = value; }
        }

        [DataMember]
        [JsonProperty("ListEdiAdrWorkspaceAssignment")]
        public List<EdiClientWorkspaceValue> ListEdiAdrWorkspaceAssignment { get; set; } = new List<EdiClientWorkspaceValue>();

        public EdiClientWorkspaceValue Copy()
        {
            return (EdiClientWorkspaceValue)this.MemberwiseClone();
        }
    }
}
