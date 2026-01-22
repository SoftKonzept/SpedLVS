using Common.Models;
using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;

namespace LVS.Models
{
    [Serializable]
    [DataContract]
    public class EdiZQMQalityXml
    {
        [DataMember]
        [JsonProperty("Id")]
        private int _Id = 0;
        public int Id
        {
            get { return _Id; }
            set { _Id = value; }
        }

        [DataMember]
        [JsonProperty("iDocNo")]
        private string _iDocNo = string.Empty;
        public string iDocNo
        {
            get { return _iDocNo; }
            set { _iDocNo = value; }
        }

        [DataMember]
        [JsonProperty("iDocDate")]
        private DateTime _iDocDate = new DateTime(1900, 1, 1);
        public DateTime iDocDate
        {
            get { return _iDocDate; }
            set { _iDocDate = value; }
        }

        [DataMember]
        [JsonProperty("Path")]
        private string _Path = string.Empty;
        public string Path
        {
            get { return _Path; }
            set { _Path = value; }
        }

        [DataMember]
        [JsonProperty("FileName")]
        private string _FileName = string.Empty;
        public string FileName
        {
            get { return _FileName; }
            set { _FileName = value; }
        }

        [DataMember]
        [JsonProperty("WorkspaceId")]
        private int _WorkspaceId = 0;
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
        [JsonProperty("ArticleId")]
        private int _ArticleId = 0;
        public int ArticleId
        {
            get { return _ArticleId; }
            set { _ArticleId = value; }
        }

        [DataMember]
        [JsonProperty("IsActive")]
        private bool _IsActive = false;
        public bool IsActive
        {
            get { return _IsActive; }
            set { _IsActive = value; }
        }

        [DataMember]
        [JsonProperty("iDocXml")]
        private string _iDocXml = string.Empty;
        public string iDocXml
        {
            get { return _iDocXml; }
            set { _iDocXml = value; }
        }

        [DataMember]
        [JsonProperty("LfsNr")]
        private string _LfsNr = string.Empty;
        public string LfsNr
        {
            get { return _LfsNr; }
            set { _LfsNr = value; }
        }

        [DataMember]
        [JsonProperty("Produktionsnummer")]
        private string _Produktionsnummer = string.Empty;
        public string Produktionsnummer
        {
            get { return _Produktionsnummer; }
            set { _Produktionsnummer = value; }
        }

        [DataMember]
        [JsonProperty("Description")]
        private string _Description = string.Empty;
        public string Description
        {
            get { return _Description; }
            set { _Description = value; }
        }

        [DataMember]
        [JsonProperty("WorkspaceXmlRef")]
        private string _WorkspaceXmlRef = string.Empty;
        public string WorkspaceXmlRef
        {
            get { return _WorkspaceXmlRef; }
            set { _WorkspaceXmlRef = value; }
        }

        public EdiZQMQalityXml Copy()
        {
            return (EdiZQMQalityXml)this.MemberwiseClone();
        }

    }
}
