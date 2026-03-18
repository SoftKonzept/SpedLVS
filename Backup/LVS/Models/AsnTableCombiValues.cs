using Common.Models;
using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;


namespace LVS.Models
{
    [Serializable]
    [DataContract]
    public class AsnTableCombiValues
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
        [JsonProperty("WorkspaceId")]
        private int _WorkspaceId;
        public int WorkspaceId
        {
            get { return _WorkspaceId; }
            set { _WorkspaceId = value; }
        }

        [DataMember]
        [JsonProperty("Sender")]
        private int _Sender;
        public int Sender
        {
            get { return _Sender; }
            set { _Sender = value; }
        }

        [DataMember]
        [JsonProperty("SenderAdress")]
        private Addresses _SenderAdress;
        public Addresses SenderAdress
        {
            get { return _SenderAdress; }
            set { _SenderAdress = value; }
        }

        [DataMember]
        [JsonProperty("Receiver")]
        private int _Receiver;
        public int Receiver
        {
            get { return _Receiver; }
            set { _Receiver = value; }
        }

        [DataMember]
        [JsonProperty("ReceiverAdress")]
        private Addresses _ReceiverAdress;
        public Addresses ReceiverAdress
        {
            get { return _ReceiverAdress; }
            set { _ReceiverAdress = value; }
        }



        [DataMember]
        [JsonProperty("TableName")]
        private string _TableName;
        public string TableName
        {
            get { return _TableName; }
            set { _TableName = value; }
        }

        [DataMember]
        [JsonProperty("ColValue")]
        private string _ColValue;
        public string ColValue
        {
            get { return _ColValue; }
            set { _ColValue = value; }
        }

        [DataMember]
        [JsonProperty("UseValueSeparator")]
        private bool _UseValueSeparator;
        public bool UseValueSeparator
        {
            get { return _UseValueSeparator; }
            set { _UseValueSeparator = value; }
        }

        [DataMember]
        [JsonProperty("ColsForCombination")]
        private string _ColsForCombination;
        public string ColsForCombination
        {
            get { return _ColsForCombination; }
            set { _ColsForCombination = value; }
        }

        [DataMember]
        [JsonProperty("ValueSeparator")]
        private string _ValueSeparator;
        public string ValueSeparator
        {
            get { return _ValueSeparator; }
            set { _ValueSeparator = value; }
        }



        public AsnTableCombiValues Copy()
        {
            return (AsnTableCombiValues)this.MemberwiseClone();
        }
    }
}
