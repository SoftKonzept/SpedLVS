using Common.Models;
using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;


namespace LVS.Models
{
    [Serializable]
    [DataContract]
    public class ASNArtFieldAssignment
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
        [JsonProperty("ASNField")]
        private string _ASNField;
        public string ASNField
        {
            get { return _ASNField; }
            set { _ASNField = value; }
        }

        [DataMember]
        [JsonProperty("ArtField")]
        private string _ArtField;
        public string ArtField
        {
            get { return _ArtField; }
            set { _ArtField = value; }
        }

        [DataMember]
        [JsonProperty("IsDefValue")]
        private bool _IsDefValue;
        public bool IsDefValue
        {
            get { return _IsDefValue; }
            set { _IsDefValue = value; }
        }

        [DataMember]
        [JsonProperty("DefValue")]
        private string _DefValue;
        public string DefValue
        {
            get { return _DefValue; }
            set { _DefValue = value; }
        }

        [DataMember]
        [JsonProperty("CopyToField")]
        private string _CopyToField;
        public string CopyToField
        {
            get { return _CopyToField; }
            set { _CopyToField = value; }
        }

        [DataMember]
        [JsonProperty("FormatFunction")]
        private string _FormatFunction;
        public string FormatFunction
        {
            get { return _FormatFunction; }
            set { _FormatFunction = value; }
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
        [JsonProperty("Workspace")]
        private Workspaces _Workspace;
        public Workspaces Workspace
        {
            get { return _Workspace; }
            set { _Workspace = value; }
        }

        [DataMember]
        [JsonProperty("IsGlobalFieldVar")]
        private bool _IsGlobalFieldVar;
        public bool IsGlobalFieldVar
        {
            get { return _IsGlobalFieldVar; }
            set { _IsGlobalFieldVar = value; }
        }

        [DataMember]
        [JsonProperty("GlobalFieldVar")]
        private string _GlobalFieldVar;
        public string GlobalFieldVar
        {
            get { return _GlobalFieldVar; }
            set { _GlobalFieldVar = value; }
        }

        [DataMember]
        [JsonProperty("SubASNField")]
        private string _SubASNField;
        public string SubASNField
        {
            get { return _SubASNField; }
            set { _SubASNField = value; }
        }


        public ASNArtFieldAssignment Copy()
        {
            return (ASNArtFieldAssignment)this.MemberwiseClone();
        }
    }
}
