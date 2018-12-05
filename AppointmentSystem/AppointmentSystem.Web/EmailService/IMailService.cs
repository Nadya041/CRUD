using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppointmentSystem.Web.EmailService
{
    public interface IMailService
    {
        Task SendAsync(string email, string subject, string body);
    }
}
