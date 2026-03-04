using FinanceCore.Domain.Events.Account;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FinanceCore.Application.Events.Accounts
{
    public class AccountCreatedEventHandler
        : INotificationHandler<AccountCreated>
    {
        private readonly ILogger<AccountCreatedEventHandler> _logger;

        public AccountCreatedEventHandler(
            ILogger<AccountCreatedEventHandler> logger)
        {
            _logger = logger;
        }

        public async Task Handle(
            AccountCreated notification,
            CancellationToken cancellationToken)
        {
            _logger.LogInformation(
                "Account created: {AccountId} for {Name}",
                notification.AccountId,
                notification.Name);

            await Task.CompletedTask;
        }
    }
}
