using Common.Enumerations;
using Common.Models;
using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Common.Models
{
    [Serializable]
    [DataContract]
    public class DatabaseManipulations
    {

        [DataMember]
        [JsonProperty("tableName")]
        public string TableName { get; set; } = string.Empty;
        [DataMember]
        [JsonProperty("tableId")]
        public int TableId { get; set; } = 0;

        [DataMember]
        [JsonProperty("action")]
        public enumDatabaseAction Action { get; set; } = enumDatabaseAction.NotSet;

        [DataMember]
        [JsonProperty("actionList")]
        public List<DatabaseTableProperties> ActionList { get; set; } = new List<DatabaseTableProperties>();





        public DatabaseManipulations Copy()
        {
            return (DatabaseManipulations)this.MemberwiseClone();
        }
    }

}
