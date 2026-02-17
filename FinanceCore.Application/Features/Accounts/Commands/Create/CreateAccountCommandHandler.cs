using FinanceCore.Application.Abstractions;
using FinanceCore.Domain.Accounts;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Application.Features.Accounts.Commands.Create
{
    public class CreateAccountCommandHandler : IRequestHandler<CreateAccountCommand, Guid>
    {
        private readonly IAccountRepository _accountRepository;

        public CreateAccountCommandHandler(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task<Guid> Handle(CreateAccountCommand command, CancellationToken cancellationToken)
        {
            var account = Account.Create(
                command.UserId,
                command.Name,
                command.Type,
                command.Currency,
                command.InitialBalance);

            await _accountRepository.AddAsync(account, cancellationToken);

            return account.Id;
        }
    }
}
