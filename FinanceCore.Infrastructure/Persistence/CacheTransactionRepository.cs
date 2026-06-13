using FinanceCore.Application.Abstractions;
using FinanceCore.Application.DTOs;
using FinanceCore.Application.DTOs.Transaction;
using FinanceCore.Application.Models;
using FinanceCore.Domain.Enums;
using FinanceCore.Domain.Transactions;
using FinanceCore.Infrastructure.Repositories;
using Microsoft.Extensions.Caching.Memory;
namespace FinanceCore.Infrastructure.Persistence
{
    public class CacheTransactionRepository : ITransactionRepository
    {
        private readonly TransactionRepository _transactionRepository;
        private readonly IMemoryCache _memoryCache;
        public CacheTransactionRepository(TransactionRepository transactionRepository, IMemoryCache memoryCache)
        {
            _transactionRepository = transactionRepository;
            _memoryCache = memoryCache;
        }
        public Task<Transaction?> GetByIdAsync(Guid id, CancellationToken token = default)
        {
            var key = $"Transaction_{id}";
            return _memoryCache.GetOrCreateAsync(key, entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10);
                return _transactionRepository.GetByIdAsync(id, token);
            });

        }
        public Task<TransactionDto> TransferAsync(Transaction transaction, CancellationToken token = default)
        {
            var key = $"Transfer_{transaction.Id}";
            return _memoryCache.GetOrCreateAsync(key, entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10);
                return _transactionRepository.TransferAsync(transaction, token);
            });
        }
        public Task<TransactionDto> IncomeTransactionAsync(Transaction transaction, CancellationToken token)
        {
            var key = $"Income_{transaction.Id}";
            return _memoryCache.GetOrCreateAsync(key, entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10);
                return _transactionRepository.IncomeTransactionAsync(transaction, token);
            });
        }
        public Task<TransactionDto> ExpenseTransactionAsync(Transaction transaction, CancellationToken token)
        {
            var key = $"Expense_{transaction.Id}";
            return _memoryCache.GetOrCreateAsync(key, entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10);
                return _transactionRepository.ExpenseTransactionAsync(transaction, token);
            });
        }
        public  Task<decimal> GetTotalSpendingByCategoryAsync(Guid userId,Guid categoryId,DateTime start,DateTime end,CancellationToken token)
        {

            var key = $"TotalSpendingByCategory_{userId}_{categoryId}_{start}_{end}";
            return _memoryCache.GetOrCreateAsync(key, entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10);
                return _transactionRepository.GetTotalSpendingByCategoryAsync(userId, categoryId, start, end, token);
            });

        }
        public Task<IEnumerable<TransactionDto>?> GetFilteredTransactionsAsync(Guid userId ,Guid? accountId,Guid? toAccountId , Guid? categoryId, DateTime? start, DateTime? end, EnTransactionType? type, int page, int pageSize,CancellationToken token = default)
        {
            var key = $"FiltredTransactions_{categoryId}_{start}_{end}_{type}_{page}_{pageSize}";
            return _memoryCache.GetOrCreateAsync(key, entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10);
                return _transactionRepository.GetFilteredTransactionsAsync(userId,accountId,toAccountId,categoryId, start, end, type, page, pageSize,token);
            });
        }
        public Task UpdateAsync(Transaction transaction, CancellationToken token = default)
        {
            return _transactionRepository.UpdateAsync(transaction, token);
        }
        public Task DeleteAsync(Guid id, CancellationToken token = default)
        {
            return _transactionRepository.DeleteAsync(id, token);
        }
        public Task<Transaction?> GetByIdAndUserId(Guid userId, Guid id, CancellationToken token = default)
        {
            var key = $"Transaction_{userId}_{id}";
            return _memoryCache.GetOrCreateAsync(key, entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10);
                return _transactionRepository.GetByIdAndUserId(userId, id, token);
            });
        }
        public Task<TransactionDto?> GetDtoByIdAndUserId(Guid userId, Guid id, CancellationToken token = default)
        {
            var key = $"TransactionDto_{userId}_{id}";
            return _memoryCache.GetOrCreateAsync(key, entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10);
                return _transactionRepository.GetDtoByIdAndUserId(userId, id, token);
            });
        }
        public Task<IEnumerable<MonthlyTrendDto>> GetMonthlyTrend(Guid UserId, int lastNMonth , CancellationToken token)
        {
            var key = $"MonthlyTrend_{UserId}_last_{lastNMonth}";
            return _memoryCache.GetOrCreateAsync(key, entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10);
                return _transactionRepository.GetMonthlyTrend(UserId,lastNMonth,token);
            });

        }
        public Task<IEnumerable<MonthlySummaryDto>> GetMonthlySummaryAsync(Guid userId,Guid? accountId, DateTime start, DateTime end, int page , int pageSize , CancellationToken token = default)
        {
             var key = $"MonthlySummary_{userId}_{accountId}_{start}_{end}_{page}_{pageSize}";
            return _memoryCache.GetOrCreateAsync(key, entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10);
                return _transactionRepository.GetMonthlySummaryAsync(userId,accountId, start, end,page,pageSize,token);
            });
        }

        public Task<IEnumerable<SpendingByCategoryDto>> GetSpendingByCategoryAsync(
            Guid userId, Guid? accountId, DateTime start, DateTime end ,int page , int pageSize,CancellationToken token = default)
        {
            var key = $"SpendingByCategory_{userId}_{accountId}_{start}_{end}_{page}_{pageSize}";
            return _memoryCache.GetOrCreateAsync(key, entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10);
                return _transactionRepository.GetSpendingByCategoryAsync(userId, accountId, start, end,page,pageSize,token);
            });
        }
        public Task<bool> IsExistsAsync(Guid userId, Guid id, CancellationToken token = default)
        {
            var key = $"TransactionExists_{userId}_{id}";
            return _memoryCache.GetOrCreateAsync(key, entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10);
                return _transactionRepository.IsExistsAsync(userId, id, token);
            });
        }
    }
}
