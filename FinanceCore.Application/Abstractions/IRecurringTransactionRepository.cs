using FinanceCore.Application.DTOs.RecurringTransaction;
using FinanceCore.Domain.Enums;
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
        Task<RecurringTransaction?> GetByIdAsync(Guid userId ,Guid id );
        Task<RecurringTransactionDto?> GetDtoByIdAsync(Guid userId , Guid id );
        Task<IEnumerable<RecurringTransaction>> GetActiveAsync();
        Task<IEnumerable<RecurringTransactionDto>> GetRecurringTransactionsAsync(Guid userId , Guid? accountId , Guid? categoryId , bool? isActive, EnPeriod? period, DateTime? start, DateTime? end, int page, int pageSize, CancellationToken token = default);
        Task AddAsync(RecurringTransaction recurringTransaction );
        Task UpdateAsync(RecurringTransaction recurringTransaction);
        Task DeleteAsync(Guid id);
    }
}
