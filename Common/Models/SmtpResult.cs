using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;

namespace Common.Models
{
    public class SmtpResult
    {
        public SmtpResult() 
        { 
        }

        public bool Success { get; set; } = false;
        public SmtpStatusCode StatusCode { get; set; }
        public string RawResponse { get; set; } = string.Empty;
        public DateTime TimestampUtc { get; set; } = DateTime.UtcNow;
    }
}
