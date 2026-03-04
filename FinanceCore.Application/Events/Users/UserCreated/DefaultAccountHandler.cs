using FinanceCore.Application.Abstractions;
using FinanceCore.Application.DTOs;
using FinanceCore.Application.Features.Accounts.Commands.Create;
using FinanceCore.Domain.Enums;
using FinanceCore.Domain.Events.User;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FinanceCore.Domain.Accounts;
namespace FinanceCore.Application.Events.Users.UserCreated
{
    public class DefaultAccountHandler : INotificationHandler<UserCreatedEvent>
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IMediator _eventBus;
        public DefaultAccountHandler(IAccountRepository accountRepository, IMediator eventBus)
        {
            _accountRepository = accountRepository;
            _eventBus = eventBus;
        }
        public async Task Handle(UserCreatedEvent notification, CancellationToken cancellationToken)
        {
            var command = new CreateAccountCommand(notification.UserId,"DefaultAccount", EnAccountType.Cash , EnCurrency.USD);
            var account = Account.Create(
                command.UserId,
                command.Name,
                command.Type,
                command.Currency,
                command.InitialBalance);

            await _accountRepository.AddAsync(account, cancellationToken);

            await DomainEventDispatcher.DispatchAsync(_eventBus, account, cancellationToken);

        }
    }
}
