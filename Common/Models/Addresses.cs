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
    public class Addresses
    {
        [DataMember]
        [JsonProperty("id")]
        public int Id { get; set; }

        [DataMember]
        [JsonProperty("viewId")]
        public string ViewId { get; set; }

        [DataMember]
        [JsonProperty("kundenId")]
        public int KundenId { get; set; }

        [DataMember]
        [JsonProperty("fbez")]
        public string FBez { get; set; }

        [DataMember]
        [JsonProperty("Name1")]
        public string Name1 { get; set; }

        [DataMember]
        [JsonProperty("Name2")]
        public string Name2 { get; set; }

        [DataMember]
        [JsonProperty("Name3")]
        public string Name3 { get; set; }

        [DataMember]
        [JsonProperty("street")]
        public string Street { get; set; }

        [DataMember]
        [JsonProperty("HouseNo")]
        public string HouseNo { get; set; }

        [DataMember]
        [JsonProperty("Postcode")]
        public string Postcode { get; set; }

        [DataMember]
        [JsonProperty("POBox")]
        public string POBox { get; set; }

        [DataMember]
        [JsonProperty("ZIP")]
        public string ZIP { get; set; }

        [DataMember]
        [JsonProperty("city")]
        public string City { get; set; }

        [DataMember]
        [JsonProperty("Land")]
        public string Land { get; set; }

        [DataMember]
        [JsonProperty("LKZ")]
        public string LKZ { get; set; }

        [DataMember]
        [JsonProperty("activ")]
        public bool activ { get; set; }

        [DataMember]
        [JsonProperty("storenumber")]
        public int Storenumber { get; set; }

        [DataMember]
        [JsonProperty("ASNCom")]
        public bool ASNCom { get; set; }

        [DataMember]
        [JsonProperty("AdrId_Be")]
        public int AdrId_Be { get; set; }

        [DataMember]
        [JsonProperty("AdrId_Ent")]
        public int AdrId_Ent { get; set; }

        [DataMember]
        [JsonProperty("AdrId_Post")]
        public int AdrId_Post { get; set; }

        [DataMember]
        [JsonProperty("AdrId_RG")]
        public int AdrId_RG { get; set; }

        [DataMember]
        [JsonProperty("IsAuftraggeber")]
        public bool IsAuftraggeber { get; set; }

        [DataMember]
        [JsonProperty("IsVersender")]
        public bool IsVersender { get; set; }

        [DataMember]
        [JsonProperty("IsBelade")]
        public bool IsBelade { get; set; }

        [DataMember]
        [JsonProperty("IsEmpfaenger")]
        public bool IsEmpfaenger { get; set; }

        [DataMember]
        [JsonProperty("IsEntlade")]
        public bool IsEntlade { get; set; }

        [DataMember]
        [JsonProperty("DunsNr")]
        public int DunsNr { get; set; }

        public AddressCustomer CustomerData { get; set; } = new AddressCustomer();

        private string _AddressStringShort = string.Empty;
        public string AddressStringShort
        {
            get
            {
                string strReturn = Name1 + Environment.NewLine;
                strReturn += ZIP + " - " + City;
                return strReturn;
            }
        }

        private string _ViewIdString = string.Empty;
        public string ViewIdString
        {
            get
            {
                string strReturn = "[" + Id.ToString() + "] - " + ViewId;
                return strReturn;
            }
        }

        public Addresses Copy()
        {
            return (Addresses)this.MemberwiseClone();
        }
    }

}
