using Newtonsoft.Json;
using System;
using System.Runtime.Serialization;

namespace LVS.Models
{
    [Serializable]
    [DataContract]
    public class Users
    {
        [DataMember]
        [JsonProperty("Id")]
        private decimal _Id;
        public decimal Id
        {
            get { return _Id; }
            set { _Id = value; }
        }

        [DataMember]
        [JsonProperty("Name")]
        private string _Name;
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }

        [DataMember]
        [JsonProperty("Vorname")]
        private string _Vorname;
        public string Vorname
        {
            get { return _Vorname; }
            set { _Vorname = value; }
        }

        [DataMember]
        [JsonProperty("pass")]
        private string _pass;
        public string pass
        {
            get { return _pass; }
            set { _pass = value; }
        }

        [DataMember]
        [JsonProperty("Initialen")]
        private string _Initialen;
        public string Initialen
        {
            get { return _Initialen; }
            set { _Initialen = value; }
        }

        [DataMember]
        [JsonProperty("LoginName")]
        private string _LoginName;
        public string LoginName
        {
            get { return _LoginName; }
            set { _LoginName = value; }
        }

        [DataMember]
        [JsonProperty("Tel")]
        private string _Tel;
        public string Tel
        {
            get { return _Tel; }
            set { _Tel = value; }
        }

        [DataMember]
        [JsonProperty("Fax")]
        private string _Fax;
        public string Fax
        {
            get { return _Fax; }
            set { _Fax = value; }
        }

        [DataMember]
        [JsonProperty("Mail")]
        private string _Mail;
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
        public string SMTPServer { get; set; }

        [DataMember]
        [JsonProperty("SMTPPort")]
        public Int32 SMTPPort { get; set; }

        [DataMember]
        [JsonProperty("SMTPUser")]
        public string SMTPUser { get; set; }

        [DataMember]
        [JsonProperty("SMTPPasswort")]
        public string SMTPPasswort { get; set; }

        [DataMember]
        [JsonProperty("SMTPSSL")]
        public bool SMTPSSL { get; set; }

        [DataMember]
        [JsonProperty("IsAdmin")]
        public bool IsAdmin { get; set; }

        public UserAuthorizations UserAuthorization { get; set; }



    }

}
