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
    public class AddressCustomer
    {
        [DataMember]
        [JsonProperty("id")]
        public int Id { get; set; } = 0;

        [DataMember]
        [JsonProperty("KD_ID")]
        public int KD_ID { get; set; } = 0;

        [DataMember]
        [JsonProperty("AdrId")]
        public int AdrId { get; set; } = 0;

        [DataMember]
        [JsonProperty("SteuerNr")]
        public string SteuerNr { get; set; } = string.Empty;

        [DataMember]
        [JsonProperty("UstId")]
        public string UstId { get; set; } = string.Empty;

        [DataMember]
        [JsonProperty("MwSt")]
        public bool MwSt { get; set; } = false;

        [DataMember]
        [JsonProperty("MwStSatz")]
        public decimal MwStSatz { get; set; } = 0M;

        [DataMember]
        [JsonProperty("Bank1")]
        public string Bank1 { get; set; } = string.Empty;

        [DataMember]
        [JsonProperty("BLZ1")]
        public int BLZ1 { get; set; } = 0;

        [DataMember]
        [JsonProperty("Kto1")]
        public string Kto1 { get; set; } = string.Empty;

        [DataMember]
        [JsonProperty("Swift1")]
        public string Swift1 { get; set; } = string.Empty;

        [DataMember]
        [JsonProperty("IBAN1")]
        public string IBAN1 { get; set; } = string.Empty;

        [DataMember]
        [JsonProperty("Bank2")]
        public string Bank2 { get; set; } = string.Empty;

        [DataMember]
        [JsonProperty("Kto2")]
        public string Kto2 { get; set; } = string.Empty;

        [DataMember]
        [JsonProperty("BLZ2")]
        public int BLZ2 { get; set; } = 0;

        [DataMember]
        [JsonProperty("Swift2")]
        public string Swift2 { get; set; } = string.Empty;

        [DataMember]
        [JsonProperty("IBAN2")]
        public string IBAN2 { get; set; } = string.Empty;

        [DataMember]
        [JsonProperty("Debitor")]
        public int Debitor { get; set; } = 0;

        [DataMember]
        [JsonProperty("Kreditor")]
        public int Kreditor { get; set; } = 0;


        [DataMember]
        [JsonProperty("Zahlungziel")]
        public Int32 Zahlungziel { get; set; } = 0;

        [DataMember]
        [JsonProperty("KD_IDman")]
        public int KD_IDman { get; set; } = 0;

        [DataMember]
        [JsonProperty("SalesTaxKeyDebitor")]
        public Int32 SalesTaxKeyDebitor { get; set; } = 0;

        [DataMember]
        [JsonProperty("SalesTaxKeyKreditor")]
        public Int32 SalesTaxKeyKreditor { get; set; } = 0;

        [DataMember]
        [JsonProperty("Contact")]
        public string Contact { get; set; } = string.Empty;

        [DataMember]
        [JsonProperty("Phone")]
        public string Phone { get; set; } = string.Empty;

        [DataMember]
        [JsonProperty("Mailaddress")]
        public string Mailaddress { get; set; } = string.Empty;

        [DataMember]
        [JsonProperty("Organisation")]
        public string Organisation { get; set; } = string.Empty;

        public AddressCustomer Copy()
        {
            return (AddressCustomer)this.MemberwiseClone();
        }
    }

}
