using Common.Models;
using LVS;
using LVS.Mail;
using LVS.ViewData;
using System;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Forms;

namespace Sped4.Controls.ToDo
{
    public partial class ctrUserlistMailCredentials : UserControl
    {
        public Globals._GL_USER GL_User { get; set; }
        internal UsersViewData usersViewData { get; set; }

        public ctrUserlistMailCredentials(Globals._GL_USER myGL_User)
        {
            InitializeComponent();
            GL_User = myGL_User;
        }
        private void AppendLog(string text)
        {
            try
            {
                if (tbLog.InvokeRequired)
                {
                    tbLog.Invoke(new Action<string>(AppendLog), text);
                    return;
                }

                tbLog.AppendText($"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] {text}{Environment.NewLine}");
            }
            catch
            {
                // best-effort
            }
        }

        private void SetProgress(int value, int maximum)
        {
            try
            {
                if (tbProcessStatus.InvokeRequired)
                {
                    tbProcessStatus.Invoke(new Action<int, int>(SetProgress), value, maximum);
                    return;
                }

                int max = Math.Max(1, maximum);
                tbProcessStatus.Maximum = max;
                tbProcessStatus.Value = Math.Min(max, Math.Max(0, value));
            }
            catch
            {
                // ignore progress errors
            }
        }

        private string SanitizeProfileName(string raw)
        {
            if (string.IsNullOrWhiteSpace(raw)) return "profile";
            return Regex.Replace(raw, @"[^A-Za-z0-9_\-]", "_");
        }

        private void btnCreateCredentials_Click_1(object sender, EventArgs e)
        {
            btnCreateCredentials.Enabled = false;
            tbLog.Clear();
            SetProgress(0, 1);

            try
            {
                usersViewData = new UsersViewData(this.GL_User);
                try
                {
                    usersViewData.GetUsersList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Fehler beim Lesen der Benutzerdaten: " + ex.Message, "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }

                if (usersViewData.ListUsers == null || usersViewData.ListUsers.Count == 0)
                {
                    AppendLog("Keine Benutzer gefunden.");
                    return;
                }

                SetProgress(0, usersViewData.ListUsers.Count);
                int processed = 0;
                foreach (var user in usersViewData.ListUsers)
                {
                    AppendLog($"Benutzer: {user.LoginName} ({user.Id})");
                    try
                    {
                        processed++;
                        // ID parsen
                        decimal decId = 0;

                        string login = (user.LoginName ?? $"user_{user.Id}").Trim();
                        AppendLog($"Verarbeite Benutzer: {login} ({user.Id})");

                        // Felder aus dem Model entnehmen (Existenz der Properties vorausgesetzt)
                        string smtpServer = (user.SMTPServer ?? string.Empty).Trim();
                        int smtpPort = user.SMTPPort <= 0 ? clsUser.Default_SMTPPort : user.SMTPPort;
                        string smtpUser = (user.SMTPUser ?? string.Empty).Trim();
                        string smtpPass = (user.SMTPPasswort ?? string.Empty);
                        string mailFrom = (user.Mail ?? string.Empty).Trim();
                        bool smtpSsl = user.SMTPSSL;

                        // Validierung
                        var errors = new System.Collections.Generic.List<string>();
                        if (string.IsNullOrWhiteSpace(smtpServer)) errors.Add("SMTP-Server fehlt");
                        if (string.IsNullOrWhiteSpace(mailFrom)) errors.Add("E-Mail fehlt");
                        if (smtpPort < 1 || smtpPort > 65535) errors.Add("SMTP-Port ungültig");
                        if (!string.IsNullOrWhiteSpace(smtpUser) && string.IsNullOrWhiteSpace(smtpPass)) errors.Add("SMTP-Passwort fehlt");
                        if (errors.Count > 0)
                        {
                            AppendLog($"{login} ({user.Id}): übersprungen - {string.Join(", ", errors)}");
                            SetProgress(processed, usersViewData.ListUsers.Count);
                            continue;
                        }

                        try
                        {
                            var cfg = new MailCheckConfig
                            {
                                Server = smtpServer,
                                Port = smtpPort,
                                Username = smtpUser,
                                Password = smtpPass,
                                MailFrom = mailFrom,
                                EnableSsl = smtpSsl,
                                MailBCC = string.Empty
                            };

                            var rawProfile = $"user_{(int)user.Id}_{login}";
                            var profileName = SanitizeProfileName(rawProfile);

                            // Erzeuge verschlüsselte XML-Bytes direkt im Speicher (keine Temp-Datei)
                            var mgr = new MailCredentialsManager();
                            byte[] credBytes = mgr.CreateEncryptedCredentialsBytes(profileName, cfg);

                            //byte[] credBytes = mgr.CreateEncryptedCredentialsBytes(profileName, cfg);
                            if (credBytes == null || credBytes.Length == 0)
                            {
                                AppendLog($"{login} ({user.Id}): Fehler beim Erzeugen der verschlüsselten Bytes.");
                                SetProgress(processed, usersViewData.ListUsers.Count);
                                continue;
                            }

                            string strAddition = $"{user.Id}_{user.Vorname}{user.Name}";
                            string fileName = MailCredentialsManager.GetDefaultCredentialsFileName(strAddition);

                            var dbUser = new clsUser((int)user.Id);
                            dbUser._GL_User = this.GL_User;
                            bool okDbSave = false;
                            try
                            {
                                okDbSave = dbUser.SaveMailCredentialsToUser(credBytes, fileName);
                            }
                            catch (Exception exDb)
                            {
                                AppendLog($"{login} ({user.Id}): DB-Speichern fehlgeschlagen - {exDb.Message}");
                            }

                            if (okDbSave)
                            {
                                AppendLog($"{login} ({user.Id}): Credentials erzeugt und in DB gespeichert ({fileName}).");
                            }
                            else
                            {
                                AppendLog($"{login} ({user.Id}): Fehler beim Speichern in DB.");
                            }
                        }
                        catch (Exception exInner)
                        {
                            AppendLog($"{login} ({user.Id}): Fehler beim Erstellen/Speichern - {exInner.Message}");
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Fehler beim Lesen der Benutzerdaten: " + ex.Message, "Fehler", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                }
                AppendLog("Verarbeitung abgeschlossen.");
            }
            finally
            {
                btnCreateCredentials.Enabled = true;
                SetProgress(tbProcessStatus.Maximum, tbProcessStatus.Maximum);
            }
        }
    }
    
}
