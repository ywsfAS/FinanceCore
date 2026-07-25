using FinanceCore.Application.Abstractions;
using FinanceCore.Domain.Common;
using FinanceCore.Domain.Events.User;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FinanceCore.Application.Events.Users.UserCreated
{
    public class SendEmailHandler : INotificationHandler<UserCreatedEvent>
    {
        private readonly IEmailService _emailService;
        private readonly ILogger<SendEmailHandler> _logger;
        public SendEmailHandler(IEmailService emailService , ILogger<SendEmailHandler> logger) { 
            _emailService = emailService;
            _logger = logger;
        }
        public async Task Handle(UserCreatedEvent notification, CancellationToken cancellationToken)
        {
            var Email = new Email(notification.Email);
            var Subject = "Welcome to FinanceCore!";
            var Body = $"Dear {notification.Name},\n\nWelcome to FinanceCore! We're excited to have you on board. If you have any questions or need assistance, feel free to reach out to our support team.\n\nBest regards,\nThe FinanceCore Team";
            try
            {
                await _emailService.SendEmailAsync(Email,Subject,Body);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex,"Failed to send email to {userId}",notification.UserId); 
            }
        }
    }
}
