using FinanceCore.Application.Abstractions;
using FinanceCore.Application.DTOs;
using FinanceCore.Application.Events;
using FinanceCore.Domain.Accounts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Application.Features.Accounts.Commands.Create
{
    public class CreateAccountCommandHandler : IRequestHandler<CreateAccountCommand, AccountDto>
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IMediator _eventBus;

        public CreateAccountCommandHandler(IAccountRepository accountRepositorsy,IMediator eventBus)
        {
            _accountRepository = accountRepositorsy;
            _eventBus = eventBus;
        }

        public async Task<AccountDto> Handle(CreateAccountCommand command, CancellationToken cancellationToken)
        {
            var account = Account.Create(
                command.UserId,
                command.Name,
                command.Type,
                command.Currency,
                command.InitialBalance);

            await _accountRepository.AddAsync(account, cancellationToken);

            await DomainEventDispatcher.DispatchAsync(_eventBus,account, cancellationToken);
  
            return new AccountDto(account.Id,account.UserId,account.Name,account.Type,account.Balance,account.Currency,account.CreatedAt);
        }
    }
}
