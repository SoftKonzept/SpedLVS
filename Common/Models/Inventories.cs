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
    public class Inventories
    {
        [DataMember]
        [JsonProperty("id")]
        public int Id { get; set; } = 0;


        [DataMember]
        [JsonProperty("name")]
        public string Name { get; set; } = String.Empty;

        [DataMember]
        [JsonProperty("description")]
        public string Description { get; set; } = String.Empty;

        [DataMember]
        [JsonProperty("countarticle")]
        public int CountArticle { get; set; } = 0;

        [DataMember]
        [JsonProperty("art")]
        public enumInventoryArt Art { get; set; } = enumInventoryArt.NotSet;

        [DataMember]
        [JsonProperty("userId")]
        public int UserId { get; set; } = 0;

        [DataMember]
        [JsonProperty("status")]
        public enumInventoryStatus Status { get; set; } = enumInventoryStatus.NotSet;

        [DataMember]
        [JsonProperty("arbeitsbereichId")]
        public int ArbeitsbereichId { get; set; } = 0;

        [DataMember]
        [JsonProperty("created")]
        public DateTime Created { get; set; } = new DateTime(1900, 1, 1);

        [DataMember]
        [JsonProperty("closeDate")]
        public DateTime CloseDate { get; set; } = new DateTime(1900, 1, 1);

        [DataMember]
        [JsonProperty("closeUserId")]
        public int CloseUserId { get; set; } = 0;




        [DataMember]
        [JsonProperty("inventoryArticle")]
        public InventoryArticles InventoryArticle { get; set; } = new InventoryArticles();


        [DataMember]
        [JsonProperty("inventoryArticleList")]
        public List<InventoryArticles> InventoryArticleList = new List<InventoryArticles>();


        public Inventories Copy()
        {
            return (Inventories)this.MemberwiseClone();
        }
    }

}
