using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;

namespace AppointmentSystem.Web.EmailService
{
    public class MailServiceSender : IMailService
    {
        public async Task SendAsync(string email, string subject, string body)
        {
            //If you want to recieve mails, you should make a registration in mailtrap.io and copy - paste the Credentials
            var client = new SmtpClient
            {
                Host = "smtp.mailtrap.io",
                Port = 2525,
                Credentials = new NetworkCredential("661f7dac80d6e9", "5e45d98a1d9be1"),
                EnableSsl = true,
            };

            var from = new MailAddress("georgigerdjikov033@gmail.com", "My Awesome Admin");
            var to = new MailAddress(email);

            using (var mail = new MailMessage(from, to)
            {
                Subject = subject,
                Body = body,
                IsBodyHtml = true
            })
            {
                await client.SendMailAsync(mail);
            }
        }
    }
}
