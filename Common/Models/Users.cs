using Common.Enumerations;
using Common.Helper;
using Common.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;


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

        [JsonIgnore]
        [IgnoreDataMember]
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

        /// <summary>
        /// Deserialisierte Mail-Credentials (wenn MailCredentialsData erfolgreich verarbeitet wurde).
        /// Nur lesend, damit Aufrufer Zugriff ohne erneute Deserialisierung haben.
        /// </summary>
        [JsonIgnore]
        [IgnoreDataMember]
        public MailCredentials MailCredentials => _internalMailCredentials;

        [DataMember]
        [JsonProperty("MailCredentialsFileName")]
        public string MailCredentialsFileName { get; set; } = string.Empty;

        // intern gehaltene, deserialisierte Credentials (falls erfolgreich gefunden)
        private MailCredentials _internalMailCredentials = null;

        /// <summary>
        /// Versucht die Bytes als UTF8-JSON oder als DPAPI-geschützte Bytes zu interpretieren.
        /// Setzt bei Erfolg _internalMailCredentials und wendet die Werte auf die Felder an.
        /// </summary>
        private void ProcessMailCredentialsBytes(byte[] bytes)
        {
            if (bytes == null || bytes.Length == 0)
            {
                _internalMailCredentials = null;
                return;
            }

            // Versuch 1: direkte JSON (UTF8)
            try
            {
                var json = Encoding.UTF8.GetString(bytes);
                var creds = JsonConvert.DeserializeObject<MailCredentials>(json);
                if (creds != null)
                {
                    _internalMailCredentials = creds;
                    ApplyMailCredentialsToFields(creds);
                    return;
                }
            }
            catch
            {
                // kein valides JSON - _internalMailCredentials bleibt null
                _internalMailCredentials = null;
            }
        }
        /// <summary>
        /// Öffentliche Hilfs-Methode: verarbeitet bereits entschlüsselte JSON-Bytes (UTF8).
        /// Aufruf durch plattformspezifischen Code (z.B. LVS) möglich.
        /// </summary>
        public void SetMailCredentialsFromJsonBytes(byte[] jsonUtf8Bytes)
        {
            ProcessMailCredentialsBytes(jsonUtf8Bytes);
        }
        /// <summary>
        /// Wendet deserialisierte MailCredentials auf die Benutzerfelder an.
        /// Überschreibt nur, wenn Credential-Werte vorhanden sind.
        /// </summary>
        private void ApplyMailCredentialsToFields(MailCredentials creds)
        {
            if (creds == null) return;
            try
            {
                if (!string.IsNullOrWhiteSpace(creds.SmtpHost))
                    this.SMTPServer = creds.SmtpHost;

                if (creds.SmtpPort > 0)
                    this.SMTPPort = creds.SmtpPort;

                if (!string.IsNullOrWhiteSpace(creds.SmtpUser))
                    this.SMTPUser = creds.SmtpUser;

                if (!string.IsNullOrWhiteSpace(creds.SmtpPassword))
                    this.SMTPPasswort = creds.SmtpPassword;

                if (!string.IsNullOrWhiteSpace(creds.SmtpDisplayName))
                    this.Mail = creds.SmtpDisplayName;
                else if (!string.IsNullOrWhiteSpace(creds.SmtpUser))
                    this.Mail = creds.SmtpUser;

                // heuristische SMTPSSL-Setzung
                if (creds.SmtpPort == 465 || creds.SmtpPort == 587)
                    this.SMTPSSL = true;
            }
            catch
            {
                // defensiv: keine Ausnahme nach außen
            }
        }



        public Users Copy()
        {
            return (Users)this.MemberwiseClone();
        }



    }

}

