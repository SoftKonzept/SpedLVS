using Common.Models;
using LVS.Models;
using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;

namespace LVS.Views
{
    [Serializable]
    [DataContract]
    public class ctrReihe_ReiheGutAssignmentView
    {
        public ctrReihe_ReiheGutAssignmentView(int myId, Reihe myReihe, Goodstypes myGut)
        {
            this.Id = myId;
            this.Reihe = myReihe ?? new Reihe();
            this.Gut = myGut ?? new Goodstypes();
        }

        internal Reihe Reihe { get; set; }
        internal Goodstypes Gut { get; set; }

        [DataMember]
        [JsonProperty("Id")]
        public int Id { get; set; } = 0;

        [DataMember]
        [JsonProperty("ReiheId")]
        public int ReiheId
        {
            get
            {
                return Reihe?.Id ?? 0;
            }
        }

        [DataMember]
        [JsonProperty("Reihen-Bezeichnung")]
        public string ReihenBezeichnung
        {
            get
            {
                return Reihe?.Bezeichnung ?? string.Empty;
            }
        }

        [DataMember]
        [JsonProperty("Reihe-OrderId")]
        public int ReiheOrderId
        {
            get
            {
                return Reihe?.OrderId ?? 0;
            }
        }

        [DataMember]
        [JsonProperty("GArtId")]
        public int GArtId
        {
            get
            {
                return Gut?.Id ?? 0;
            }
        }

        [DataMember]
        [JsonProperty("Matchcode")]
        public string Matchcode
        {
            get
            {
                return Gut?.ViewID ?? string.Empty;
            }
        }

        [DataMember]
        [JsonProperty("Güterart")]
        public string Güterart
        {
            get
            {
                return Gut?.Bezeichnung ?? string.Empty;
            }
        }

        [DataMember]
        [JsonProperty("HalleId")]
        public int HalleId
        {
            get
            {
                return Reihe?.HalleId ?? 0;
            }
        }
    }
}
