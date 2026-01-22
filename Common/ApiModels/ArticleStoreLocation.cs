using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;

namespace Common.ApiModels
{
    [Serializable]
    [DataContract]
    public class ArticleStoreLocation
    {
        [DataMember]
        [JsonProperty("articleId")]
        private int _ArticleId;
        public int ArticleId
        {
            get { return _ArticleId; }
            set { _ArticleId = value; }
        }

        [DataMember]
        [JsonProperty("lVS_ID")]
        private int _LVS_ID;
        public int LVS_ID
        {
            get { return _LVS_ID; }
            set { _LVS_ID = value; }
        }

        [DataMember]
        [JsonProperty("lagerOrt")]
        public int LagerOrt { get; set; }

        [DataMember]
        [JsonProperty("lagerOrtTable")]
        public string LagerOrtTable { get; set; }



        [DataMember]
        [JsonProperty("werk")]
        public string Werk { get; set; }

        [DataMember]
        [JsonProperty("halle")]
        public string Halle { get; set; }

        [DataMember]
        [JsonProperty("reihe")]
        public string Reihe { get; set; }

        [DataMember]
        [JsonProperty("ebene")]
        public string Ebene { get; set; }

        [DataMember]
        [JsonProperty("platz")]
        public string Platz { get; set; }


        public ArticleStoreLocation Copy()
        {
            return (ArticleStoreLocation)MemberwiseClone();
        }
    }

}
