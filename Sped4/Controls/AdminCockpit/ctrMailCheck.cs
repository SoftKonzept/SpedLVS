using LVS;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net;
using System.Net.Mail;
using System.Net.Sockets;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Sped4.Controls.AdminCockpit
{
    public partial class ctrMailCheck : UserControl
    {
        // ── Log / Status ────────────────────────────────────────
        private ToolStripStatusLabel lblStatus;
        private ToolStripProgressBar progressBar;
        internal ctrMenu ctrMenu;

        public ctrMailCheck(ctrMenu myMenu)
        {
            InitializeComponent();
            ctrMenu = myMenu;
        }

        private void MailCheck_Load(object sender, EventArgs e)
        {
            // Designer-Control verwenden, nicht neu erstellen
            rtbLog.ReadOnly = true;
            rtbLog.BackColor = Color.FromArgb(30, 30, 30);
            rtbLog.ForeColor = Color.LightGreen;
            rtbLog.Font = new Font("Consolas", 9f);
            rtbLog.ScrollBars = RichTextBoxScrollBars.Vertical;

            // StatusBar-Label initialisieren
            lblStatus = new ToolStripStatusLabel
            {
                Text = "Bereit",
                Spring = true,
                TextAlign = ContentAlignment.MiddleLeft
            };
            statusStrip.Items.Add(lblStatus);

            // ProgressBar initialisieren
            progressBar = new ToolStripProgressBar
            {
                Visible = false,
                Style = ProgressBarStyle.Marquee
            };
            statusStrip.Items.Add(progressBar);
        }

        // ═══════════════════════════════════════════════════════
        //  Aktionen
        // ═══════════════════════════════════════════════════════

        private async void btnSmtpTest_Click(object sender, EventArgs e)
        {
            // Validierung
            if (string.IsNullOrWhiteSpace(tbServer.Text))
            {
                MessageBox.Show("Bitte Server eingeben.", "Pflichtfeld", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            SetBusy(true, "SMTP-Test läuft...");
            Log("── SMTP-Verbindungstest ─────────────────────────", Color.Cyan);

            // UI-Werte auslesen (vor Task.Run)
            string server = tbServer.Text.Trim();
            int port = (int)nudPort.Value;
            bool ssl = cbSSLTLS.Checked;
            bool useAuth = cbSmtpAuth.Checked;
            string user = useAuth ? tbUser.Text.Trim() : null;
            string pass = useAuth ? tbPass.Text : null;
            string mailFrom = tbMailFrom.Text.Trim();
            string mailTo = tbMailTo.Text.Trim();
            List<string> recipients = new List<string>();
            recipients.Add(mailTo);

            try
            {
                Log($"Server:  {server}:{port}", Color.White);
                Log($"SSL/TLS: {(ssl ? "JA" : "NEIN")}", Color.White);
                Log($"Auth:    {(useAuth ? "JA" : "NEIN")}", Color.White);
                Log(string.Empty);

                // Mail-Klasse instanziieren
                var mailChecker = new LVS.Mail.Mail(
                    server: server,
                    port: port,
                    mailFrom: mailFrom,
                    subject: "SMTP-Verbindungstest",
                    body: "Automatischer Test – bitte ignorieren.",
                    enableSsl: ssl,
                    username: user,
                    password: pass,
                    recipients: recipients,
                    attachments: null
                    );

                // SmtpTest() aufrufen
                var result = await mailChecker.SmtpTest();

                // Ergebnis verarbeiten
                if (result.Success)
                {
                    Log("Ergebnis: "+ result.Message  +" ✓", Color.LightGreen);
                    //Log("Ergebnis: SMTP-Test erfolgreich ✓", Color.LightGreen);
                    //Log(result.Message, Color.LightGreen);
                    SetStatus("SMTP-Test erfolgreich.");
                    //MessageBox.Show(
                    //    "SMTP-Verbindung funktioniert einwandfrei!\n\n" + result.Message,
                    //    "Info",
                    //    MessageBoxButtons.OK,
                    //    MessageBoxIcon.Information);
                    MessageBox.Show(
                                    result.Message,
                                    "Info",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Information);
                }
                else
                {
                    Log("Ergebnis: SMTP-Test fehlgeschlagen ✗", Color.OrangeRed);
                    Log($"Fehler: {result.Message}", Color.OrangeRed);
                    SetStatus("SMTP-Test fehlgeschlagen.");

                    // Fehlerbehandlung
                    if (result.SmtpStatusCode.HasValue)
                    {
                        string hinweis = mailChecker.GetSmtpErrorHint(result.SmtpStatusCode);
                        Log($"Hinweis: {hinweis}", Color.Yellow);
                    }

                    if (result.Exception is SocketException se)
                    {
                        string socketHint = mailChecker.GetSocketErrorHint(se.SocketErrorCode);
                        Log($"Netzwerk-Hinweis: {socketHint}", Color.Yellow);
                    }
                    else if (result.Exception != null)
                    {
                        Log($"Exception: {result.Exception.Message}", Color.Orange);
                    }
                }
            }
            catch (Exception ex)
            {
                Log("Ergebnis: Allgemeiner Fehler ✗", Color.OrangeRed);
                LogError("Fehler", ex);
                SetStatus("Fehler beim Test.");
            }
            finally
            {
                SetBusy(false);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private async void btnMailSend_Click(object sender, EventArgs e)
        {
            if (!Validieren()) return;

            SetBusy(true, "Sende E-Mail...");
            Log("── Mailversand ──────────────────────────────────", Color.Cyan);
            Log($"Server:  {tbServer.Text.Trim()}:{(int)nudPort.Value}  SSL={cbSSLTLS.Checked}");
            Log($"Von:     {tbMailFrom.Text.Trim()}");
            Log($"An:      {tbMailTo.Text.Trim()}");
            Log($"Betreff: {tbBetreff.Text.Trim()}");
            Log(string.Empty);

            // UI-Werte auslesen (vor async Operation)
            string server = tbServer.Text.Trim();
            int port = (int)nudPort.Value;
            bool ssl = cbSSLTLS.Checked;
            bool useAuth = cbSmtpAuth.Checked;
            string user = useAuth ? tbUser.Text.Trim() : null;
            string pass = useAuth ? tbPass.Text : null;
            string mailFrom = tbMailFrom.Text.Trim();
            string mailTo = tbMailTo.Text.Trim();
            string subject = tbBetreff.Text.Trim();
            string body = tbMessage.Text;

            List<string> recipients = new List<string>();
            recipients.Add(mailTo);

            try
            {
                // Mail-Klasse instanziieren
                var mailSender = new LVS.Mail.Mail(
                    server: server,
                    port: port,
                    mailFrom: mailFrom,
                    subject: subject,
                    body: body,
                    enableSsl: ssl,
                    username: user,
                    password: pass,
                    recipients: recipients,
                    attachments: null
                );

                // E-Mail versenden
                var result = await mailSender.SendMailAsync();

                // Ergebnis verarbeiten
                if (result.Success)
                {
                    string successMessage = "Ergebnis: Mail erfolgreich versendet ✓" + Environment.NewLine +
                                           "Die E-Mail wurde erfolgreich versendet!" + Environment.NewLine;
                    Log(successMessage, Color.LightGreen);
                    SetStatus("Mail versendet.");
                    MessageBox.Show(
                        successMessage + Environment.NewLine + result.Message,
                        "Erfolg",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information);
                }
                else
                {
                    Log("Ergebnis: Mailversand fehlgeschlagen ✗", Color.OrangeRed);
                    Log($"Fehler: {result.Message}", Color.OrangeRed);
                    SetStatus("Fehler beim Versand.");

                    // Detaillierte Fehlerbehandlung
                    if (result.SmtpStatusCode.HasValue)
                    {
                        string hinweis = mailSender.GetSmtpErrorHint(result.SmtpStatusCode);
                        Log($"SMTP-Hinweis: {hinweis}", Color.Yellow);
                    }

                    if (result.Exception is SocketException se)
                    {
                        string socketHint = mailSender.GetSocketErrorHint(se.SocketErrorCode);
                        Log($"Netzwerk-Hinweis: {socketHint}", Color.Yellow);
                    }
                    else if (result.Exception is FormatException)
                    {
                        Log("Ungültige E-Mail-Adresse – Format prüfen (z.B. name@domain.de)", Color.Yellow);
                    }
                    else if (result.Exception != null)
                    {
                        Log($"Exception: {result.Exception.GetType().Name}", Color.Orange);
                        Log($"Message: {result.Exception.Message}", Color.Orange);
                    }

                    MessageBox.Show(
                        $"Fehler beim Mailversand:\n\n{result.Message}",
                        "Fehler",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                Log("Ergebnis: Allgemeiner Fehler ✗", Color.OrangeRed);
                LogError("Fehler", ex);
                SetStatus("Fehler.");
            }
            finally
            {
                SetBusy(false);
            }
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLog_Click(object sender, EventArgs e)
        {
            rtbLog.Clear();
            Log("Log geleert.", Color.Yellow);
        }



        private void LogError(string titel, Exception ex)
        {
            Log($"{titel}: {ex.GetType().Name}", Color.OrangeRed);
            Log($"Meldung: {ex.Message}", Color.OrangeRed);
            if (ex.InnerException != null)
                Log($"Inner:   {ex.InnerException.Message}", Color.Orange);

            if (ex is SocketException se)
            {
                Log($"Socket-Fehlercode: {se.ErrorCode} ({se.SocketErrorCode})", Color.Yellow);
                string hinweis;
                switch (se.SocketErrorCode)
                {
                    case SocketError.ConnectionRefused:
                        hinweis = "Verbindung abgelehnt – Port geschlossen oder falsch.";
                        break;
                    case SocketError.HostNotFound:
                        hinweis = "DNS-Auflösung fehlgeschlagen – Hostname prüfen.";
                        break;
                    case SocketError.TimedOut:
                        hinweis = "Timeout – Firewall blockiert Port oder Server antwortet nicht.";
                        break;
                    case SocketError.NetworkUnreachable:
                        hinweis = "Netzwerk nicht erreichbar.";
                        break;
                    default:
                        hinweis = "Netzwerkfehler.";
                        break;
                }
                Log($"Hinweis: {hinweis}", Color.Yellow);
            }
        }


        // ═══════════════════════════════════════════════════════
        //  Hilfsmethoden
        // ═══════════════════════════════════════════════════════
        private bool Validieren()
        {
            if (string.IsNullOrWhiteSpace(tbServer.Text))
            { MessageBox.Show("Bitte Server eingeben.", "Pflichtfeld", MessageBoxButtons.OK, MessageBoxIcon.Warning); return false; }
            if (string.IsNullOrWhiteSpace(tbMailFrom.Text))
            { MessageBox.Show("Bitte Absender eingeben.", "Pflichtfeld", MessageBoxButtons.OK, MessageBoxIcon.Warning); return false; }
            if (string.IsNullOrWhiteSpace(tbMailTo.Text))
            { MessageBox.Show("Bitte Empfänger eingeben.", "Pflichtfeld", MessageBoxButtons.OK, MessageBoxIcon.Warning); return false; }
            return true;
        }

        private void Log(string text, Color? color = null)
        {
            if (rtbLog.InvokeRequired) { rtbLog.Invoke(new Action(() => Log(text, color))); return; }
            string line = $"[{DateTime.Now:HH:mm:ss}] {text}{Environment.NewLine}";
            rtbLog.SelectionStart = rtbLog.TextLength;
            rtbLog.SelectionLength = 0;
            rtbLog.SelectionColor = color ?? Color.LightGreen;
            rtbLog.AppendText(line);
            rtbLog.ScrollToCaret();
        }

        private void SetStatus(string text)
        {
            if (statusStrip.InvokeRequired)
            {
                statusStrip.Invoke(new Action(() => SetStatus(text)));
                return;
            }
            if (lblStatus != null)
                lblStatus.Text = text;
        }
        private void SetBusy(bool busy, string status = "Bereit")
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => SetBusy(busy, status)));
                return;
            }
            btnMailSend.Enabled = !busy;
            btnSmtpTest.Enabled = !busy;
            if (progressBar != null)
                progressBar.Visible = busy;
            if (lblStatus != null)
                lblStatus.Text = status;
        }

        private void btnMaildaten_Click(object sender, EventArgs e)
        {
            if (this.ctrMenu._frmMain.system.Client is clsClient)
            {
                tbServer.Text = this.ctrMenu._frmMain.system.Client.Modul.Mail_SMTPServer;
                nudPort.Value = this.ctrMenu._frmMain.system.Client.Modul.Mail_SMTPPort;
                cbSSLTLS.Checked = this.ctrMenu._frmMain.system.Client.Modul.Mail_SMTPSSL;

                cbSmtpAuth.Checked = !string.IsNullOrWhiteSpace(this.ctrMenu._frmMain.system.Client.Modul.Mail_SMTPUser) &&
                                     !string.IsNullOrWhiteSpace(this.ctrMenu._frmMain.system.Client.Modul.Mail_SMTPPasswort);

                tbUser.Text = this.ctrMenu._frmMain.system.Client.Modul.Mail_SMTPUser;
                tbPass.Text = this.ctrMenu._frmMain.system.Client.Modul.Mail_SMTPPasswort;

                tbMailFrom.Text = this.ctrMenu._frmMain.system.Client.Modul.Mail_MailAdress;
                tbMailTo.Text = "support@softkonzept.com";
                tbBetreff.Text = "Testmail aus AdminCockpit";
                tbMessage.Text = "Dies ist eine Testmail aus dem AdminCockpit.";
            }
        }

        private void btnSKData_Click(object sender, EventArgs e)
        {
            tbServer.Text = "smtp.ionos.de";
            nudPort.Value = 587;
            cbSSLTLS.Checked = true;

            cbSmtpAuth.Checked = true;

            tbUser.Text = "support@softkonzept.com";
            tbPass.Text = "!29suPP%1Ay33e&fcdW";

            tbMailFrom.Text = "support@softkonzept.com";
            tbMailTo.Text = "info@softkonzept.com";
            tbBetreff.Text = "Testmail aus AdminCockpit";
            tbMessage.Text = "Dies ist eine Testmail aus dem AdminCockpit.";
        }
    }
}
