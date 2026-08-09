using FinanceCore.Application.Abstractions;
using Microsoft.Extensions.Logging;
using Quartz;

namespace FinanceCore.Infrastructure.BackgroundJobs
{
    public sealed class SavingsInterestJob : IJob
    {
        private readonly IAccountRepository _accountRepository;
        private readonly ILogger<SavingsInterestJob> _logger;

        public SavingsInterestJob(IAccountRepository accountRepository, ILogger<SavingsInterestJob> logger)
        {
            _accountRepository = accountRepository;
            _logger = logger;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var now = DateTime.UtcNow;

            _logger.LogInformation(
                "Savings interest job started at {Now}",
                now);

            var accounts =
                await _accountRepository
                    .GetSavingsAccountsForInterestProcessingAsync(
                        context.CancellationToken);

            foreach (var account in accounts)
            {
                account.AccrueInterest(now);

                if (account.SavingsDetails!.NextInterestCreditAt <= now)
                {
                    account.CreditAccruedInterest(now);
                }

                await _accountRepository.UpdateAsync(
                    account,
                    null,context.CancellationToken);
            }

            _logger.LogInformation(
                "Savings interest job completed at {Now}",
                DateTime.UtcNow);
        }
    }
}
