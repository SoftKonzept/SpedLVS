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
    public class AddressReferences
    {
        [DataMember]
        [JsonProperty("id")]
        public int Id { get; set; } = 0;

        [DataMember]
        [JsonProperty("SenderAddress")]
        public Addresses SenderAddress { get; set; } = new Addresses();

        [DataMember]
        [JsonProperty("SenderAdrId")]
        public int SenderAdrId { get; set; } = 0;

        [DataMember]
        [JsonProperty("ReceiverAddress")]
        public Addresses ReceiverAddress { get; set; } = new Addresses();

        [DataMember]
        [JsonProperty("VerweisAdrId")]
        public int VerweisAdrId { get; set; } = 0;

        [DataMember]
        [JsonProperty("MandantenId")]
        public int MandantenId { get; set; } = 0;

        [DataMember]
        [JsonProperty("WorkspaceId")]
        public int WorkspaceId { get; set; } = 0;

        [DataMember]
        [JsonProperty("SupplierReference")]
        public string SupplierReference { get; set; } = string.Empty;
        [DataMember]
        [JsonProperty("SenderReference")]
        public string SenderReference { get; set; } = string.Empty;

        [DataMember]
        [JsonProperty("ReferenceArt")]
        public string ReferenceArt { get; set; } = string.Empty;

        [DataMember]
        [JsonProperty("SupplierId")]
        public int SupplierId { get; set; } = 0;

        [DataMember]
        [JsonProperty("SupplierNo")]
        public string SupplierNo { get; set; } = string.Empty;

        [DataMember]
        [JsonProperty("Reference")]
        public string Reference { get; set; } = string.Empty;

        [DataMember]
        [JsonProperty("ASNFileTyp")]
        public string ASNFileTyp { get; set; } = string.Empty;

        [DataMember]
        [JsonProperty("IsActive")]
        public bool IsActive { get; set; } = false;

        [DataMember]
        [JsonProperty("Remark")]
        public string Remark { get; set; } = string.Empty;

        [DataMember]
        [JsonProperty("UseS712F04")]
        public bool UseS712F04 { get; set; } = false;

        [DataMember]
        [JsonProperty("UseS713F13")]
        public bool UseS713F13 { get; set; } = false;

        [DataMember]
        [JsonProperty("Description")]
        public string Description { get; set; } = string.Empty;

        [DataMember]
        [JsonProperty("ReferencePart1")]
        public string ReferencePart1 { get; set; } = string.Empty;

        [DataMember]
        [JsonProperty("ReferencePart2")]
        public string ReferencePart2 { get; set; } = string.Empty;

        [DataMember]
        [JsonProperty("ReferencePart3")]
        public string ReferencePart3 { get; set; } = string.Empty;
        public AddressReferences Copy()
        {
            return (AddressReferences)this.MemberwiseClone();
        }
    }

}
