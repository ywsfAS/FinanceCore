using FinanceCore.Application.Abstractions;
using Quartz;
using FinanceCore.Domain.Transactions;
using Microsoft.Extensions.Logging;

namespace FinanceCore.Infrastructure.BackgroundJobs
{
    public class RecurringTransactionJob : IJob
    {
        private readonly IRecurringTransactionRepository _recurringTransactionRepository;
        private readonly IAccountRepository _accountRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITransactionRepository _transactionRepository;
        private readonly ILogger<RecurringTransactionJob> _logger;
        public RecurringTransactionJob(ILogger<RecurringTransactionJob> logger ,
            IRecurringTransactionRepository recurringTransactionRepository , ITransactionRepository transaction
            ,IAccountRepository accountRepository , IUnitOfWork unitOfWork
            )
        {
            _recurringTransactionRepository = recurringTransactionRepository;
            _transactionRepository = transaction;
            _accountRepository = accountRepository;
            _unitOfWork = unitOfWork;
            _logger = logger;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var now = DateTime.UtcNow;
            var recurringTransactions = await _recurringTransactionRepository.GetActiveAsync(); 
            _logger.LogInformation("RecurringTransactionJob started at {Time}", now);
            foreach (var recurringTransaction in recurringTransactions)
            {
                try
                {
                    if (!recurringTransaction.CanExecute(now))
                    {
                        continue;
                    }
                    var transaction = Transaction.Create(recurringTransaction.AccountId,null, recurringTransaction.Amount, recurringTransaction.CategoryId, recurringTransaction.Type, now, recurringTransaction.Description);
                    var account = await _accountRepository.GetAccountByIdAsync(recurringTransaction.AccountId);
                    if (account is null) continue;
                    recurringTransaction.MarkAsExecuted(now);
                    account.ApplyTransaction(transaction.Amount, transaction.Type);

                    await _unitOfWork.BeginAsync();

                    await _transactionRepository.AddAsync(transaction, _unitOfWork);
                    await _accountRepository.UpdateAsync(account , _unitOfWork);
                    await _recurringTransactionRepository.UpdateAsync(recurringTransaction , _unitOfWork);

                    await _unitOfWork.CommitAsync();

                    _logger.LogInformation("Executed recurring transaction {Id}", recurringTransaction.Id);
                }
                catch (Exception ex) {
                    await _unitOfWork.RollBackAsync();
                    _logger.LogError(ex, "Error executing recurring transaction {Id}", recurringTransaction.Id);
                }
            }
        }
    }
}
