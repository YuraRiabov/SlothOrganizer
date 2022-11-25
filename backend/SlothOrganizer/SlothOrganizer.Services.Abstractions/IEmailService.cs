using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SlothOrganizer.Services.Abstractions
{
    public interface IEmailService
    {
        Task SendEmail(string to, string subject, string message);
    }
}
