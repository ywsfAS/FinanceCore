using FinanceCore.Application.Abstractions;
using FinanceCore.Application.DTOs;
using FinanceCore.Application.DTOs.Transaction;
using FinanceCore.Application.Models;
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
        public Task<IEnumerable<Transaction>> GetByAccountIdAsync(Guid id, CancellationToken token = default)
        {
            var key = $"Transactions_Account_{id}";
            return _memoryCache.GetOrCreateAsync(key, entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10);
                return _transactionRepository.GetByAccountIdAsync(id, token);
            });
        }
        public Task<CreateTransferDto> TransferAsync(Transaction transaction, CancellationToken token = default)
        {
            var key = $"Transfer_{transaction.Id}";
            return _memoryCache.GetOrCreateAsync(key, entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10);
                return _transactionRepository.TransferAsync(transaction, token);
            });
        }
        public Task<CreateTransactionDto> IncomeAsync(Transaction transaction, CancellationToken token)
        {
            var key = $"Income_{transaction.Id}";
            return _memoryCache.GetOrCreateAsync(key, entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10);
                return _transactionRepository.IncomeAsync(transaction, token);
            });
        }
        public Task<CreateTransactionDto> ExpenseAsync(Transaction transaction, CancellationToken token)
        {
            var key = $"Expense_{transaction.Id}";
            return _memoryCache.GetOrCreateAsync(key, entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10);
                return _transactionRepository.ExpenseAsync(transaction, token);
            });
        }
        public Task<IEnumerable<TransactionDto>?> GetFiltredTransactionsAsync(Guid? categoryId, DateTime? start, DateTime? end, byte? type, int page, int pageSize)
        {
            var key = $"FiltredTransactions_{categoryId}_{start}_{end}_{type}_{page}_{pageSize}";
            return _memoryCache.GetOrCreateAsync(key, entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10);
                return _transactionRepository.GetFiltredTransactionsAsync(categoryId, start, end, type, page, pageSize);
            });
        }
        public Task<decimal> GetTotalSpentAsync(Guid categoryId, DateTime start, DateTime end, byte Type)
        {
            var key = $"TotalSpent_{categoryId}_{start}_{end}_{Type}";
            return _memoryCache.GetOrCreateAsync(key, entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10);
                return _transactionRepository.GetTotalSpentAsync(categoryId, start, end, Type);
            });
        }

        public Task AddAsync(Transaction transaction, CancellationToken token = default)
        {
            return _transactionRepository.AddAsync(transaction, token);
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
        public Task<ReportModel?> GetMonthlySummary(Guid sccountId, DateTime start, DateTime end)
        {
             var key = $"MonthlySummary_{sccountId}_{start}_{end}";
            return _memoryCache.GetOrCreateAsync(key, entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10);
                return _transactionRepository.GetMonthlySummary(sccountId, start, end);
            });
        }
        public Task<IEnumerable<TransactionDto>?> FetchTransactionsByIdPageAsync(Guid accountId, int page, int pageSize)
        {
            var key = $"TransactionsByPage_{accountId}_{page}_{pageSize}";
            return _memoryCache.GetOrCreateAsync(key, entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10);
                return _transactionRepository.FetchTransactionsByIdPageAsync(accountId, page, pageSize);
            });
        }
        public Task<IEnumerable<SpendingByCategoryDto>> GetSpendingByCategory(
            Guid userId, Guid? accountId, DateTime start, DateTime end)
        {
            var key = $"SpendingByCategory_{userId}_{accountId}_{start}_{end}";
            return _memoryCache.GetOrCreateAsync(key, entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10);
                return _transactionRepository.GetSpendingByCategory(userId, accountId, start, end);
            });
        }
        public Task<bool> IsExists(Guid userId, Guid id, CancellationToken token = default)
        {
            var key = $"TransactionExists_{userId}_{id}";
            return _memoryCache.GetOrCreateAsync(key, entry =>
            {
                entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(10);
                return _transactionRepository.IsExists(userId, id, token);
            });
        }
    }
}
