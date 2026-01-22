using Common.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace LVS.Models
{
    [Serializable]
    [DataContract]
    public class _EdiQalityD96A
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
        [JsonProperty("iDocNo")]
        private string _iDocNo;
        public string iDocNo
        {
            get { return _iDocNo; }
            set { _iDocNo = value; }
        }

        [DataMember]
        [JsonProperty("iDocDate")]
        private DateTime _iDocDate;
        public DateTime iDocDate
        {
            get { return _iDocDate; }
            set { _iDocDate = value; }
        }

        [DataMember]
        [JsonProperty("Path")]
        private string _Path;
        public string Path
        {
            get { return _Path; }
            set { _Path = value; }
        }

        [DataMember]
        [JsonProperty("FileName")]
        private string _FileName;
        public string FileName
        {
            get { return _FileName; }
            set { _FileName = value; }
        }

        [DataMember]
        [JsonProperty("Datum")]
        private DateTime _Datum;
        public DateTime Datum
        {
            get { return _Datum; }
            set { _Datum = value; }
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
        [JsonProperty("ArticleId")]
        private int _ArticleId;
        public int ArticleId
        {
            get { return _ArticleId; }
            set { _ArticleId = value; }
        }

        [DataMember]
        [JsonProperty("IsActive")]
        private bool _IsActive;
        public bool IsActive
        {
            get { return _IsActive; }
            set { _IsActive = value; }
        }


        private List<_EdiQalityD96AValue> _ListQalityD96AValue = new List<_EdiQalityD96AValue>();
        public List<_EdiQalityD96AValue> ListQalityD96AValue
        {
            get { return _ListQalityD96AValue; }
            set { _ListQalityD96AValue = value; }
        }

        public _EdiQalityD96A Copy()
        {
            return (_EdiQalityD96A)this.MemberwiseClone();
        }

    }
}
