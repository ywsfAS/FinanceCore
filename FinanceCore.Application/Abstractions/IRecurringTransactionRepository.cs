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

        Task<IEnumerable<RecurringTransaction>> GetActiveAsync();
        Task<IEnumerable<RecurringTransaction>> GetByAccountAsync(Guid accountId);
        Task<IEnumerable<RecurringTransaction>> GetByCategoryAsync(Guid categoryId);
        Task<IEnumerable<RecurringTransaction>> GetDueTransactionsAsync(DateTime currentDate);
    }
}
