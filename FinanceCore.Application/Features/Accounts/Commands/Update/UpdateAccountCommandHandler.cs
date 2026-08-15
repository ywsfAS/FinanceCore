using FinanceCore.Application.Abstractions;
using FinanceCore.Application.Events;
using FinanceCore.Domain.Enums;
using FinanceCore.Domain.Exceptions;
using MediatR;
namespace FinanceCore.Application.Features.Accounts.Commands.Update
{
    public class UpdateAccountCommandHandler
        : IRequestHandler<UpdateAccountCommand>
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IMediator _eventBus;

        public UpdateAccountCommandHandler(
            IAccountRepository accountRepository,
            IMediator eventBus)
        {
            _accountRepository = accountRepository;
            _eventBus = eventBus;
        }

        public async Task Handle(
            UpdateAccountCommand command,
            CancellationToken cancellationToken)
        {
            var account = await _accountRepository.GetByIdAndUserIdAsync(
                command.UserId,
                command.AccountId,
                cancellationToken);

            if (account is null)
                throw new AccountNotFoundException(command.AccountId);

            account.UpdateDetails(
                command.Name,
                command.AccountType);

            if (command.AccountType == EnAccountType.Savings)
            {
                account.ChangeInterestRate(
                    command.InterestRate.Value);

                account.ChangeAccrualFrequency(
                    command.AccrualFrequency.Value,
                    DateTime.UtcNow);

                account.ChangeCreditFrequency(
                    command.CreditFrequency.Value,
                    DateTime.UtcNow);
            }

            await DomainEventDispatcher.DispatchAsync(
                _eventBus,
                account,
                cancellationToken);

            await _accountRepository.UpdateAsync(
                account,
                null,
                cancellationToken);
        }
    }
}
