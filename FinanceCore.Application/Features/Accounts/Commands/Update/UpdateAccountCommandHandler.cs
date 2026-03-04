using FinanceCore.Application.Abstractions;
using MediatR;
using FinanceCore.Domain.Exceptions;
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
            var account = await _accountRepository.GetByIdAndUserIdAsync(command.Id , command.accountId, cancellationToken);

            if (account is null)
                throw new AccountNotFoundException(command.accountId);

            account.UpdateDetails(command.Name);

            await _accountRepository.UpdateAsync(account, cancellationToken);
        }
    }
}
