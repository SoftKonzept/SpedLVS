using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Net.Sockets;
using System.Threading.Tasks;
using Common.Models;

namespace LVS.Mail
{
    public class MailSending
    {
        private Mail mail { get; set; } = new Mail();
        public List<string> attachment { get; set; } = new List<string>();
        public List<string> recipients { get; set; } = new List<string>();
        public List<string> infoMessages { get; set; } = new List<string>();

        public string Subject { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public string MailFrom { get; set; } = string.Empty;
        public string MailBCC { get; set; } = string.Empty;
        public string SMTPServer { get; set; } = string.Empty;
        public Int32 SMTPPort { get; set; } = 587;
        public string SMTPUser { get; set; } = string.Empty;
        public string SMTPPasswort { get; set; } = string.Empty;
        public bool SMTPSsl { get; set; } = true;
        public bool SuccessSending { get; set; } = false;

        private clsSystem system { get; set; } = new clsSystem();

        //-------------------------------------------------------------------------- Konstruktor / Initialisierung
        /// <summary>
        ///                 Initialisierung
        /// </summary>
        public MailSending()
        {
            mail = new Mail();
            attachment = new List<string>();
            recipients = new List<string>();
            infoMessages = new List<string>();
            system = new clsSystem();
        }

        public MailSending(Globals._GL_USER myGLUser, clsSystem mySystem):this()
        {
            SetMailCredentials(myGLUser, mySystem);
        }


        //---------------------------------------------------------------------------- Credentials
        /// <summary>
        /// 
        /// </summary>
        /// <param name="myGLUser"></param>
        /// <param name="mySystem"></param>

        public void SetMailCredentials(Globals._GL_USER myGLUser, clsSystem mySystem)
        {
            system = mySystem;

            if (mySystem.VE_IsWatchDog)
            {
                this.SMTPServer = clsSystem.const_Mail_SMTPServer;
                this.SMTPUser = clsSystem.const_Mail_SMTPUser;
                this.SMTPPasswort = clsSystem.const_Mail_SMTPPasswort;
                this.SMTPPort = clsSystem.const_Mail_SMTPPort;
                this.MailFrom = clsSystem.const_MailAdress;
                this.SMTPSsl = true;
            }
            else
            {
                if (
                        (myGLUser.IsAdmin) &&
                        (myGLUser.Name.ToUpper().Equals("ADMINISTRATOR"))
                   )
                {
                    this.SMTPServer = clsSystem.const_Mail_SMTPServer;
                    this.SMTPUser = clsSystem.const_Mail_SMTPUser;
                    this.SMTPPasswort = clsSystem.const_Mail_SMTPPasswort;
                    this.SMTPPort = clsSystem.const_Mail_SMTPPort;
                    this.MailFrom = clsSystem.const_MailAdress;
                    this.SMTPSsl = true;
                }
                else
                {
                    if (mySystem.Client is clsClient)
                    {
                        if (mySystem.Client.Modul.Mail_UsingMainMailForMailing)
                        {
                            this.SMTPServer = mySystem.Client.Modul.Mail_SMTPServer;
                            this.SMTPUser = mySystem.Client.Modul.Mail_SMTPUser;
                            this.SMTPPasswort = mySystem.Client.Modul.Mail_SMTPPasswort;
                            this.SMTPPort = mySystem.Client.Modul.Mail_SMTPPort;
                            this.MailFrom = mySystem.Client.Modul.Mail_MailAdress;
                            this.SMTPSsl = mySystem.Client.Modul.Mail_SMTPSSL;
                        }
                        else
                        {
                            if (myGLUser.User_ID == 0)
                            {
                                this.SMTPServer = mySystem.Client.Modul.Mail_SMTPServer;
                                this.SMTPUser = mySystem.Client.Modul.Mail_SMTPUser;
                                this.SMTPPasswort = mySystem.Client.Modul.Mail_SMTPPasswort;
                                this.SMTPPort = mySystem.Client.Modul.Mail_SMTPPort;
                                this.MailFrom = mySystem.Client.Modul.Mail_MailAdress;
                                this.SMTPSsl = mySystem.Client.Modul.Mail_SMTPSSL;
                            }
                            else
                            {
                                this.SMTPServer = myGLUser.SMTPServer;
                                this.SMTPUser = myGLUser.SMTPUser;
                                this.SMTPPasswort = myGLUser.SMTPPass;
                                this.SMTPPort = myGLUser.SMTPPort;
                                this.MailFrom = myGLUser.Mail;
                                this.SMTPSsl = true;
                            }
                        }
                    }
                }
            }
        }
        ///------------------------------------------------------------------------Subject
        /// <summary>
        /// 
        /// </summary>
        /// <param name="mySubject"></param>
        public void SetMail_Subject(string mySubject)
        {
            this.Subject = mySubject;
        }
        ///------------------------------------------------------------------------Message
        /// <summary>
        /// 
        /// </summary>
        /// <param name="myMessage"></param>
        public void SetMail_Message(string myMessage)
        {
            this.Message = myMessage;
        }

        //------------------------------------------------------------------------ Attachmnt 
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filePath"></param>
        public void AddAttachment(string filePath)
        {
            attachment.Add(filePath);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filePaths"></param>
        public void AddAttachments(List<string> filePaths)
        {
            attachment.AddRange(filePaths);
        }
        //------------------------------------------------------------------------ Validation
        /// <summary>
        /// Prüft, ob alle erforderlichen Daten für den E-Mail-Versand vorhanden sind
        /// </summary>
        /// <returns>true wenn alle Daten vorhanden sind, false wenn Daten fehlen</returns>
        public bool CheckMailDataComplete()
        {
            bool bErrorExist = true;
            infoMessages.Clear();

            // SMTP-Konfiguration prüfen
            if (string.IsNullOrWhiteSpace(this.SMTPServer))
            {
                infoMessages.Add("SMTP-Server fehlt");
                bErrorExist = false;
            }

            if (this.SMTPPort <= 0 || this.SMTPPort > 65535)
            {
                infoMessages.Add("SMTP-Port ungültig (gültig: 1-65535)");
                bErrorExist = false;
            }

            if (string.IsNullOrWhiteSpace(this.SMTPUser))
            {
                infoMessages.Add("SMTP-Benutzer fehlt");
                bErrorExist = false;
            }

            if (string.IsNullOrWhiteSpace(this.SMTPPasswort))
            {
                infoMessages.Add("SMTP-Passwort fehlt");
            }

            // Absender prüfen
            if (string.IsNullOrWhiteSpace(this.MailFrom))
            {
                infoMessages.Add("Absender-E-Mail-Adresse fehlt");
                bErrorExist = false;
            }
            else
            {
                if (!IsValidEmailAddress(this.MailFrom))
                {
                    infoMessages.Add($"Absender-E-Mail-Adresse ungültig: {this.MailFrom}");
                    bErrorExist = false;
                }
            }

            // Empfänger prüfen
            if (this.recipients == null || this.recipients.Count == 0)
            {
                infoMessages.Add("Keine Empfänger vorhanden");
                bErrorExist = false;
            }
            else
            {
                foreach (var recipient in this.recipients)
                {
                    if (string.IsNullOrWhiteSpace(recipient))
                    {
                        infoMessages.Add("Leere Empfänger-E-Mail-Adresse gefunden");
                        bErrorExist = false;
                    }
                    else if (!IsValidEmailAddress(recipient))
                    {
                        infoMessages.Add($"Empfänger-E-Mail-Adresse ungültig: {recipient}");
                        bErrorExist = false;
                    }
                }
            }

            // Betreffzeile prüfen
            if (string.IsNullOrWhiteSpace(this.Subject))
            {
                infoMessages.Add("Betreffzeile fehlt");
                bErrorExist = false;
            }

            // Nachrichtentext prüfen
            if (string.IsNullOrWhiteSpace(this.Message))
            {
                infoMessages.Add("Nachrichtentext fehlt");
            }

            return bErrorExist; // infoMessages.Count == 0;
        }

        /// <summary>
        /// Validiert E-Mail-Adresse
        /// </summary>
        private bool IsValidEmailAddress(string email)
        {
            try
            {
                new MailAddress(email);
                return true;
            }
            catch
            {
                return false;
            }
        }

        //------------------------------------------------------------------------ SendMail
        /// <summary>
        /// Synchroner Wrapper für das asynchrone Send(bool)
        /// </summary>
        public bool SendSync(bool myIsError)
        {
            try
            {
                return Task.Run(() => Send(myIsError)).GetAwaiter().GetResult();
            }
            catch (AggregateException ae)
            {
                throw ae.InnerException ?? ae;
            }
        }
        /// <summary>
        /// Versendet die E-Mail asynchron nach Validierung
        /// </summary>
        /// <param name="myIsError">Bei true werden System-Mail-Credentials verwendet</param>
        /// <returns>true bei erfolgreichem Versand, false bei Fehler</returns>
        public async Task<bool> Send(bool myIsError)
        {
            infoMessages.Clear();
            this.SuccessSending = false;

            if (myIsError)
            {
                //--- Credentials
                this.SMTPServer = clsSystem.const_Mail_SMTPServer;
                this.SMTPPort = clsSystem.const_Mail_SMTPPort;
                this.SMTPUser = clsSystem.const_Mail_SMTPUser;
                this.SMTPPasswort = clsSystem.const_Mail_SMTPPasswort;
                this.MailFrom = clsSystem.const_MailAdress;
                this.SMTPSsl = true;

                recipients.Clear();
                recipients.Add(clsSystem.const_MailAdress);
            }

            if (this.system.DebugModeCOM)
            {
                this.recipients.Clear();
                this.recipients.Add(clsSystem.const_MailAdress);
            }

            // Daten vor dem Versand validieren
            if (!CheckMailDataComplete())
            {
                infoMessages.Insert(0, "E-Mail-Versand nicht möglich - folgende Daten fehlen:");
                return false; 
            }

            mail = new Mail(this.SMTPServer,
                            this.SMTPPort,
                            this.MailFrom,                           
                            this.Subject,
                            this.Message,
                            this.SMTPSsl,
                            this.SMTPUser,
                            this.SMTPPasswort,
                            this.MailBCC,
                            this.recipients,
                            this.attachment);

            await SendProzess();
            return this.SuccessSending;
        }
        /// <summary>
        /// Synchroner Wrapper für das asynchrone SendNoReply()
        /// </summary>
        public bool SendNoReplySync()
        {
            try
            {
                return Task.Run(() => SendNoReply()).GetAwaiter().GetResult();
            }
            catch (AggregateException ae)
            {
                throw ae.InnerException ?? ae;
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<bool> SendNoReply()
        {
            infoMessages.Clear();
            this.SuccessSending = false;

            //--- Credentials
            this.SMTPServer = this.system.Client.Modul.Mail_Noreply_SMTPServer;
            this.SMTPPort = this.system.Client.Modul.Mail_Noreply_SMTPPort;
            this.SMTPUser = this.system.Client.Modul.Mail_Noreply_SMTPUser;
            this.SMTPPasswort = this.system.Client.Modul.Mail_Noreply_SMTPPasswort;
            this.MailFrom = this.system.Client.Modul.Mail_Noreply_MailAdress;
            this.SMTPSsl = true;

            if (this.system.DebugModeCOM)
            {
                this.recipients.Clear();
                this.recipients.Add(clsSystem.const_MailAdress);
            }
            this.Message = "Dies ist eine automatisch generierte E-Mail. Bitte antworten Sie nicht auf diese Nachricht.";

            // Daten vor dem Versand validieren
            if (!CheckMailDataComplete())
            {
                infoMessages.Insert(0, "E-Mail-Versand nicht möglich - folgende Daten fehlen:");
                return false;
            }
            mail = new Mail(this.SMTPServer,
                            this.SMTPPort,
                            this.MailFrom,
                            this.Subject,
                            this.Message,
                            this.SMTPSsl,
                            this.SMTPUser,
                            this.SMTPPasswort,
                            this.MailBCC,
                            this.recipients,
                            this.attachment);

            await SendProzess();
            return this.SuccessSending;
        }


        //------------------------------------------------------------------------ SendMailProzess 
        /// <summary>
        /// 
        /// </summary>
        private async Task SendProzess()
        {
            try
            {
                // E-Mail versenden
                var result = await mail.SendMailAsync();
                this.SuccessSending = result.Success;

                // Ergebnis verarbeiten
                if (result.Success)
                {
                    this.infoMessages.Add("Die E-Mail wurde erfolgreich versendet ✓");
                }
                else
                {
                    this.infoMessages.Add("Der E-Mailversand ist fehlgeschlagen ✗");
                    this.infoMessages.Add("Fehlermeldung:");
                    this.infoMessages.Add(result.Message);

                    // Detaillierte Fehlerbehandlung
                    if (result.SmtpStatusCode.HasValue)
                    {
                        string hinweis = mail.GetSmtpErrorHint(result.SmtpStatusCode);
                        this.infoMessages.Add($"SMTP-Hinweis: {hinweis}");
                    }

                    if (result.Exception is SocketException se)
                    {
                        string socketHint = mail.GetSocketErrorHint(se.SocketErrorCode);
                        this.infoMessages.Add($"Netzwerk-Hinweis: {socketHint}");
                    }
                    else if (result.Exception is FormatException)
                    {
                        this.infoMessages.Add("Ungültige E-Mail-Adresse – Format prüfen (z.B. name@domain.de)");
                    }
                    else if (result.Exception != null)
                    {
                        this.infoMessages.Add($"Exception: {result.Exception.GetType().Name}");
                        this.infoMessages.Add($"Message: {result.Exception.Message}");
                    }


                }
            }
            catch (Exception ex)
            {
                this.infoMessages.Add("Ergebnis: Allgemeiner Fehler ✗");
                this.infoMessages.Add("Exception/Error:");
                this.infoMessages.Add(ex.Message.ToString());
            }
            finally
            {
            }
        }
        /// <summary>
        /// Speichert aktuelle Mail-Credentials in verschlüsselter Datei
        /// </summary>
        public bool SaveCredentialsToFile(string profileName)
        {
            try
            {
                var manager = new MailCredentialsManager();
                var config = new MailCheckConfig
                {
                    Server = this.SMTPServer,
                    Port = this.SMTPPort,
                    Username = this.SMTPUser,
                    Password = this.SMTPPasswort,
                    MailFrom = this.MailFrom,
                    EnableSsl = this.SMTPSsl,
                    MailBCC = this.MailBCC
                };

                return manager.SaveCredentials(profileName, config);
            }
            catch (Exception ex)
            {
                infoMessages.Add($"Fehler beim Speichern der Credentials: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Lädt Mail-Credentials aus verschlüsselter Datei
        /// </summary>
        public bool LoadCredentialsFromFile(string profileName)
        {
            try
            {
                var manager = new MailCredentialsManager();
                var config = manager.LoadCredentials(profileName);

                if (config == null)
                {
                    infoMessages.Add($"Profil '{profileName}' nicht gefunden");
                    return false;
                }

                this.SMTPServer = config.Server;
                this.SMTPPort = config.Port;
                this.SMTPUser = config.Username;
                this.SMTPPasswort = config.Password;
                this.MailFrom = config.MailFrom;
                this.SMTPSsl = config.EnableSsl;
                this.MailBCC = config.MailBCC;

                return true;
            }
            catch (Exception ex)
            {
                infoMessages.Add($"Fehler beim Laden der Credentials: {ex.Message}");
                return false;
            }
        }

        /// <summary>
        /// Gibt alle verfügbaren Credential-Profile zurück
        /// </summary>
        public string[] GetCredentialProfiles()
        {
            var manager = new MailCredentialsManager();
            return manager.GetAllProfileNames();
        }
    }
}
