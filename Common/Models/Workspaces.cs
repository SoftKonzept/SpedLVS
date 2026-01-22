using Common.Enumerations;
using Common.Helper;
using Common.Models;
using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Drawing;

namespace Common.Models
{
    [Serializable]
    [DataContract]
    public class Workspaces
    {
        [DataMember]
        [JsonProperty("id")]
        public int Id { get; set; } = 0;

        [DataMember]
        [JsonProperty("name")]
        public string Name { get; set; } = string.Empty;

        [DataMember]
        [JsonProperty("descrition")]
        public string Descrition { get; set; } = string.Empty;

        [DataMember]
        [JsonProperty("isActiv")]
        public bool IsActiv { get; set; } = false;

        [DataMember]
        [JsonProperty("isLager")]
        public bool IsLager { get; set; } = false;

        [DataMember]
        [JsonProperty("isSpedition")]
        public bool IsSpedition { get; set; } = false;

        [DataMember]
        [JsonProperty("ASNTransfer")]
        public bool ASNTransfer { get; set; } = false;

        [DataMember]
        [JsonProperty("exist")]
        public bool Exist { get; set; } = false;

        [DataMember]
        [JsonProperty("mandantId")]
        public int MandantId { get; set; } = 0;

        [DataMember]
        [JsonProperty("mandant")]
        public Mandanten Mandant { get; set; }

        [DataMember]
        [JsonProperty("useAutoRowAssignment")]
        public bool UseAutoRowAssignment { get; set; } = false;

        [DataMember]
        [JsonProperty("maxCountArticleInStoreOut")]
        public int MaxCountArticleInStoreOut { get; set; } = 0;

        [DataMember]
        [JsonProperty("workspaceOwner")]
        public int WorkspaceOwner { get; set; } = 0;

        [DataMember]
        [JsonProperty("workspaceOwnerAddress")]
        public Addresses WorkspaceOwnerAddress { get; set; }

        [DataMember]
        [JsonProperty("AbrufDefEmpfaengerId")]
        public int AbrufDefEmpfaengerId { get; set; } = 0;
        [DataMember]
        [JsonProperty("EingangDefEmpfaengerId")]
        public int EingangDefEmpfaengerId { get; set; } = 0;
        [DataMember]
        [JsonProperty("EingangDefEntladeId")]
        public int EingangDefEntladeId { get; set; } = 0;
        [DataMember]
        [JsonProperty("EingangDefBeladeId")]
        public int EingangDefBeladeId { get; set; } = 0;
        [DataMember]
        [JsonProperty("AusgangDefEmpfaengerId")]
        public int AusgangDefEmpfaengerId { get; set; } = 0;
        [DataMember]
        [JsonProperty("AusgangDefVersenderId")]
        public int AusgangDefVersenderId { get; set; } = 0;
        [DataMember]
        [JsonProperty("AusgangDefEntladeId")]
        public int AusgangDefEntladeId { get; set; } = 0;
        [DataMember]
        [JsonProperty("AusgangDefBeladeId")]
        public int AusgangDefBeladeId { get; set; } = 0;

        [DataMember]
        [JsonProperty("UBDefEmpfaengerId")]
        public int UBDefEmpfaengerId { get; set; } = 0;
        [DataMember]
        [JsonProperty("UBDefAuftraggeberNeuId")]
        public int UBDefAuftraggeberNeuId { get; set; } = 0;



        public string WorkspaceString
        {
            get
            {
                string strRet = string.Empty;
                if (Id > 0)
                {
                    strRet += "[" + Id.ToString() + "] - " + Name;
                }
                return strRet;
            }
        }

        [JsonIgnore]
        public Color ViewBackgroundcolor
        {
            get
            {
                Color tmpBC = ValueToColorConverter.ViewBackgroundColorWorkspace_IdConvert(Id);
                return tmpBC;
            }
        }

        public Workspaces Copy()
        {
            return (Workspaces)this.MemberwiseClone();
        }
    }

}
