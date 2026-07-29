using System;
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

            //--- Log Formating
            rtbLog = new RichTextBox
            {
                Dock = DockStyle.Fill,
                ReadOnly = true,
                BackColor = Color.FromArgb(30, 30, 30),
                ForeColor = Color.LightGreen,
                Font = new Font("Consolas", 9f),
                ScrollBars = RichTextBoxScrollBars.Vertical
            };
        }

        private void SetCredentialValues()
        {

            tbServer.Text = "slegmbh-de0i.mail.protection.outlook.com";

            cbSmtpAuth.Checked = false;
            cbSSLTLS.Checked = true;
            nudPort.Value = 25;

            tbUser.Text = "";
            tbPass.Text = "";

            tbMailFrom.Text = "noreply@sle-gmbh.de";
            tbMailTo.Text = "info@softkonzept.com";
            tbBetreff.Text = "Testmail aus AdminCockpit";
            tbMessage.Text = "Dies ist eine Testmail aus dem AdminCockpit.";
        }
        // ═══════════════════════════════════════════════════════
        //  Aktionen
        // ═══════════════════════════════════════════════════════

        private async void btnSmtpTest_Click(object sender, EventArgs e)
        {
            // Fix: Validierung auch hier, damit kein ArgumentException bei leerem Servernamen
            if (string.IsNullOrWhiteSpace(tbServer.Text))
            {
                MessageBox.Show("Bitte Server eingeben.", "Pflichtfeld", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            string server = tbServer.Text.Trim();
            int port = (int)nudPort.Value;

            SetBusy(true, "Verbindungstest läuft...");
            Log("── Verbindungstest ──────────────────────────────", Color.Cyan);
            Log($"Ziel:  {server}:{port}");

            try
            {
                TcpClient tcp = new System.Net.Sockets.TcpClient();
                try
                {
                    var connectTask = tcp.ConnectAsync(server, port);
                    var winner = await System.Threading.Tasks.Task.WhenAny(
                        connectTask, System.Threading.Tasks.Task.Delay(5000));

                    if (winner == connectTask)
                    {
                        // Fix: gefaulte Task explizit prüfen – WhenAny wirft selbst keine Exception
                        if (connectTask.IsFaulted)
                        {
                            Exception inner = connectTask.Exception?.InnerException ?? connectTask.Exception;
                            if (inner != null)
                                throw inner;
                            else
                                throw new Exception("Unbekannter Verbindungsfehler.");
                        }
                        Log("TCP-Verbindung:  OK ✓", Color.LightGreen);
                        SetStatus("TCP-Verbindung erfolgreich.");
                    }
                    else
                    {
                        Log("TCP-Verbindung:  TIMEOUT nach 5 s ✗", Color.OrangeRed);
                        SetStatus("Timeout.");
                    }
                }
                finally
                {
                    if (tcp != null)
                        tcp.Dispose();
                }
            }
            catch (Exception ex)
            {
                LogError("TCP-Fehler", ex);
                SetStatus("Verbindung fehlgeschlagen.");
            }
            finally { SetBusy(false); }
        }

        private async void btnMailSend_Click(object sender, EventArgs e)
        {
            if (!Validieren()) return;

            SetBusy(true, "Sende E-Mail...");
            Log("── Mailversand ──────────────────────────────────", Color.Cyan);
            Log($"Server:  {tbServer.Text.Trim()}:{(int)nudPort.Value}  SSL={cbSSLTLS.Checked}");
            Log($"Von:     {tbMailFrom.Text.Trim()}");
            Log($"An:      {tbMailTo.Text.Trim()}");
            Log($"Betreff: {tbBetreff.Text.Trim()}");

            // UI-Werte VOR Task.Run auslesen (nur UI-Thread darf auf Controls zugreifen)
            string server = tbServer.Text.Trim();
            int port = (int)nudPort.Value;
            bool ssl = cbSSLTLS.Checked;
            bool useAuth = cbSmtpAuth.Checked;
            string user = tbUser.Text.Trim();
            string pass = tbPass.Text;
            string von = tbMailFrom.Text.Trim();
            string an = tbMailTo.Text.Trim();
            string betreff = tbBetreff.Text.Trim();
            string body = rtbLog.Text;

            try
            {
                await System.Threading.Tasks.Task.Run(() =>
                {
                    using (var client = new SmtpClient(server, port))
                    {
                        client.EnableSsl = ssl;
                        client.Timeout = 15000;
                        client.DeliveryMethod = SmtpDeliveryMethod.Network;
                        client.UseDefaultCredentials = false;

                        if (useAuth)
                            client.Credentials = new NetworkCredential(user, pass);

                        MailMessage mail = new MailMessage();
                        try
                        {
                            mail.From = new MailAddress(von);
                            mail.Subject = betreff;
                            mail.Body = body;
                            mail.IsBodyHtml = false;
                            mail.To.Add(an);

                            client.Send(mail);
                        }
                        finally
                        {
                            if (mail != null)
                                mail.Dispose();
                        }
                    }
                });

                Log("Ergebnis: Mail erfolgreich versendet ✓", Color.LightGreen);
                SetStatus("Mail versendet.");
                MessageBox.Show("Die Testmail wurde erfolgreich versendet!", "Erfolg",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (SmtpException ex)
            {
                Log("Ergebnis: SMTP-Fehler ✗", Color.OrangeRed);
                LogSmtpError(ex);
                SetStatus("Fehler beim Senden.");
            }
            catch (FormatException ex)
            {
                Log("Ergebnis: Ungültige E-Mail-Adresse ✗", Color.OrangeRed);
                Log($"Meldung: {ex.Message}", Color.OrangeRed);
                Log("Hinweis: Absender- und Empfängeradresse auf korrektes Format prüfen (z.B. name@domain.de).", Color.Yellow);
                SetStatus("Ungültige E-Mail-Adresse.");
            }
            catch (Exception ex)
            {
                Log("Ergebnis: Allgemeiner Fehler ✗", Color.OrangeRed);
                LogError("Fehler", ex);
                SetStatus("Fehler.");
            }
            finally { SetBusy(false); }
        }

        private void btnLog_Click(object sender, EventArgs e)
        {

        }

        // ═══════════════════════════════════════════════════════
        //  Fehler-Logging
        // ═══════════════════════════════════════════════════════
        private void LogSmtpError(SmtpException ex)
        {
            Log($"SMTP-StatusCode:  {ex.StatusCode} ({(int)ex.StatusCode})", Color.OrangeRed);
            Log($"Meldung:          {ex.Message}", Color.OrangeRed);

            // Hinweise je nach StatusCode
            string hinweis;
            switch (ex.StatusCode)
            {
                case SmtpStatusCode.ServiceNotAvailable:
                    hinweis = "Server nicht erreichbar oder Dienst deaktiviert.";
                    break;
                case SmtpStatusCode.MailboxUnavailable:
                    hinweis = "Absender-Adresse nicht zugelassen oder Postfach gesperrt.";
                    break;
                case SmtpStatusCode.ClientNotPermitted:
                    hinweis = "IP-Adresse nicht in Connector-Whitelist eingetragen.";
                    break;
                case SmtpStatusCode.MustIssueStartTlsFirst:
                    hinweis = "Server erwartet STARTTLS – SSL/TLS aktivieren.";
                    break;
                case SmtpStatusCode.CommandNotImplemented:
                    hinweis = "Befehl nicht unterstützt. Port oder SSL-Einstellung prüfen.";
                    break;
                case SmtpStatusCode.TransactionFailed:
                    hinweis = "Transaktion abgebrochen. Absender/Empfänger prüfen.";
                    break;
                case SmtpStatusCode.GeneralFailure:
                    hinweis = "Allgemeiner SMTP-Fehler. Serverlog prüfen.";
                    break;
                default:
                    hinweis = "Unbekannter SMTP-Statuscode.";
                    break;
            }
            Log($"Hinweis:          {hinweis}", Color.Yellow);

            if (ex.InnerException != null)
                Log($"Inner:            {ex.InnerException.Message}", Color.Orange);

            Log("── Mögliche Ursachen ────────────────────────────", Color.Gray);
            Log("  • IP des Absenders nicht im Exchange-Connector hinterlegt", Color.Gray);
            Log("  • Port 25 durch Firewall/Provider blockiert", Color.Gray);
            Log("  • SSL-Einstellung passt nicht zum Port (25=kein SSL, 587=STARTTLS)", Color.Gray);
            Log("  • Absender-Domain nicht als akzeptierte Domain konfiguriert", Color.Gray);
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
            if (statusStrip.InvokeRequired) { statusStrip.Invoke(new Action(() => SetStatus(text))); return; }
            lblStatus.Text = text;
        }

        private void SetBusy(bool busy, string status = "Bereit")
        {
            if (InvokeRequired) { Invoke(new Action(() => SetBusy(busy, status))); return; }
            btnMailSend.Enabled = !busy;
            btnSmtpTest.Enabled = !busy;
            progressBar.Visible = busy;
            progressBar.Style = busy ? ProgressBarStyle.Marquee : ProgressBarStyle.Blocks;
            lblStatus.Text = status;
        }

        // ── Control-Helfer ──────────────────────────────────────
        //private static Label MakeLabel(string text, int x, int y) =>
        //    new Label { Text = text, Location = new Point(x, y + 3), AutoSize = true };

        //private static TextBox MakeTextBox(int x, int y, int w, string def,
        //    bool password = false, bool enabled = true) =>
        //    new TextBox
        //    {
        //        Location = new Point(x, y),
        //        Width = w,
        //        Text = def,
        //        UseSystemPasswordChar = password,
        //        Enabled = enabled
        //    };
    }
}
