using FinanceCore.Application.Abstractions;
using FinanceCore.Domain.Enums;
using FinanceCore.Domain.Events.User;
using MediatR;
using FinanceCore.Domain.Accounts;
using FinanceCore.Domain.Common;
using Microsoft.Extensions.Logging;
namespace FinanceCore.Application.Events.Users.UserCreated
{
    public class DefaultAccountHandler : INotificationHandler<UserCreatedEvent>
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IMediator _eventBus;
        private readonly ILogger<DefaultAccountHandler> _logger;
        public DefaultAccountHandler(IAccountRepository accountRepository, IMediator eventBus, ILogger<DefaultAccountHandler> logger)
        {
            _accountRepository = accountRepository;
            _eventBus = eventBus;
            _logger = logger;
        }
        public async Task Handle(UserCreatedEvent notification, CancellationToken cancellationToken)
        {
            var account = Account.Create(
                notification.UserId,
                "Default Account",
                EnAccountType.Cash,
                Money.Zero(EnCurrency.USD));
            try
            {
                await _accountRepository.AddAsync(account, cancellationToken);
                await DomainEventDispatcher.DispatchAsync(_eventBus, account, cancellationToken);
            }
            catch(Exception ex)
            {
                _logger.LogCritical(ex,"Failed to create default account for user : {userId} {email}",notification.UserId,notification.Email);
                throw;
            }
        }
    }
}
