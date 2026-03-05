using FinanceCore.Application.Abstractions;
using FinanceCore.Domain.Exceptions;
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
            var Isaccount = await _accountRepository.IsExists(command.UserId,command.Id,cancellationToken);

            if (!Isaccount)
                throw new AccountNotFoundException(command.Id);

            await _accountRepository.DeleteAsync(command.Id, cancellationToken);
        }
    }
}
