using Common.Enumerations;
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
    public class Damages
    {
        [DataMember]
        [JsonProperty("id")]
        public int Id { get; set; } = 0;

        [DataMember]
        [JsonProperty("Designation")]
        public string Designation { get; set; } = string.Empty;

        [DataMember]
        [JsonProperty("Descrition")]
        public string Descrition { get; set; } = string.Empty;

        [DataMember]
        [JsonProperty("isActiv")]
        public bool IsActiv { get; set; } = false;

        [DataMember]
        [JsonProperty("ViewId")]
        public int ViewId { get; set; } = 0;

        [DataMember]
        [JsonProperty("Art")]
        public int Art { get; set; } = 0;

        [DataMember]
        [JsonProperty("Code")]
        public int Code { get; set; } = 0;

        [DataMember]
        [JsonProperty("AutoSPL")]
        public bool AutoSPL { get; set; } = false;

        [DataMember]
        [JsonProperty("ArtString")]
        public string ArtString { get; set; } = string.Empty;

        public enumDamageArt DamageArt
        {
            get
            {
                enumDamageArt tmpArt = enumDamageArt.criticalDamage;
                switch (Art)
                {
                    case 0:
                        tmpArt = enumDamageArt.criticalDamage;
                        break;
                    case 1:
                        tmpArt = enumDamageArt.slightDamage;
                        break;

                }
                return tmpArt;
            }
        }

        public Color BackgroundColorView
        {
            get
            {
                Color tmpColor = Color.Empty;
                switch (DamageArt)
                {
                    case enumDamageArt.criticalDamage:
                        tmpColor = Color.Tomato;
                        break;
                    case enumDamageArt.slightDamage:
                        tmpColor = Color.Yellow;
                        break;

                }
                return tmpColor;
            }
        }
        public Damages Copy()
        {
            return (Damages)this.MemberwiseClone();
        }
    }

}
