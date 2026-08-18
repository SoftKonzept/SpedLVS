using System.Collections.Generic;

namespace Common.Models
{
    public class MailCheckConfig
    {
        public string Server { get; set; } = string.Empty;
        public int Port { get; set; } = 587;
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public bool EnableSsl { get; set; } = true;
        public string MailFrom { get; set; } = string.Empty;
        public List<string> MailTo { get; set; } = new List<string>();
        public string MailBCC { get; set; } = string.Empty;
        public string Subject { get; set; } = string.Empty;
        public string Body { get; set; } = string.Empty;
        public int TimeoutMs { get; set; } = 15000;
    }
}