using Common.Models;
using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;

namespace LVS.Models
{
    [Serializable]
    [DataContract]
    public class Invoices
    {
        [DataMember]
        [JsonProperty("id")]
        public int Id { get; set; }

        [DataMember]
        [JsonProperty("InvoiceNo")]
        public int InvoiceNo { get; set; }

        [DataMember]
        [JsonProperty("Datum")]
        public DateTime Datum { get; set; }

        [DataMember]
        [JsonProperty("DueDate")]
        public DateTime DueDate { get; set; }

        [DataMember]
        [JsonProperty("Vat")]
        public decimal Vat { get; set; }

        [DataMember]
        [JsonProperty("VatRate")]
        public decimal VatRate { get; set; }

        [DataMember]
        [JsonProperty("NetAmount")]
        public decimal NetAmount { get; set; }

        [DataMember]
        [JsonProperty("GrossAmount")]
        public decimal GrossAmount { get; set; }

        [DataMember]
        [JsonProperty("IsCancelation")]
        public bool IsCancelation { get; set; }

        [DataMember]
        [JsonProperty("IsInvoice")]
        public bool IsInvoice { get; set; }

        [DataMember]
        [JsonProperty("Paid")]
        public DateTime Paid { get; set; }

        [DataMember]
        [JsonProperty("IsPrinted")]
        public bool IsPrinted { get; set; }

        [DataMember]
        [JsonProperty("PrintDate")]
        public DateTime PrintDate { get; set; }

        [DataMember]
        [JsonProperty("UserId")]
        public int UserId { get; set; }

        [DataMember]
        [JsonProperty("InvoiceType")]
        public string InvoiceType { get; set; }

        [DataMember]
        [JsonProperty("ClientId")]
        public int ClientId { get; set; }

        [DataMember]
        [JsonProperty("Client")]
        public Mandanten Client { get; set; }


        [DataMember]
        [JsonProperty("WorkspaceId")]
        public int WorkspaceId { get; set; }

        [DataMember]
        [JsonProperty("Workspace")]
        public Workspaces Workspace { get; set; }

        [DataMember]
        [JsonProperty("ExFibu")]
        public bool ExFibu { get; set; }

        [DataMember]
        [JsonProperty("BillingPeriodStart")]
        public DateTime BillingPeriodStart { get; set; }

        [DataMember]
        [JsonProperty("BillingPeriodEnd")]
        public DateTime BillingPeriodEnd { get; set; }

        [DataMember]
        [JsonProperty("Receiver")]
        public int Receiver { get; set; }

        [DataMember]
        [JsonProperty("AdrReceiver")]
        public Addresses AdrReceiver { get; set; }

        [DataMember]
        [JsonProperty("StornoId")]
        public int StornoId { get; set; }

        [DataMember]
        [JsonProperty("TarifName")]
        public string TarifName { get; set; }

        [DataMember]
        [JsonProperty("InsuranceRate")]
        public decimal InsuranceRate { get; set; }

        [DataMember]
        [JsonProperty("InvoiceBookPrintDate")]
        public DateTime InvoiceBookPrintDate { get; set; }

        [DataMember]
        [JsonProperty("InfoText")]
        public string InfoText { get; set; }

        [DataMember]
        [JsonProperty("FibuInfo")]
        public string FibuInfo { get; set; }

        [DataMember]
        [JsonProperty("DocName")]
        public string DocName { get; set; }



        public Invoices Copy()
        {
            return (Invoices)this.MemberwiseClone();
        }
    }

}
