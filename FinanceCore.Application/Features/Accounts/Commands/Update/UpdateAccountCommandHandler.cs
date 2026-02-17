using FinanceCore.Application.Abstractions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Application.Features.Accounts.Commands.Update
{
    public class UpdateAccountCommandHandler : IRequestHandler<UpdateAccountCommand>
    {
        private readonly IAccountRepository _accountRepository;

        public UpdateAccountCommandHandler(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task Handle(UpdateAccountCommand command, CancellationToken cancellationToken)
        {
            var account = await _accountRepository.GetByIdAsync(command.Id, cancellationToken);

            if (account is null)
                throw new InvalidOperationException("Account not found.");

            account.UpdateDetails(command.Name);

            await _accountRepository.UpdateAsync(account, cancellationToken);
        }
    }
}
