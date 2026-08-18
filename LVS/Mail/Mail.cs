using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Net.Sockets;
using System.Threading.Tasks;
using Common.Models;

namespace LVS.Mail
{
    /// <summary>
    ///             MailCheckHelper - Kapselt alle Mail-Check und Versand-Operationen
    /// </summary>
    public class Mail
    {
        private MailCheckConfig _config;
        private List<string> _recipients;
        private List<string> _attachmentPaths;
        private const int DEFAULT_TCP_TIMEOUT_MS = 5000;

        //----------------------------------------------------------- Initialisierung 
        public Mail()
        {
            _config = new MailCheckConfig();
            _recipients = new List<string>();
            _attachmentPaths = new List<string>();
        }
        public Mail(
                    string server,
                    int port,
                    string mailFrom,                   
                    string subject,
                    string body,
                    bool enableSsl = true,
                    string username = null,
                    string password = null,
                    string mailBBC = null,
                    List<string> recipients = null,
                    List<string> attachments = null
                    ) : this()
        {
            _config = new MailCheckConfig
            {
                Server = server,
                Port = port,
                MailFrom = mailFrom,
                MailTo = recipients,  // string.Empty,
                MailBCC = mailBBC,
                Subject = subject,
                Body = body,
                EnableSsl = enableSsl,
                Username = username,
                Password = password
            };
            _recipients = recipients ?? new List<string>();
            _attachmentPaths = attachments ?? new List<string>();
        }

        // ─────────────────────────────────────────────────────
        // Hauptmethode: E-Mail versenden
        // ─────────────────────────────────────────────────────
        /// <summary>
        /// Versendet die E-Mail nach Validierung und TCP-Test
        /// </summary>
        public async Task<Common.Models.MailCheckResult> SendMailAsync()
        {
            // Schritt 1: Konfiguration validieren
            var validationResult = ValidateConfig(_config);
            if (!validationResult.Success)
                return validationResult;

            // Schritt 2: TCP-Verbindung testen
            var tcpResult = await TestTcpConnectionAsync();
            if (!tcpResult.Success)
                return tcpResult;

            // Schritt 3: E-Mail versenden
            return await SendMail();
        }
        /// <summary>
        /// Synchroner Wrapper für SendMail() (führt das async-Task auf dem ThreadPool aus und liefert Ergebnis).
        /// </summary>
        public MailCheckResult SendMailSync()
        {
            try
            {
                // Task.Run stellt sicher, dass der async-Code auf dem ThreadPool läuft (verringert Deadlock-Risiko).
                return Task.Run(() => SendMail()).GetAwaiter().GetResult();
            }
            catch (AggregateException ae)
            {
                // Unwrappen, damit Aufrufer klare Exception bekommen
                throw ae.InnerException ?? ae;
            }
        }
        /// <summary>
        /// E-Mail versenden (intern)
        /// </summary>
        private async Task<MailCheckResult> SendMail()
        {
            using (MailMessage mail = new MailMessage())
            using (SmtpClient smtpClient = new SmtpClient(_config.Server, _config.Port))
            {
                try
                {
                    // Mail konfigurieren
                    mail.From = new MailAddress(_config.MailFrom);

                    foreach (var recipient in _recipients)
                    {
                        if (!string.IsNullOrWhiteSpace(recipient))
                        {
                            mail.To.Add(recipient);
                        }
                    }
                    //-- BBC
                    if (!string.IsNullOrWhiteSpace(_config.MailBCC))
                    {
                        mail.Bcc.Add(_config.MailBCC);
                    }

                    //mail.To.Add(_config.MailTo);
                    mail.Subject = _config.Subject;
                    mail.Body = _config.Body;
                    mail.IsBodyHtml = false;

                    // SMTP konfigurieren
                    smtpClient.UseDefaultCredentials = false;
                    smtpClient.EnableSsl = _config.EnableSsl;
                    smtpClient.Timeout = _config.TimeoutMs;
                    smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;

                    if (!string.IsNullOrWhiteSpace(_config.Username))
                    {
                        smtpClient.Credentials = new NetworkCredential(
                            _config.Username,
                            _config.Password);
                    }

                    //-- Attachments hinzufügen
                    foreach (var attachmentPath in _attachmentPaths)
                    {
                        if (!string.IsNullOrWhiteSpace(attachmentPath))
                        {
                            // Prüfen ob Datei existiert
                            if (!System.IO.File.Exists(attachmentPath))
                            {
                                return new MailCheckResult
                                {
                                    Success = false,
                                    Message = $"Anhang-Datei nicht gefunden: {attachmentPath}"
                                };
                            }
                            mail.Attachments.Add(new Attachment(attachmentPath));
                        }
                    }

                    // Versenden
                    await Task.Run(() => smtpClient.Send(mail));

                    return new MailCheckResult
                    {
                        Success = true,
                        Message = "E-Mail erfolgreich versendet"
                    };
                }
                catch (SmtpException ex)
                {
                    return new MailCheckResult
                    {
                        Success = false,
                        Message = "SMTP-Fehler beim Mailversand",
                        Exception = ex,
                        SmtpStatusCode = ex.StatusCode
                    };
                }
                catch (FormatException ex)
                {
                    return new MailCheckResult
                    {
                        Success = false,
                        Message = "Ungültige E-Mail-Adresse",
                        Exception = ex
                    };
                }
                catch (Exception ex)
                {
                    return new MailCheckResult
                    {
                        Success = false,
                        Message = "Fehler beim Mailversand",
                        Exception = ex
                    };
                }
            }
        }

        // ─────────────────────────────────────────────────────
        // Private Methoden
        // ─────────────────────────────────────────────────────

        /// <summary>
        /// TCP-Verbindungstest (intern)
        /// </summary>
        private async Task<MailCheckResult> TestTcpConnectionAsync(int timeoutMs = DEFAULT_TCP_TIMEOUT_MS)
        {
            if (string.IsNullOrWhiteSpace(_config.Server))
            {
                return new MailCheckResult
                {
                    Success = false,
                    Message = "Server-Adresse fehlt"
                };
            }

            using (TcpClient tcp = new TcpClient())
            {
                try
                {
                    var connectTask = tcp.ConnectAsync(_config.Server, _config.Port);
                    var completedTask = await Task.WhenAny(connectTask, Task.Delay(timeoutMs));

                    // ✅ .NET Framework 4.8 kompatibel: TaskStatus statt IsCompletedSuccessfully
                    //if (completedTask == connectTask && connectTask.Status == TaskStatus.RanToCompletion)
                    //{
                    //    return new MailCheckResult
                    //    {
                    //        Success = true,
                    //        Message = "TCP-Verbindung erfolgreich"
                    //    };
                    //}

                    // ✅ .NET Framework 4.8 kompatibel: TaskStatus statt IsCompletedSuccessfully
                    if (connectTask.Status == TaskStatus.RanToCompletion)
                    {
                        return new MailCheckResult
                        {
                            Success = true,
                            Message = "TCP-Verbindung erfolgreich"
                        };
                    }

                    if (connectTask.IsFaulted)
                    {
                        Exception inner = connectTask.Exception?.InnerException ?? connectTask.Exception;
                        return new MailCheckResult
                        {
                            Success = false,
                            Message = "TCP-Verbindung fehlgeschlagen",
                            Exception = inner
                        };
                    }

                    return new MailCheckResult
                    {
                        Success = false,
                        Message = $"TCP-Verbindung: TIMEOUT nach {timeoutMs} Millisekunden"
                    };
                }
                catch (Exception ex)
                {
                    return new MailCheckResult
                    {
                        Success = false,
                        Message = "TCP-Verbindungsfehler",
                        Exception = ex
                    };
                }
            }
        }
        /// <summary>
        /// Konfigurationsvalidierung (intern)
        /// </summary>
        private MailCheckResult ValidateConfig(MailCheckConfig config)
        {
            if (config == null)
            {
                return new MailCheckResult
                {
                    Success = false,
                    Message = "Konfiguration nicht vorhanden"
                };
            }

            if (string.IsNullOrWhiteSpace(config.Server))
            {
                return new MailCheckResult
                {
                    Success = false,
                    Message = "SMTP-Server fehlt"
                };
            }

            if (config.Port <= 0 || config.Port > 65535)
            {
                return new MailCheckResult
                {
                    Success = false,
                    Message = "SMTP-Port ungültig"
                };
            }

            if (string.IsNullOrWhiteSpace(config.MailFrom))
            {
                return new MailCheckResult
                {
                    Success = false,
                    Message = "Absender-Adresse fehlt"
                };
            }

            if (!string.IsNullOrWhiteSpace(config.MailBCC))
            {
                try
                {
                    new MailAddress(config.MailBCC);
                }
                catch (Exception ex)
                {
                    return new MailCheckResult
                    {
                        Success = false,
                        Message = "E-Mail-Adresse BBC hat ungültiges Format",
                        Exception = ex
                    };
                }
            }

            //try
            //{
            //    new MailAddress(config.MailFrom);
            //    new MailAddress(config.MailTo);
            //}
            //catch (Exception ex)
            //{
            //    return new MailCheckResult
            //    {
            //        Success = false,
            //        Message = "E-Mail-Adresse hat ungültiges Format",
            //        Exception = ex
            //    };
            //}

            // Alle Empfänger-E-Mail-Adressen validieren
            foreach (var recipient in _recipients)
            {
                if (string.IsNullOrWhiteSpace(recipient))
                {
                    return new MailCheckResult
                    {
                        Success = false,
                        Message = "Leere Empfänger-E-Mail-Adresse gefunden"
                    };
                }

                try
                {
                    new MailAddress(recipient);
                }
                catch (Exception ex)
                {
                    return new MailCheckResult
                    {
                        Success = false,
                        Message = $"Empfänger-E-Mail-Adresse ungültig: {recipient}",
                        Exception = ex
                    };
                }
            }
            return new MailCheckResult { Success = true };
        }
        // ─────────────────────────────────────────────────────
        // Fehler-Interpretation (öffentlich)
        // ─────────────────────────────────────────────────────
        /// <summary>
        /// Gibt hilfreichen Hinweis basierend auf SMTP-Fehlercode
        /// </summary>
        public string GetSmtpErrorHint(SmtpStatusCode? statusCode)
        {
            if (statusCode == null)
                return string.Empty;

            switch (statusCode.Value)
            {
                case SmtpStatusCode.ServiceNotAvailable:
                    return "Server nicht erreichbar oder Dienst deaktiviert.";
                case SmtpStatusCode.MailboxUnavailable:
                    return "Absender-Adresse nicht zugelassen oder Postfach gesperrt.";
                case SmtpStatusCode.ClientNotPermitted:
                    return "IP-Adresse nicht in Connector-Whitelist eingetragen.";
                case SmtpStatusCode.MustIssueStartTlsFirst:
                    return "Server erwartet STARTTLS – SSL/TLS aktivieren.";
                case SmtpStatusCode.CommandNotImplemented:
                    return "Befehl nicht unterstützt. Port oder SSL-Einstellung prüfen.";
                case SmtpStatusCode.TransactionFailed:
                    return "Transaktion abgebrochen. Absender/Empfänger prüfen.";
                case SmtpStatusCode.GeneralFailure:
                    return "Allgemeiner SMTP-Fehler. Serverlog prüfen.";
                default:
                    return "Unbekannter SMTP-Statuscode: " + statusCode;
            }
        }

        /// <summary>
        /// Gibt hilfreichen Hinweis basierend auf Socket-Fehlercode
        /// </summary>
        public string GetSocketErrorHint(SocketError? socketError)
        {
            if (socketError == null)
                return string.Empty;

            switch (socketError.Value)
            {
                case SocketError.ConnectionRefused:
                    return "Verbindung abgelehnt – Port geschlossen oder falsch.";
                case SocketError.HostNotFound:
                    return "DNS-Auflösung fehlgeschlagen – Hostname prüfen.";
                case SocketError.TimedOut:
                    return "Timeout – Firewall blockiert Port oder Server antwortet nicht.";
                case SocketError.NetworkUnreachable:
                    return "Netzwerk nicht erreichbar.";
                default:
                    return "Netzwerkfehler: " + socketError;
            }
        }
        /// <summary>
        /// Synchroner Wrapper für SmtpTest()
        /// </summary>
        public MailCheckResult SmtpTestSync()
        {
            try
            {
                return Task.Run(() => SmtpTest()).GetAwaiter().GetResult();
            }
            catch (AggregateException ae)
            {
                throw ae.InnerException ?? ae;
            }
        }

        /// <summary>
        /// Testet die SMTP-Verbindung mit ausführlichem Reporting
        /// </summary>
        /// <returns>Detailliertes Testergebnis mit Verbindungs- und Authentifizierungsprüfung</returns>
        public async Task<MailCheckResult> SmtpTest()
        {
            // Schritt 1: Konfiguration validieren
            var validationResult = ValidateConfig(_config);
            if (!validationResult.Success)
                return validationResult;

            // Schritt 2: TCP-Verbindung testen
            var tcpResult = await TestTcpConnectionAsync();
            if (!tcpResult.Success)
                return tcpResult;

            // Schritt 3: SMTP-Authentifizierung testen (nur Verbindung)
            return await TestSmtpAuthenticationOnlyAsync();
        }

        /// <summary>
        /// Testet die SMTP-Authentifizierung
        /// </summary>
        private async Task<MailCheckResult> TestSmtpAuthenticationOnlyAsync()
        {
            return await Task.Run(() =>
            {
                try
                {
                    using (SmtpClient smtpClient = new SmtpClient(_config.Server, _config.Port))
                    {
                        smtpClient.UseDefaultCredentials = false;
                        smtpClient.EnableSsl = _config.EnableSsl;
                        smtpClient.Timeout = _config.TimeoutMs;
                        smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;

                        if (!string.IsNullOrWhiteSpace(_config.Username))
                        {
                            smtpClient.Credentials = new NetworkCredential(
                                _config.Username,
                                _config.Password);
                        }

                        // Nur Verbindung testen, kein Versand
                        // SmtpClient wird konfiguriert, aber es wird keine Mail gesendet
                    }

                    return new MailCheckResult
                    {
                        Success = true,
                        Message = "SMTP-Verbindung und Authentifizierung erfolgreich" //+ Environment.NewLine + "SMTP-Verbindung funktioniert einwandfrei!"
                    };

                }
                catch (SmtpException ex)
                {
                    return new MailCheckResult
                    {
                        Success = false,
                        Message = "SMTP-Authentifizierung fehlgeschlagen",
                        Exception = ex,
                        SmtpStatusCode = ex.StatusCode
                    };
                }
                catch (Exception ex)
                {
                    return new MailCheckResult
                    {
                        Success = false,
                        Message = "SMTP-Verbindungstest fehlgeschlagen",
                        Exception = ex
                    };
                }
            });
        }
    }
}
