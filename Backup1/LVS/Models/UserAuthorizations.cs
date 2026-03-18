using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;

namespace LVS.Models
{
    [Serializable]
    [DataContract]

    public class UserAuthorizations
    {
        [DataMember]
        [JsonProperty("Id")]
        public decimal Id { get; set; }

        [DataMember]
        [JsonProperty("UserID")]
        public decimal UserID { get; set; }

        public Users User { get; set; }

        [DataMember]
        [JsonProperty("read_ADR")]
        public bool read_ADR { get; set; }

        [DataMember]
        [JsonProperty("write_ADR")]
        public bool write_ADR { get; set; }

        [DataMember]
        [JsonProperty("read_Kunde")]
        public bool read_Kunde { get; set; }

        [DataMember]
        [JsonProperty("write_Kunde")]
        public bool write_Kunde { get; set; }

        [DataMember]
        [JsonProperty("read_Personal")]
        public bool read_Personal { get; set; }

        [DataMember]
        [JsonProperty("write_Personal")]
        public bool write_Personal { get; set; }

        [DataMember]
        [JsonProperty("read_KFZ")]
        public bool read_KFZ { get; set; }

        [DataMember]
        [JsonProperty("write_KFZ")]
        public bool write_KFZ { get; set; }

        [DataMember]
        [JsonProperty("read_Gut")]
        public bool read_Gut { get; set; }

        [DataMember]
        [JsonProperty("write_Gut")]
        public bool write_Gut { get; set; }

        [DataMember]
        [JsonProperty("read_Relation")]
        public bool read_Relation { get; set; }

        [DataMember]
        [JsonProperty("write_Relation")]
        public bool write_Relation { get; set; }

        [DataMember]
        [JsonProperty("read_Order")]
        public bool read_Order { get; set; }

        [DataMember]
        [JsonProperty("write_Order")]
        public bool write_Order { get; set; }

        [DataMember]
        [JsonProperty("read_TransportOrder")]
        public bool read_TransportOrder { get; set; }

        [DataMember]
        [JsonProperty("write_TransportOrder")]
        public bool write_TransportOrder { get; set; }

        [DataMember]
        [JsonProperty("read_Disposition")]
        public bool read_Disposition { get; set; }

        [DataMember]
        [JsonProperty("write_Disposition")]
        public bool write_Disposition { get; set; }

        [DataMember]
        [JsonProperty("read_FaktLager")]
        public bool read_FaktLager { get; set; }

        [DataMember]
        [JsonProperty("write_FaktLager")]
        public bool write_FaktLager { get; set; }

        [DataMember]
        [JsonProperty("read_FaktSpedition")]
        public bool read_FaktSpedition { get; set; }

        [DataMember]
        [JsonProperty("write_FaktSpedition")]
        public bool write_FaktSpedition { get; set; }

        [DataMember]
        [JsonProperty("read_Bestand")]
        public bool read_Bestand { get; set; }

        [DataMember]
        [JsonProperty("read_LagerEingang")]
        public bool read_LagerEingang { get; set; }

        [DataMember]
        [JsonProperty("write_LagerEingang")]
        public bool write_LagerEingang { get; set; }

        [DataMember]
        [JsonProperty("read_LagerAusgang")]
        public bool read_LagerAusgang { get; set; }

        [DataMember]
        [JsonProperty("write_LagerAusgang")]
        public bool write_LagerAusgang { get; set; }

        [DataMember]
        [JsonProperty("read_User")]
        public bool read_User { get; set; }

        [DataMember]
        [JsonProperty("write_User")]
        public bool write_User { get; set; }

        [DataMember]
        [JsonProperty("read_Arbeitsbereich")]
        public bool read_Arbeitsbereich { get; set; }

        [DataMember]
        [JsonProperty("write_Arbeitsbereich")]
        public bool write_Arbeitsbereich { get; set; }

        [DataMember]
        [JsonProperty("read_Mandant")]
        public bool read_Mandant { get; set; }

        [DataMember]
        [JsonProperty("write_Mandant")]
        public bool write_Mandant { get; set; }

        [DataMember]
        [JsonProperty("read_Statistik")]
        public bool read_Statistik { get; set; }

        [DataMember]
        [JsonProperty("read_Einheit")]
        public bool read_Einheit { get; set; }

        [DataMember]
        [JsonProperty("write_Einheit")]
        public bool write_Einheit { get; set; }

        [DataMember]
        [JsonProperty("read_Schaden")]
        public bool read_Schaden { get; set; }

        [DataMember]
        [JsonProperty("write_Schaden")]
        public bool write_Schaden { get; set; }

        [DataMember]
        [JsonProperty("read_LagerOrt")]
        public bool read_LagerOrt { get; set; }

        [DataMember]
        [JsonProperty("write_LagerOrt")]
        public bool write_LagerOrt { get; set; }

        [DataMember]
        [JsonProperty("read_ASNTransfer")]
        public bool read_ASNTransfer { get; set; }

        [DataMember]
        [JsonProperty("write_ASNTransfer")]
        public bool write_ASNTransfer { get; set; }

        [DataMember]
        [JsonProperty("read_FaktExtraCharge")]
        public bool read_FaktExtraCharge { get; set; }

        [DataMember]
        [JsonProperty("write_FaktExtraCharge")]
        public bool write_FaktExtraCharge { get; set; }

        [DataMember]
        [JsonProperty("access_StKV")]
        public bool access_StKV { get; set; }

        [DataMember]
        [JsonProperty("access_App")]
        public bool access_App { get; set; }

        [DataMember]
        [JsonProperty("access_AppStoreIn")]
        public bool access_AppStoreIn { get; set; }

        [DataMember]
        [JsonProperty("access_AppStoreOut")]
        public bool access_AppStoreOut { get; set; }

        [DataMember]
        [JsonProperty("access_AppInventory")]
        public bool access_AppInventory { get; set; }

        [DataMember]
        [JsonProperty("read_Inventory")]
        public bool read_Inventory { get; set; }
        [DataMember]
        [JsonProperty("write_Inventory")]
        public bool write_Inventory { get; set; }


    }
}
