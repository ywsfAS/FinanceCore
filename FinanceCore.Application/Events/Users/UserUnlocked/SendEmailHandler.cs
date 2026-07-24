using FinanceCore.Application.Abstractions;
using FinanceCore.Domain.Events.User;
using MediatR;

namespace FinanceCore.Application.Events.Users.UserUnlocked
{
    public sealed class SendEmailHandler
        : INotificationHandler<UserAccountUnlockedEvent>
    {
        private readonly IEmailService _emailService;

        public SendEmailHandler(IEmailService emailService)
        {
            _emailService = emailService;
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

            await _emailService.SendEmailAsync(
                notification.Email,
                subject,
                body);
        }
    }
}