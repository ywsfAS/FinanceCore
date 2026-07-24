using FinanceCore.Application.Abstractions;
using FinanceCore.Domain.Events.User;
using MediatR;

namespace FinanceCore.Application.Events.Users.UserLocked
{
    public sealed class SendEmailHandler
        : INotificationHandler<UserAccountLockedEvent>
    {
        private readonly IEmailService _emailService;

        public SendEmailHandler(IEmailService emailService)
        {
            _emailService = emailService;
        }
        public async Task Handle( UserAccountLockedEvent notification, CancellationToken cancellationToken)
        {
            const string subject = "Your FinanceCore account has been temporarily locked";

            var unlockTime = notification.LockedUntil
                .ToString("f");

            var body = $"""
            Dear FinanceCore user,

            Your FinanceCore account has been temporarily locked due to multiple unsuccessful login attempts.

            Your account will be automatically unlocked on {unlockTime}. If you believe this activity was not performed by you, please contact our support team.

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