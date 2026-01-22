using Common.Barcodes;
using Common.Enumerations;
using Common.Models;
using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Common.Models
{
    public class ArticleSearch
    {
        [DataMember]
        [JsonProperty("lvsNoSearch")]
        public string LvsNoSearch { get; set; } = string.Empty;

        [DataMember]
        [JsonProperty("existLvsNoSearchValue")]
        public bool ExistLvsNoSearchValue { get; set; } = false;

        [DataMember]
        [JsonProperty("productionsNoSearch")]
        public string ProductionsNoSearch { get; set; } = string.Empty;

        [DataMember]
        [JsonProperty("existProductionsNoSearchValue")]
        public bool ExistProductionsNoSearchValue { get; set; } = false;

        [DataMember]
        [JsonProperty("errorText")]
        public string ErrorText { get; set; } = string.Empty;

        [DataMember]
        [JsonProperty("articleSearcheResult")]
        public Articles ArticleSearcheResult { get; set; }

        [DataMember]
        [JsonProperty("IsRebookedArticle")]
        public bool IsRebookedArticle { get; set; } = false;

        [DataMember]
        [JsonProperty("ListArticles")]
        public List<Articles> ListArticles { get; set; } = new List<Articles>();

        [DataMember]
        [JsonProperty("currentSearchValueDatafield")]
        public Enumerations.enumArticle_Datafields CurrentSearchValueDatafield { get; set; } = Enumerations.enumArticle_Datafields.NotSet;


        public string BarcodeProductionNo
        {
            get
            {
                string strTmp = string.Empty;
                //if (!Article_ProductionNo.Equals(String.Empty))
                //{
                //    Code39 c39 = new Code39(Article_ProductionNo);
                //    strTmp = c39.BarcodeCode39WihtoutCheckDigit;
                //}
                if (ArticleSearcheResult is Articles)
                {
                    Code39 c39 = new Code39(ArticleSearcheResult.Produktionsnummer);
                    strTmp = c39.BarcodeCode39WihtoutCheckDigit;
                }
                return strTmp;
            }
        }
        public string BarcodeProductionWithouthDigit
        {
            get
            {
                string strTmp = string.Empty;
                //if (!Article_ProductionNo.Equals(String.Empty))
                //{
                //    Code39 c39 = new Code39(Article_ProductionNo);
                //    strTmp = c39.BarcodeCode39WihtoutCheckDigit;
                //}
                if (ArticleSearcheResult is Articles)
                {
                    Code39 c39 = new Code39(ArticleSearcheResult.Produktionsnummer);
                    strTmp = c39.BarcodeCode39WihtCheckDigit;
                }
                return strTmp;
            }
        }

        public ArticleSearch Copy()
        {
            return (ArticleSearch)this.MemberwiseClone();
        }
    }
}
