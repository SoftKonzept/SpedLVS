using System;
using System.Collections.Generic;
using System.Net.Mail;
using System.Text;

namespace Common.Models
{

    public class SmtpRecipientStatusResult
    {
        public string Recipient { get; set; } = string.Empty;
        public bool Success { get; set; }
        public SmtpStatusCode StatusCode { get; set; }
        public string RawResponse { get; set; } = string.Empty;
    }

}
