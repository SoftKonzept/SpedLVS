using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using MimeKit.Text;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LVS.Mail
{
    /// <summary>
    /// Versendet E-Mails über IONOS SMTP via MailKit (.NET Framework 4.8).
    /// Account A = SMTP-Login (technischer Absender)
    /// Account B = From-Adresse (angezeigter Absender beim Empfänger)
    /// </summary>
    
    public class Mail
    {

        private readonly MailCredentials mailCredentials;
        private SmtpClient _client;
        private bool _disposed;

        public Mail()
        {

        }
        public Mail(MailCredentials myMailCredentials)
        {
            mailCredentials = myMailCredentials;
        }
        public Mail(clsSystem mySys)
        {
            mailCredentials = new MailCredentials
            {
                //SmtpHost = mySys.MailSmtpHost,
                //SmtpPort = mySys.MailSmtpPort,
                //SmtpUser = mySys.MailSmtpUser,
                //SmtpPassword = mySys.MailSmtpPassword,
                //SmtpDisplayName = mySys.MailSmtpDisplayName,
                //KeepAlive = false
            };
        }
        public Mail(string toEmail, string subject, string body,
                    bool isHtml = true, CancellationToken ct = default)
        {
            //mailCredentials = new MailCredentials
            //{
            //    SmtpHost = mySys.MailSmtpHost,
            //    SmtpPort = mySys.MailSmtpPort,
            //    SmtpUser = mySys.MailSmtpUser,
            //    SmtpPassword = mySys.MailSmtpPassword,
            //    SmtpDisplayName = mySys.MailSmtpDisplayName,
            //    KeepAlive = false
            //};
        }
        // -------------------------------------------------------------------------
        // Einfacher Versand – From = SMTP-Konto (Account A)
        // -------------------------------------------------------------------------
        //public async Task SendAsync(string toEmail, string subject, string body,
        //    bool isHtml = true, CancellationToken ct = default)
        //{
        //    var message = BuildMessage(
        //        fromEmail: mailCredentials.SmtpUser,
        //        fromName: mailCredentials.SmtpDisplayName,
        //        toEmail: toEmail,
        //        subject: subject,
        //        body: body,
        //        isHtml: isHtml);

        //    await SendMessageAsync(message, ct);
        //}

        // -------------------------------------------------------------------------
        // Versand mit abweichendem Absender (Account B)
        // From = customerEmail, SMTP-Login = Account A
        // -------------------------------------------------------------------------
        public async Task SendAsAsync(string toEmail, string subject, string body,
            string fromEmail, string fromName = "",
            bool isHtml = true, CancellationToken ct = default)
        {
            var message = BuildMessage(
                fromEmail: fromEmail,
                fromName: fromName,
                toEmail: toEmail,
                subject: subject,
                body: body,
                isHtml: isHtml);

            await SendMessageAsync(message, ct);
        }

        // -------------------------------------------------------------------------
        // Versand mit Reply-To (sicherste Option gegen Spamfilter)
        // From = Account A, Reply-To = Account B
        // -------------------------------------------------------------------------
        public async Task SendWithReplyToAsync(string toEmail, string subject, string body,
            string replyToEmail, string replyToName = "",
            bool isHtml = true, CancellationToken ct = default)
        {
            var message = BuildMessage(
                fromEmail: mailCredentials.SmtpUser,
                fromName: mailCredentials.SmtpDisplayName,
                toEmail: toEmail,
                subject: subject,
                body: body,
                isHtml: isHtml);

            message.ReplyTo.Add(new MailboxAddress(replyToName, replyToEmail));

            await SendMessageAsync(message, ct);
        }

        // -------------------------------------------------------------------------
        // Versand mit CC und BCC
        // -------------------------------------------------------------------------
        public Task SendWithCcBccAsync(string toEmail, string subject, string body,
              IEnumerable<string> ccEmails = null,
              IEnumerable<string> bccEmails = null,
              string fromEmail = null, string fromName = "",
              bool isHtml = true, CancellationToken ct = default(CancellationToken))
        {
            var message = BuildMessage(
                fromEmail: fromEmail ?? mailCredentials.SmtpUser,
                fromName: fromName,
                toEmail: toEmail,
                subject: subject,
                body: body,
                isHtml: isHtml);

            if (ccEmails != null)
                foreach (var cc in ccEmails)
                    message.Cc.Add(MailboxAddress.Parse(cc));

            if (bccEmails != null)
                foreach (var bcc in bccEmails)
                    message.Bcc.Add(MailboxAddress.Parse(bcc));

            return SendMessageAsync(message, ct);
        }

        // -------------------------------------------------------------------------
        // Versand mit Anhängen
        // -------------------------------------------------------------------------
        public Task SendWithAttachmentsAsync(string toEmail, string subject, string body,
            IEnumerable<string> attachmentPaths,
            string fromEmail = null, string fromName = "",
            bool isHtml = true, CancellationToken ct = default(CancellationToken))
        {
            var message = BuildMessage(
                fromEmail: fromEmail ?? mailCredentials.SmtpUser,
                fromName: fromName,
                toEmail: toEmail,
                subject: subject,
                body: body,
                isHtml: isHtml);

            var multipart = new Multipart("mixed");
            multipart.Add(message.Body);

            foreach (var path in attachmentPaths)
            {
                if (!File.Exists(path))
                    throw new FileNotFoundException("Anhang nicht gefunden: " + path);

                var attachment = new MimePart
                {
                    Content = new MimeContent(File.OpenRead(path)),
                    ContentDisposition = new ContentDisposition(ContentDisposition.Attachment),
                    ContentTransferEncoding = ContentEncoding.Base64,
                    FileName = Path.GetFileName(path)
                };
                multipart.Add(attachment);
            }

            message.Body = multipart;

            return SendMessageAsync(message, ct);
        }

        // -------------------------------------------------------------------------
        // Intern: MimeMessage aufbauen
        // -------------------------------------------------------------------------
        private static MimeMessage BuildMessage(string fromEmail, string fromName,
            string toEmail, string subject, string body, bool isHtml)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(fromName, fromEmail));
            message.To.Add(MailboxAddress.Parse(toEmail));
            message.Subject = subject;
            message.Body = new TextPart(isHtml ? TextFormat.Html : TextFormat.Plain)
            {
                Text = body
            };
            return message;
        }

        // -------------------------------------------------------------------------
        // Intern: Verbindung herstellen und Mail senden
        // -------------------------------------------------------------------------
        private async Task SendMessageAsync(MimeMessage message, CancellationToken ct)
        {
            var client = await GetConnectedClientAsync(ct);
            await client.SendAsync(message, ct);
        }

        // -------------------------------------------------------------------------
        // Intern: SMTP-Client mit optionalem Keep-Alive
        // -------------------------------------------------------------------------
        private async Task<SmtpClient> GetConnectedClientAsync(CancellationToken ct)
        {
            if (mailCredentials.KeepAlive)
            {
                if (_client == null || !_client.IsConnected || !_client.IsAuthenticated)
                {
                    if (_client == null)
                        _client = new SmtpClient();

                    await ConnectAsync(_client, ct).ConfigureAwait(false);
                }
                return _client;
            }

            var client = new SmtpClient();
            await ConnectAsync(client, ct).ConfigureAwait(false);
            return client;
        }

        private async Task ConnectAsync(SmtpClient client, CancellationToken ct)
        {
            var secureOption = mailCredentials.SmtpPort == 465
                ? SecureSocketOptions.SslOnConnect
                : SecureSocketOptions.StartTls;

            await client.ConnectAsync(mailCredentials.SmtpHost, mailCredentials.SmtpPort, secureOption, ct);
            await client.AuthenticateAsync(mailCredentials.SmtpUser, mailCredentials.SmtpPassword, ct);
        }

        // -------------------------------------------------------------------------
        // Dispose
        // -------------------------------------------------------------------------
        public async ValueTask DisposeAsync()
        {
            if (_client != null)
            {
                if (_client.IsConnected)
                    await _client.DisconnectAsync(true);
                _client.Dispose();
                _client = null;
            }
        }
    }
}
