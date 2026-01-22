using Common.Enumerations;
using Common.Helper;
using Common.Models;
using System;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Common.Models
{
    [Serializable]
    [DataContract]
    public class Users
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
        [JsonProperty("Name")]
        private string _Name = string.Empty;
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }

        [DataMember]
        [JsonProperty("Vorname")]
        private string _Vorname = string.Empty;
        public string Vorname
        {
            get { return _Vorname; }
            set { _Vorname = value; }
        }

        [DataMember]
        [JsonProperty("pass")]
        private string _pass = string.Empty;
        public string pass
        {
            get { return _pass; }
            set { _pass = value; }
        }

        [DataMember]
        [JsonProperty("Initialen")]
        private string _Initialen = string.Empty;
        public string Initialen
        {
            get { return _Initialen; }
            set { _Initialen = value; }
        }

        [DataMember]
        [JsonProperty("LoginName")]
        private string _LoginName = string.Empty;
        public string LoginName
        {
            get { return _LoginName; }
            set { _LoginName = value; }
        }

        [DataMember]
        [JsonProperty("Tel")]
        private string _Tel = string.Empty;
        public string Tel
        {
            get { return _Tel; }
            set { _Tel = value; }
        }

        [DataMember]
        [JsonProperty("Fax")]
        private string _Fax = string.Empty;
        public string Fax
        {
            get { return _Fax; }
            set { _Fax = value; }
        }

        [DataMember]
        [JsonProperty("Mail")]
        private string _Mail = string.Empty;
        public string Mail
        {
            get { return _Mail; }
            set { _Mail = value; }
        }


        [DataMember]
        [JsonProperty("FontSize")]
        private decimal _FontSize;
        public decimal FontSize
        {
            get { return _FontSize; }
            set { _FontSize = value; }
        }

        [DataMember]
        [JsonProperty("dtDispoVon")]
        public DateTime dtDispoVon { get; set; }

        [DataMember]
        [JsonProperty("dtDispoBis")]
        public DateTime dtDispoBis { get; set; }

        [DataMember]
        [JsonProperty("SMTPServer")]
        public string SMTPServer { get; set; } = string.Empty;

        [DataMember]
        [JsonProperty("SMTPPort")]
        public Int32 SMTPPort { get; set; } = 0;

        [DataMember]
        [JsonProperty("SMTPUser")]
        public string SMTPUser { get; set; } = string.Empty;

        [DataMember]
        [JsonProperty("SMTPPasswort")]
        public string SMTPPasswort { get; set; } = string.Empty;

        [DataMember]
        [JsonProperty("SMTPSSL")]
        public bool SMTPSSL { get; set; }

        [DataMember]
        [JsonProperty("IsAdmin")]
        public bool IsAdmin { get; set; }

        [DataMember]
        [JsonProperty("userAuthorization")]
        public UserAuthorizations UserAuthorization { get; set; }

        public Users Copy()
        {
            return (Users)this.MemberwiseClone();
        }



    }

}
