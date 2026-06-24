using FinanceCore.Application.DTOs;
using FinanceCore.Application.DTOs.RecurringTransaction;
using FinanceCore.Domain.Enums;
using FinanceCore.Domain.RecurringTransaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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
        Task<IEnumerable<SubsriptionDataDto>> GetSubscriptions(Guid userId, Guid? accountId, Guid? categoryId, string? name, EnPeriod? period, EnTransactionType? type, int page, int pageSize, CancellationToken token);
        Task DeleteAsync(Guid id);
    }
}
