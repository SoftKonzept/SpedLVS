using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;

namespace LVS.Models
{
    [Serializable]
    [DataContract]
    public class ArticleVita
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
        [JsonProperty("TableId")]
        private int _TableId = 0;
        public int TableId
        {
            get { return _TableId; }
            set { _TableId = value; }
        }

        [DataMember]
        [JsonProperty("TableName")]
        private string _TableName = string.Empty;
        public string TableName
        {
            get { return _TableName; }
            set { _TableName = value; }
        }

        [DataMember]
        [JsonProperty("Action")]
        private string _Action = string.Empty;
        public string Action
        {
            get { return _Action; }
            set { _Action = value; }
        }

        [DataMember]
        [JsonProperty("Date")]
        private DateTime _Date = new DateTime(1900, 1, 1);
        public DateTime Date
        {
            get { return _Date; }
            set { _Date = value; }
        }

        [DataMember]
        [JsonProperty("UserId")]
        private int _UserId = 0;
        public int UserId
        {
            get { return _UserId; }
            set { _UserId = value; }
        }

        [DataMember]
        [JsonProperty("Description")]
        private string _Description = string.Empty;
        public string Description
        {
            get { return _Description; }
            set { _Description = value; }
        }
    }
}
