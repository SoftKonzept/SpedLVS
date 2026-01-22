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
    public class Mandanten
    {
        [DataMember]
        [JsonProperty("id")]
        public int Id { get; set; } = 0;

        [DataMember]
        [JsonProperty("AddressId")]
        public int AddressId { get; set; } = 0;

        [DataMember]
        [JsonProperty("Address")]
        public Addresses Address { get; set; }

        [DataMember]
        [JsonProperty("Description")]
        public string Description { get; set; } = string.Empty;

        [DataMember]
        [JsonProperty("isActiv")]
        public bool IsActiv { get; set; } = false;

        [DataMember]
        [JsonProperty("Matchcode")]
        public string Matchcode { get; set; } = string.Empty;

        [DataMember]
        [JsonProperty("IsDefaultSped")]
        public bool IsDefaultSped { get; set; } = false;

        [DataMember]
        [JsonProperty("IsDefaultStore")]
        public bool IsDefaultStore { get; set; } = false;

        [DataMember]
        [JsonProperty("VDA4905Verweis")]
        public string VDA4905Verweis { get; set; } = string.Empty;

        [DataMember]
        [JsonProperty("ReportPath")]
        public string ReportPath { get; set; } = string.Empty;

        [DataMember]
        [JsonProperty("Bank")]
        public string Bank { get; set; } = string.Empty;

        [DataMember]
        [JsonProperty("BLZ")]
        public string Blz { get; set; } = string.Empty;

        [DataMember]
        [JsonProperty("BIC")]
        public string Bic { get; set; } = string.Empty;

        [DataMember]
        [JsonProperty("Konto")]
        public string Konto { get; set; } = string.Empty;

        [DataMember]
        [JsonProperty("IBAN")]
        public string Iban { get; set; } = string.Empty;

        [DataMember]
        [JsonProperty("Contact")]
        public string Contact { get; set; } = string.Empty;

        [DataMember]
        [JsonProperty("Mail")]
        public string Mail { get; set; } = string.Empty;

        [DataMember]
        [JsonProperty("Homepage")]
        public string Homepage { get; set; } = string.Empty;

        [DataMember]
        [JsonProperty("Phone")]
        public string Phone { get; set; } = string.Empty;

        [DataMember]
        [JsonProperty("TaxNumber")]
        public string TaxNumber { get; set; } = string.Empty;

        [DataMember]
        [JsonProperty("VatId")]
        public string VatId { get; set; } = string.Empty;

        [DataMember]
        [JsonProperty("Organisation")]
        public string Organisation { get; set; } = string.Empty;

        public Mandanten Copy()
        {
            return (Mandanten)this.MemberwiseClone();
        }
    }

}
