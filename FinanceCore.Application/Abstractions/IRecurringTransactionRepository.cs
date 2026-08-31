using FinanceCore.Application.DTOs;
using FinanceCore.Application.DTOs.RecurringTransaction;
using FinanceCore.Domain.Enums;
using FinanceCore.Domain.RecurringTransaction;

namespace FinanceCore.Application.Abstractions
{
    public interface IRecurringTransactionRepository
    {
        Task<RecurringTransaction?> GetByIdAsync(Guid userId ,Guid id );
        Task<RecurringTransactionDto?> GetDtoByIdAsync(Guid userId , Guid id );
        Task<IEnumerable<RecurringTransaction>> GetActiveAsync();
        Task<IEnumerable<RecurringTransactionDto>> GetRecurringTransactionsAsync(Guid userId , Guid? accountId , Guid? categoryId , EnRecurringTransactionStatus? isActive, EnPeriod? period, DateTime? start, DateTime? end, int page, int pageSize, CancellationToken token = default);
        Task AddAsync(RecurringTransaction recurringTransaction );
        Task UpdateAsync(RecurringTransaction recurringTransaction , IUnitOfWork? unitOfWork = null, CancellationToken token = default);
        Task<IEnumerable<SubsriptionDataDto>> GetSubscriptionsAsync(Guid userId, Guid? accountId, Guid? categoryId, string? name, EnPeriod? period, EnTransactionType? type, int page, int pageSize, CancellationToken token);
        Task DeleteAsync(Guid id);
        Task<SubscriptionGrowthDto?> GetSubscriptionsGrowthAsync(Guid userId, Guid? accountId, EnTransactionType type, DateTime currentStartDate, DateTime currentEndDate, DateTime previousStartDate, DateTime previousEndDate, CancellationToken token);
       
    }
}
