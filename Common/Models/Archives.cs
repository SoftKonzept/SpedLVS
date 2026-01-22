using Common.Enumerations;
using Common.Models;
using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Common.Models
{
    public class Archives
    {
        [DataMember]
        [JsonProperty("id")]
        public int Id { get; set; }

        [DataMember]
        [JsonProperty("TableName")]
        public string TableName { get; set; }

        [DataMember]
        [JsonProperty("TableId")]
        public int TableId { get; set; }

        [DataMember]
        [JsonProperty("FileArt")]
        public enumFileArt FileArt { get; set; }

        [DataMember]
        [JsonProperty("FileData")]
        public byte[] FileData { get; set; }

        [DataMember]
        [JsonProperty("Filename")]
        public string Filename { get; set; }

        [DataMember]
        [JsonProperty("Extension")]
        public string Extension { get; set; }

        [DataMember]
        [JsonProperty("Created")]
        public DateTime Created { get; set; }

        [DataMember]
        [JsonProperty("UserId")]
        public int UserId { get; set; }

        [DataMember]
        [JsonProperty("Description")]
        public string Description { get; set; }

        [DataMember]
        [JsonProperty("ReportDocSettingAssignmentId")]
        public int ReportDocSettingAssignmentId { get; set; }

        [DataMember]
        [JsonProperty("ReportDocSettingId")]
        public int ReportDocSettingId { get; set; }

        [DataMember]
        [JsonProperty("DocKey")]
        public string DocKey { get; set; }

        [DataMember]
        [JsonProperty("WorkspaceId")]
        public int WorkspaceId { get; set; }

        [DataMember]
        [JsonProperty("DocKeyID")]
        public int DocKeyID { get; set; }


        public Archives Copy()
        {
            return (Archives)this.MemberwiseClone();
        }
    }
}
