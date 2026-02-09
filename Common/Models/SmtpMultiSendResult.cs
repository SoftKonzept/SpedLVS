using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Common.Models
{
    public class SmtpMultiSendResult
    {
        public List<SmtpRecipientStatusResult> Results { get; set; } = new List<SmtpRecipientStatusResult>();
        public bool AllSucceeded => Results.All(r => r.Success);
    }

}
