using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Net.Sockets;
using System.Threading.Tasks;

namespace LVS.Mail
{
    public class MailCheckResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public Exception Exception { get; set; }
        public SmtpStatusCode? SmtpStatusCode { get; set; }
    }
}
