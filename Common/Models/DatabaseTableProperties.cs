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
    public class DatabaseTableProperties
    {

        [DataMember]
        [JsonProperty("tableName")]
        public string TableName { get; set; }

        [DataMember]
        [JsonProperty("fieldName")]
        public string FieldName { get; set; }

        [DataMember]
        [JsonProperty("value")]
        public string Value { get; set; }

        public DatabaseTableProperties Copy()
        {
            return (DatabaseTableProperties)this.MemberwiseClone();
        }
    }

}
