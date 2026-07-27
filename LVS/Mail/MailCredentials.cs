using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LVS.Mail
{
    public class MailCredentials
    {
        public MailCredentials()
        {
        }

        /// <summary>IONOS SMTP Host (Standard: smtp.ionos.de)</summary>
        public string SmtpHost { get; set; } = "smtp.ionos.de";

        /// <summary>Port 587 (STARTTLS) oder 465 (SSL)</summary>
        public int SmtpPort { get; set; } = 587;

        /// <summary>Account A – SMTP-Login (technischer Absender)</summary>
        public string SmtpUser { get; set; } = string.Empty;

        /// <summary>Account A – Passwort</summary>
        public string SmtpPassword { get; set; } = string.Empty;

        /// <summary>Anzeigename für Account A</summary>
        public string SmtpDisplayName { get; set; } = string.Empty;

        /// <summary>
        /// Verbindung offen halten für mehrere Mails (Batch-Versand).
        /// Für Einzelmails auf false lassen.
        /// </summary>
        public bool KeepAlive { get; set; } = false;
    }
}
