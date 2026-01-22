using Common.Enumerations;
using Common.Helper;
using Common.Models;
using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Common.Models
{
    public class Images
    {
        [DataMember]
        [JsonProperty("id")]
        public int Id { get; set; }

        [DataMember]
        [JsonProperty("AuftragTableID")]
        public int AuftragTableID { get; set; }

        [DataMember]
        [JsonProperty("LEingangTableID")]
        public int LEingangTableID { get; set; }

        [DataMember]
        [JsonProperty("LAusgangTableID")]
        public int LAusgangTableID { get; set; }

        [DataMember]
        [JsonProperty("Pfad")]
        public string Pfad { get; set; }

        [DataMember]
        [JsonProperty("ScanFilename")]
        public string ScanFilename { get; set; }

        [DataMember]
        [JsonProperty("PicNum")]
        public int PicNum { get; set; }

        [DataMember]
        [JsonProperty("TableName")]
        public string TableName { get; set; }

        [DataMember]
        [JsonProperty("TableId")]
        public int TableId { get; set; }

        [DataMember]
        [JsonProperty("ImageArt")]
        public string ImageArt { get; set; }

        [DataMember]
        [JsonProperty("AuftragPosTableID")]
        public int AuftragPosTableID { get; set; }

        [DataMember]
        [JsonProperty("DocImage")]
        public byte[] DocImage { get; set; }

        [DataMember]
        [JsonProperty("Thumbnail")]
        public byte[] Thumbnail { get; set; }

        [DataMember]
        [JsonProperty("IsForSPLMessage")]
        public bool IsForSPLMessage { get; set; }

        [DataMember]
        [JsonProperty("Created")]
        public DateTime Created { get; set; }

        [DataMember]
        [JsonProperty("WorkspaceId")]
        public int WorkspaceId { get; set; }

        public Images Copy()
        {
            return (Images)this.MemberwiseClone();
        }
    }
}
