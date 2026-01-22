using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;

namespace LVS.Models
{
    [Serializable]
    [DataContract]
    public class InvoiceItems
    {
        [DataMember]
        [JsonProperty("id")]
        public int Id { get; set; }

        [DataMember]
        [JsonProperty("InvoiceId")]
        public int InvoiceId { get; set; }

        [DataMember]
        [JsonProperty("Position")]
        public int Position { get; set; }

        [DataMember]
        [JsonProperty("RGText")]
        public string RGText { get; set; }

        [DataMember]
        [JsonProperty("BillingUnit")]
        // - Abrechnungseinheit
        public string BillingUnit { get; set; }

        [DataMember]
        [JsonProperty("Qunatity")]
        // - Menge
        public decimal Qunatity { get; set; }

        [DataMember]
        [JsonProperty("UnitPrice")]
        // - Einzelpreis
        public decimal UnitPrice { get; set; }

        [DataMember]
        [JsonProperty("NetAmount")]
        // - Netto Rechnungsposition
        public decimal NetAmount { get; set; }

        [DataMember]
        [JsonProperty("BillingType")]
        // - Abrechnungsart
        public string BillingType { get; set; }

        [DataMember]
        [JsonProperty("TarifPosId")]
        // - TarifPosId
        public int TarifPosId { get; set; }

        [DataMember]
        [JsonProperty("TarifText")]
        // - TarifPosId
        public string TarifText { get; set; }

        [DataMember]
        [JsonProperty("MarginEuro")]
        // - Marge
        public decimal MarginEuro { get; set; }

        [DataMember]
        [JsonProperty("MarginRate")]
        // - Marge
        public decimal MarginRate { get; set; }

        [DataMember]
        [JsonProperty("InventoryStart")]
        // - Anfangsbestand
        public decimal InventoryStart { get; set; }

        [DataMember]
        [JsonProperty("InventoryOutgoing")]
        // - Abgang
        public decimal InventoryOutgoing { get; set; }

        [DataMember]
        [JsonProperty("InventoryAccess")]
        // - Zugang
        public decimal InventoryAccess { get; set; }

        [DataMember]
        [JsonProperty("InventoryEnd")]
        // - Ende
        public decimal InventoryEnd { get; set; }

        [DataMember]
        [JsonProperty("InvoiceItemText")]
        // - RGPos Text
        public string InvoiceItemText { get; set; }

        [DataMember]
        [JsonProperty("FibuAccount")]
        public int FibuAccount { get; set; }

        [DataMember]
        [JsonProperty("CalcModus")]
        public enumCalcultationModus CalcModus { get; set; }

        [DataMember]
        [JsonProperty("CalcModValue")]
        public int CalcModValue { get; set; }

        //--- PricePerUnitFactor
        [DataMember]
        [JsonProperty("PricePerUnitFactor")]
        // - Ende
        public decimal PricePerUnitFactor { get; set; }

        public InvoiceItems Copy()
        {
            return (InvoiceItems)this.MemberwiseClone();
        }
    }

}
