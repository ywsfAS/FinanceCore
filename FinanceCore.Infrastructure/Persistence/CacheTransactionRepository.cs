using FinanceCore.Application.Abstractions;
using FinanceCore.Application.DTOs;
using FinanceCore.Application.DTOs.Transaction;
using FinanceCore.Domain.Enums;
using FinanceCore.Domain.Transactions;
using FinanceCore.Infrastructure.Repositories;

namespace FinanceCore.Infrastructure.Persistence
{
    public class CacheTransactionRepository : ITransactionRepository
    {
        private readonly TransactionRepository _repo;
        private readonly ICacheService _cache;

        private static string Tag(Guid userId) => $"Transactions_{userId}";

        public CacheTransactionRepository(TransactionRepository transactionRepository, ICacheService cache)
        {
            _repo = transactionRepository;
            _cache = cache;
        }

        public Task<IEnumerable<BudgetHealthDataDto>?> GetBudgetHealthAsync(Guid userId, int page, int pageSize, CancellationToken token = default)
        {
            var key = $"BudgetHealth_{userId}_{page}_{pageSize}";
            return _cache.GetOrCreateAsync(Tag(userId), key, () => _repo.GetBudgetHealthAsync(userId, page, pageSize, token));
        }

        public Task<decimal> GetTotalSpendingByCategoryAsync(Guid userId, Guid categoryId, DateTime start, DateTime end, CancellationToken token)
        {
            var key = $"TotalSpendingByCategory_{categoryId}_{start}_{end}";
            return _cache.GetOrCreateAsync(Tag(userId), key, () => _repo.GetTotalSpendingByCategoryAsync(userId, categoryId, start, end, token));
        }

        public Task<IEnumerable<TransactionDto>> GetFilteredTransactionsAsync(Guid userId, Guid? accountId, Guid? toAccountId, Guid? categoryId, DateTime? start, DateTime? end, EnTransactionType? type, int page, int pageSize, CancellationToken token = default)
        {
            var key = $"FilteredTransactions_{accountId}_{toAccountId}_{categoryId}_{start}_{end}_{type}_{page}_{pageSize}";
            return _cache.GetOrCreateAsync(Tag(userId), key, () => _repo.GetFilteredTransactionsAsync(userId, accountId, toAccountId, categoryId, start, end, type, page, pageSize, token));
        }

        public Task<Transaction?> GetByIdAndUserId(Guid userId, Guid id, CancellationToken token = default)
        {
            var key = $"Transaction_{id}";
            return _cache.GetOrCreateAsync(Tag(userId), key, () => _repo.GetByIdAndUserId(userId, id, token));
        }

        public Task<TransactionDto?> GetDtoByIdAndUserId(Guid userId, Guid id, CancellationToken token = default)
        {
            var key = $"TransactionDto_{id}";
            return _cache.GetOrCreateAsync(Tag(userId), key, () => _repo.GetDtoByIdAndUserId(userId, id, token));
        }

        public Task<IEnumerable<MonthlyTrendDto>> GetMonthlyTrend(Guid userId, int lastNMonth, CancellationToken token)
        {
            var key = $"MonthlyTrend_last_{lastNMonth}";
            return _cache.GetOrCreateAsync(Tag(userId), key, () => _repo.GetMonthlyTrend(userId, lastNMonth, token));
        }

        public Task<IEnumerable<MonthlySummaryDto>> GetMonthlySummaryAsync(Guid userId, Guid? accountId, DateTime start, DateTime end, int page, int pageSize, CancellationToken token = default)
        {
            var key = $"MonthlySummary_{accountId}_{start}_{end}_{page}_{pageSize}";
            return _cache.GetOrCreateAsync(Tag(userId), key, () => _repo.GetMonthlySummaryAsync(userId, accountId, start, end, page, pageSize, token));
        }

        public Task<IEnumerable<SpendingByCategoryDto>> GetSpendingByCategoryAsync(Guid userId, Guid? accountId, DateTime start, DateTime end, int page, int pageSize, CancellationToken token = default)
        {
            var key = $"SpendingByCategory_{accountId}_{start}_{end}_{page}_{pageSize}";
            return _cache.GetOrCreateAsync(Tag(userId), key, () => _repo.GetSpendingByCategoryAsync(userId, accountId, start, end, page, pageSize, token));
        }

        public Task<bool> IsExistsAsync(Guid userId, Guid id, CancellationToken token = default)
        {
            var key = $"TransactionExists_{id}";
            return _cache.GetOrCreateAsync(Tag(userId), key, () => _repo.IsExistsAsync(userId, id, token));
        }

        public async Task AddAsync(Transaction transaction, IUnitOfWork? unitOfWork = null, CancellationToken cancellationToken = default)
        {
            await _repo.AddAsync(transaction, unitOfWork, cancellationToken);
            await _cache.InvalidateTagAsync(Tag(transaction.Id));
        }

        public async Task DeleteAsync(Guid userId, Guid id, CancellationToken token = default)
        {
            await _repo.DeleteAsync(userId,id, token);
            await _cache.InvalidateTagAsync(Tag(userId));
        }
    }
}