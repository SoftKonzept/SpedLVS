using Common.Enumerations;
using Common.Helper;
using Common.Models;
using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Common.Views
{
    public class DamageArticleAssignmentView
    {
        [DataMember]
        [JsonProperty("id")]
        public int Id { get; set; } = 0;

        [DataMember]
        [JsonProperty("ArticleId")]
        public int ArticleId { get; set; } = 0;

        [DataMember]
        [JsonProperty("DamageId")]
        public int DamageId { get; set; } = 0;

        [DataMember]
        [JsonProperty("UserId")]
        public int UserId { get; set; } = 0;

        [DataMember]
        [JsonProperty("Datum")]
        public DateTime Datum { get; set; } = new DateTime(1900, 1, 1);

        [DataMember]
        [JsonProperty("Damage")]
        public Damages Damage { get; set; } = new Damages();

        [DataMember]
        [JsonProperty("Article")]
        public Articles Article { get; set; } = new Articles();

        public DamageArticleAssignmentView Copy()
        {
            return (DamageArticleAssignmentView)this.MemberwiseClone();
        }
    }
}
