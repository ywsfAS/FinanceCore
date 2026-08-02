using FinanceCore.Application.DTOs;
using FinanceCore.Application.DTOs.Transaction;
using FinanceCore.Domain.Enums;
using FinanceCore.Domain.Transactions;


namespace FinanceCore.Application.Abstractions
{
    public interface ITransactionRepository
    {

        Task AddAsync(Transaction transaction, IUnitOfWork? unitOfWork = null, CancellationToken token = default);
        Task<IEnumerable<TransactionDto>> GetFilteredTransactionsAsync(Guid userId , Guid? accountId,Guid? toAccountId,Guid? categoryId, DateTime? start, DateTime? end, EnTransactionType? type, int page, int pageSize,CancellationToken token);
        Task DeleteAsync(Guid userId ,Guid id, CancellationToken token = default);
        // Get a single transaction read/write
        Task<Transaction?> GetByIdAndUserId(Guid userId , Guid id , CancellationToken token = default);
        Task<TransactionDto?> GetDtoByIdAndUserId(Guid userId , Guid id , CancellationToken token = default);
        // reports
        Task<IEnumerable<MonthlySummaryDto>> GetMonthlySummaryAsync(Guid userId , Guid? accountId, DateTime start, DateTime end, int page , int pageSize, CancellationToken token);
        Task<IEnumerable<SpendingByCategoryDto>> GetSpendingByCategoryAsync(
            Guid userId, Guid? accountId, DateTime start, DateTime end , int page , int pageSize , CancellationToken token = default);
        Task<bool> IsExistsAsync(Guid userId, Guid id, CancellationToken token = default);

        Task<IEnumerable<MonthlyTrendDto>> GetMonthlyTrend(Guid accountId, int lastNMonth, CancellationToken token = default);
        Task<IEnumerable<BudgetHealthDataDto>?> GetBudgetHealthAsync(Guid userId , int page ,int pageSize , CancellationToken token = default);
        Task<decimal> GetTotalSpendingByCategoryAsync(Guid userId, Guid categoryId, DateTime start, DateTime end, CancellationToken token);

        Task InsertTransactions(IEnumerable<Transaction> transactions , CancellationToken token);
    }
}
