using Common.Models;
using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace LvsScan.Portable.Models
{
    public class wizDamage
    {

        [DataMember]
        [JsonProperty("Article")]
        public Articles Article { get; set; } = new Articles();

        [DataMember]
        [JsonProperty("ErrorMessage")]
        public string ErrorMessage { get; set; } = string.Empty;

        public wizDamage Copy()
        {
            return (wizDamage)this.MemberwiseClone();
        }
    }
}
