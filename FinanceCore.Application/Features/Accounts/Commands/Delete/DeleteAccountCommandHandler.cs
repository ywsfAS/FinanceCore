using FinanceCore.Application.Abstractions;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Application.Features.Accounts.Commands.Delete
{
    public class DeleteAccountCommandHandler : IRequestHandler<DeleteAccountCommand>
    {
        private readonly IAccountRepository _accountRepository;

        public DeleteAccountCommandHandler(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task Handle(DeleteAccountCommand command, CancellationToken cancellationToken)
        {
            var account = await _accountRepository.GetByIdAndUserIdAsync(command.UserId,command.Id, cancellationToken);

            if (account is null)
                throw new InvalidOperationException("Account not found.");

            await _accountRepository.DeleteAsync(account, cancellationToken);
        }
    }
}
