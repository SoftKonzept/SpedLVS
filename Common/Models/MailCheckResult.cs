using System;
using System.Net.Mail;

namespace Common.Models
{
    public class MailCheckResult
    {
        public bool Success { get; set; }
        public string Message { get; set; } = string.Empty;
        public Exception Exception { get; set; }
        public SmtpStatusCode? SmtpStatusCode { get; set; }
    }
}