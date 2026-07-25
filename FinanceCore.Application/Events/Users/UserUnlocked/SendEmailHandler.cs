using FinanceCore.Application.Abstractions;
using FinanceCore.Domain.Events.User;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FinanceCore.Application.Events.Users.UserUnlocked
{
    public sealed class SendEmailHandler
        : INotificationHandler<UserAccountUnlockedEvent>
    {
        private readonly IEmailService _emailService;
        private readonly ILogger<SendEmailHandler> _logger;
        public SendEmailHandler(IEmailService emailService, ILogger<SendEmailHandler> logger)
        {
            _emailService = emailService;
            _logger = logger;
        }

        public async Task Handle(
            UserAccountUnlockedEvent notification,
            CancellationToken cancellationToken)
        {
            const string subject = "Your FinanceCore account has been unlocked";

            var body = $"""
                Dear FinanceCore user,

                Your account has been successfully unlocked, and you can now log in again.

                If you did not request this or believe this action was made in error, please contact our support team.

                Best regards,
                The FinanceCore Team
                """;
            try
            {
                await _emailService.SendEmailAsync(
                    notification.Email,
                    subject, body);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex,"Failed to send email to {email}",notification.Email.Address); 
            }
        }
    }
}