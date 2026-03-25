using FinanceCore.Domain.RecurringTransaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Application.Abstractions
{
    public interface IRecurringTransactionRepository
    {
        Task<RecurringTransaction?> GetByIdAsync(Guid id);
        Task AddAsync(RecurringTransaction recurringTransaction);
        Task UpdateAsync(RecurringTransaction recurringTransaction);
        Task DeleteAsync(Guid id);

        Task<List<RecurringTransaction>> GetActiveAsync();
        Task<List<RecurringTransaction>> GetByAccountAsync(Guid accountId);
        Task<List<RecurringTransaction>> GetByCategoryAsync(Guid categoryId);
        Task<List<RecurringTransaction>> GetDueTransactionsAsync(DateTime currentDate);
    }
}
