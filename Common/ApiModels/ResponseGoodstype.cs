using Common.Enumerations;
using Common.Models;
using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Common.ApiModels
{
    [Serializable]
    public class ResponseGoodstype
    {
        [DataMember]
        [JsonProperty("success")]
        public bool Success { get; set; } = false;

        [DataMember]
        [JsonProperty("Goodstype")]
        public Goodstypes Goodstype { get; set; }


        [DataMember]
        [JsonProperty("error")]
        public string Error { get; set; } = string.Empty;

        [DataMember]
        [JsonProperty("info")]
        public string Info { get; set; } = string.Empty;

        [DataMember]
        [JsonProperty("ListGoodstypes")]
        public List<Goodstypes> ListGoodstypes { get; set; } = new List<Goodstypes>();


        public ResponseGoodstype Copy()
        {
            return (ResponseGoodstype)this.MemberwiseClone();
        }

    }
}
