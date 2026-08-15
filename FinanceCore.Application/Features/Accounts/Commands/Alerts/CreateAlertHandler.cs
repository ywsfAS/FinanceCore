using FinanceCore.Accounts;
using FinanceCore.Application.Abstractions;
using FinanceCore.Domain.Enums;
using MediatR;

namespace FinanceCore.Application.Features.Accounts.Commands.Alerts
{
    public sealed class CreateLowBalanceAlertHandler
        : IRequestHandler<CreateLowBalanceAlertCommand, Guid>
    {
        private readonly IAccountRepository _accountRepository;
        private readonly IAlertRepository _alertRepository;

        public CreateLowBalanceAlertHandler(
            IAccountRepository accountRepository,
            IAlertRepository alertRepository)
        {
            _accountRepository = accountRepository;
            _alertRepository = alertRepository;
        }

        public async Task<Guid> Handle(
            CreateLowBalanceAlertCommand request,
            CancellationToken cancellationToken)
        {
            var account = await _accountRepository.GetAccountByIdAsync(
                request.AccountId,
                cancellationToken);

            if (account is null)
                throw new KeyNotFoundException("Account was not found.");

            if (account.Type != EnAccountType.Checking)
                throw new InvalidOperationException(
                    "Low balance alerts are only available for checking accounts.");

            var alert = new LowBalanceAlert(
                Guid.NewGuid(),
                account.Id,
                request.ThresholdAmount);

            await _alertRepository.CreateAsync(
                alert,
                cancellationToken);

            return alert.Id;
        }
    }
}
