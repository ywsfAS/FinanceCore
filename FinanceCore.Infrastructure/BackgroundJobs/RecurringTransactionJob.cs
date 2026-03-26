using FinanceCore.Application.Abstractions;
using Quartz;
using FinanceCore.Domain.Transactions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualBasic;
using Microsoft.Extensions.Logging;

namespace FinanceCore.Infrastructure.BackgroundJobs
{
    public class RecurringTransactionJob : IJob
    {
        private readonly IRecurringTransactionRepository _recurringTransactionRepository;
        private readonly ITransactionRepository _transactionRepository;
        private readonly ILogger<RecurringTransactionJob> _logger;
        public RecurringTransactionJob(ILogger<RecurringTransactionJob> logger , IRecurringTransactionRepository recurringTransactionRepository , ITransactionRepository transaction)
        {
            _recurringTransactionRepository = recurringTransactionRepository;
            _transactionRepository = transaction;
            _logger = logger;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var now = DateTime.UtcNow;
            var recurringTransactions = await _recurringTransactionRepository.GetActiveAsync(); //TODO Not Optimized approach!
            _logger.LogInformation("RecurringTransactionJob started at {Time}", now);
            foreach (var recurringTransaction in recurringTransactions)
            {
                try
                {
                    if (!recurringTransaction.CanExecute(now))
                    {
                        continue;
                    }
                    var transaction = Transaction.Create(Guid.NewGuid(), recurringTransaction.accountId, recurringTransaction.amount, recurringTransaction.categoryId, recurringTransaction.type, now, recurringTransaction.description);
                    await _transactionRepository.AddAsync(transaction);
                    recurringTransaction.markAsExecuted(now);
                    await _recurringTransactionRepository.UpdateAsync(recurringTransaction);
                    _logger.LogInformation("Executed recurring transaction {Id}", recurringTransaction.Id);
                }
                catch (Exception ex) {
                    _logger.LogError(ex, "Error executing recurring transaction {Id}", recurringTransaction.Id);
                }
            }
        }
    }
}
