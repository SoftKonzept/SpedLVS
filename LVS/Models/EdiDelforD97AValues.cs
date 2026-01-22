using Common.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace LVS.Models
{
    [Serializable]
    [DataContract]
    public class EdiDelforD97AValues
    {
        [DataMember]
        [JsonProperty("Id")]
        private decimal _Id = 0;
        public decimal Id
        {
            get { return _Id; }
            set { _Id = value; }
        }

        [DataMember]
        [JsonProperty("DocumentDate")]
        private DateTime _DocumentDate = new DateTime(1900, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
        public DateTime DocumentDate
        {
            get { return _DocumentDate; }
            set { _DocumentDate = value; }
        }

        [DataMember]
        [JsonProperty("DocumentNo")]
        private int _DocumentNo = 0;
        public int DocumentNo
        {
            get { return _DocumentNo; }
            set { _DocumentNo = value; }
        }

        [DataMember]
        [JsonProperty("Position")]
        private decimal _Position = 0;
        public decimal Position
        {
            get { return _Position; }
            set { _Position = value; }
        }

        [DataMember]
        [JsonProperty("Client")]
        private int _Client = 0;
        public int Client
        {
            get { return _Client; }
            set { _Client = value; }
        }

        [DataMember]
        [JsonProperty("ClientAdr")]
        private Addresses _ClientAdr;
        public Addresses ClientAdr
        {
            get { return _ClientAdr; }
            set { _ClientAdr = value; }
        }

        [DataMember]
        [JsonProperty("Supplier")]
        private int _Supplier = 0;
        public int Supplier
        {
            get { return _Supplier; }
            set { _Supplier = value; }
        }

        [DataMember]
        [JsonProperty("SupplierAdr")]
        private Addresses _SupplierAdr;
        public Addresses SupplierAdr
        {
            get { return _SupplierAdr; }
            set { _SupplierAdr = value; }
        }

        [DataMember]
        [JsonProperty("Recipient")]
        private int _Recipient = 0;
        public int Recipient
        {
            get { return _Recipient; }
            set { _Recipient = value; }
        }

        [DataMember]
        [JsonProperty("RecipientAdr")]
        private Addresses _RecipientAdr;
        public Addresses RecipientAdr
        {
            get { return _RecipientAdr; }
            set { _RecipientAdr = value; }
        }

        [DataMember]
        [JsonProperty("RecipientAdr")]
        private string _RecipientAdrMatchCode;
        public string RecipientAdrMatchCode
        {
            get
            {
                _RecipientAdrMatchCode = string.Empty;
                if ((RecipientAdr != null) && (RecipientAdr.Id > 0))
                {
                    _RecipientAdrMatchCode = RecipientAdr.ViewId.ToString();
                }
                return _RecipientAdrMatchCode;
            }
        }

        [DataMember]
        [JsonProperty("Werksnummer")]
        private string _Werksnummer = string.Empty;
        public string Werksnummer
        {
            get { return _Werksnummer; }
            set { _Werksnummer = value; }
        }

        [DataMember]
        [JsonProperty("OrderNo")]
        private string _OrderNo = string.Empty;
        public string OrderNo
        {
            get { return _OrderNo; }
            set { _OrderNo = value; }
        }

        [DataMember]
        [JsonProperty("DeliveryScheduleNumber")]
        private int _DeliveryScheduleNumber = 0;
        public int DeliveryScheduleNumber
        {
            get { return _DeliveryScheduleNumber; }
            set { _DeliveryScheduleNumber = value; }
        }

        [DataMember]
        [JsonProperty("CumQuantityReceived")]
        private int _CumQuantityReceived = 0;
        public int CumQuantityReceived
        {
            get { return _CumQuantityReceived; }
            set { _CumQuantityReceived = value; }
        }

        [DataMember]
        [JsonProperty("CumQuantityStartDate")]
        private DateTime _CumQuantityStartDate = new DateTime(1900, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
        public DateTime CumQuantityStartDate
        {
            get { return _CumQuantityStartDate; }
            set { _CumQuantityStartDate = value; }
        }

        [DataMember]
        [JsonProperty("ReceivedQuantity")]
        private int _ReceivedQuantity = 0;
        public int ReceivedQuantity
        {
            get { return _ReceivedQuantity; }
            set { _ReceivedQuantity = value; }
        }

        [DataMember]
        [JsonProperty("SID")]
        private string _SID = string.Empty;
        public string SID
        {
            get { return _SID; }
            set { _SID = value; }
        }

        [DataMember]
        [JsonProperty("GoodReceiptDate")]
        private DateTime _GoodReceiptDate = new DateTime(1900, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
        public DateTime GoodReceiptDate
        {
            get { return _GoodReceiptDate; }
            set { _GoodReceiptDate = value; }
        }

        [DataMember]
        [JsonProperty("SchedulingConditions")]
        private int _SchedulingConditions = 0;
        public int SchedulingConditions
        {
            get { return _SchedulingConditions; }
            set { _SchedulingConditions = value; }
        }

        [DataMember]
        [JsonProperty("CallQuantity")]
        private int _CallQuantity;
        public int CallQuantity
        {
            get { return _CallQuantity; }
            set { _CallQuantity = value; }
        }

        [DataMember]
        [JsonProperty("DeliveryDate")]
        private DateTime _DeliveryDate;
        public DateTime DeliveryDate
        {
            get { return _DeliveryDate; }
            set { _DeliveryDate = value; }
        }

        [DataMember]
        [JsonProperty("IsActive")]
        private bool _IsActive = true;
        public bool IsActive
        {
            get { return _IsActive; }
            set { _IsActive = value; }
        }

        [DataMember]
        [JsonProperty("WorkspaceId")]
        private int _WorkspaceId = 0;
        public int WorkspaceId
        {
            get { return _WorkspaceId; }
            set { _WorkspaceId = value; }
        }

        [DataMember]
        [JsonProperty("Workspace")]
        private Workspaces _Workspace;
        public Workspaces Workspace
        {
            get { return _Workspace; }
            set { _Workspace = value; }
        }

        [DataMember]
        [JsonProperty("Description")]
        private string _Description;
        public string Description
        {
            get { return _Description; }
            set { _Description = value; }
        }


        [DataMember]
        [JsonProperty("DictDelivered")]
        private Dictionary<Ausgaenge, List<Articles>> _DictDelivered;
        public Dictionary<Ausgaenge, List<Articles>> DictDelivered
        {
            get { return _DictDelivered; }
            set { _DictDelivered = value; }
        }



        public EdiDelforD97AValues Copy()
        {
            return (EdiDelforD97AValues)this.MemberwiseClone();
        }
    }
}
