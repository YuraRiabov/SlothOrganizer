using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MailKit.Security;

namespace SlothOrganizer.Services.Options
{
    public class SmtpOptions
    {
        public string HostAddress { get; set; } = string.Empty;

        public int Port { get; set; }

        public string Username { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public SecureSocketOptions SecureSocketOptions { get; set; } = SecureSocketOptions.Auto;

        public string SenderEmail { get; set; } = string.Empty;

        public string SenderName { get; set; } = string.Empty;
    }
}
