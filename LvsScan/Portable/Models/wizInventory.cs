using Common.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace LvsScan.Portable.Models
{
    public class wizInventory
    {
        [DataMember]
        [JsonProperty("Date")]
        public DateTime Date { get; set; }

        [DataMember]
        [JsonProperty("Fielname")]
        public string Fielname { get; set; } = string.Empty;

        [DataMember]
        [JsonProperty("Path")]
        public string Path { get; set; } = string.Empty;

        [DataMember]
        public Inventories SelectedInventory { get; set; } = new Inventories();

        [DataMember]
        public InventoryArticles InvnetoryArticle { get; set; } = new InventoryArticles();

        [DataMember]
        public List<InventoryArticles> InventoryArticlesList { get; set; } = new List<InventoryArticles>();

        [DataMember]
        public List<Inventories> InventoryList { get; set; } = new List<Inventories>();

        [DataMember]
        [JsonProperty("ErrorMessage")]
        public string ErrorMessage { get; set; } = string.Empty;



        //------------------ Step Input Search Value ------------------------
        [DataMember]
        [JsonProperty("SelectedInventoryArticle_InputSearch")]
        public InventoryArticles SelectedInventoryArticle_InputSearch { get; set; } = new InventoryArticles();

        [DataMember]
        [JsonProperty("SearchLvsNo")]
        public int SearchLvsNo { get; set; } = 0;

        [DataMember]
        [JsonProperty("SearchProduktionsnummer")]
        public string SearchProduktionsnummer { get; set; } = string.Empty;

        [DataMember]
        [JsonProperty("InventoryArticleList_InputSearchResult")]
        public List<InventoryArticles> InventoryArticleList_InputSearchResult { get; set; } = new List<InventoryArticles>();

        public bool StepInputSearchFinished { get; set; } = false;
        [DataMember]
        [JsonProperty("ErrorMessage")]
        public List<string> ErrorMessageList_InputSearch { get; set; } = new List<string>();



        //------------------ Step check and show result -------------------

        [DataMember]
        [JsonProperty("SelectedInventoryArticle_InputSearch")]
        public InventoryArticles SelectedInventoryArticle_ShowResult { get; set; } = new InventoryArticles();

        [DataMember]
        [JsonProperty("InventoryArticleStatus")]
        public Common.Enumerations.enumInventoryArticleStatus InventoryArticleStatus { get; set; }


        [DataMember]
        [JsonProperty("ScannedLagerOrt")]
        public string ScannedLagerOrt { get; set; } = string.Empty;

        public bool StepCheckShowFinished { get; set; } = false;


        //------------------ Step -------------------









        public wizInventory Copy()
        {
            return (wizInventory)this.MemberwiseClone();
        }
    }
}
