using Common.Enumerations;
using Common.Helper;
using Common.Models;
using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Common.Models
{
    [Serializable]
    [DataContract]
    public class PrintQueues
    {
        [DataMember]
        [JsonProperty("id")]
        private int _Id = 0;
        public int Id
        {
            get { return _Id; }
            set { _Id = value; }
        }

        [DataMember]
        [JsonProperty("IsActiv")]
        private bool _IsActiv = false;
        public bool IsActiv
        {
            get { return _IsActiv; }
            set { _IsActiv = value; }
        }

        [DataMember]
        [JsonProperty("ReportDocSettingId")]
        private int _ReportDocSettingId = 0;
        public int ReportDocSettingId
        {
            get { return _ReportDocSettingId; }
            set { _ReportDocSettingId = value; }
        }

        [DataMember]
        [JsonProperty("ReportDocSettingAssignmentId")]
        private int _ReportDocSettingAssignmentId = 0;
        public int ReportDocSettingAssignmentId
        {
            get { return _ReportDocSettingAssignmentId; }
            set { _ReportDocSettingAssignmentId = value; }
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
        [JsonProperty("TableName")]
        private string _TableName;
        public string TableName
        {
            get { return _TableName; }
            set { _TableName = value; }
        }

        [DataMember]
        [JsonProperty("TableId")]
        private int _TableId;
        public int TableId
        {
            get { return _TableId; }
            set { _TableId = value; }
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
        [JsonProperty("PrintCount")]
        private int _PrintCount;
        public int PrintCount
        {
            get { return _PrintCount; }
            set { _PrintCount = value; }
        }
        [DataMember]
        [JsonProperty("PrinterName")]
        private string _PrinterName;
        public string PrinterName
        {
            get { return _PrinterName; }
            set { _PrinterName = value; }
        }


        public PrintQueues Copy()
        {
            return (PrintQueues)this.MemberwiseClone();
        }
    }
}
