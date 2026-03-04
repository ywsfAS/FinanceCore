using FinanceCore.Application.Abstractions;
using FinanceCore.Domain.Common;
using FinanceCore.Domain.Events.User;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Application.Events.Users.UserCreated
{
    public class SendEmailHandler : INotificationHandler<UserCreatedEvent>
    {
        private readonly IEmailService _emailService;
        public SendEmailHandler(IEmailService emailService) { 
            _emailService = emailService;
        }
        public async Task Handle(UserCreatedEvent notification, CancellationToken cancellationToken)
        {
            var Email = new Email(notification.Email);
            var Subject = "Welcome to FinanceCore!";
            var Body = $"Dear {notification.Name},\n\nWelcome to FinanceCore! We're excited to have you on board. If you have any questions or need assistance, feel free to reach out to our support team.\n\nBest regards,\nThe FinanceCore Team";
            await _emailService.SendEmailAsync(Email,Subject,Body);
        }
    }
}
