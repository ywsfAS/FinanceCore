using FinanceCore.Application.Abstractions;
using FinanceCore.Domain.Common;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Infrastructure.Services
{
    public class EmailService : IEmailService
    {
        private readonly EmailSettings _emailSettings;
        public EmailService(IOptions<EmailSettings> settings) { 
            _emailSettings = settings.Value;
        }
        public async Task SendEmailAsync(Email email , string subject , string body )
        {
            var message = new MailMessage();
            message.To.Add(email.Address);
            message.Subject = subject;
            message.Body = body;
            message.From = new MailAddress(_emailSettings.Username);

            var client = new SmtpClient(_emailSettings.Host);
            client.Port = _emailSettings.Port;
            client.EnableSsl = _emailSettings.EnableSsl;
            client.Credentials = new NetworkCredential(_emailSettings.Username, _emailSettings.Password);

            await client.SendMailAsync(message);
        
        }
    }
}
