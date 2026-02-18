using FinanceCore.Application.Abstractions;
using FinanceCore.Domain.Transactions;
using FinanceCore.Infrastructure.context;

namespace FinanceCore.Infrastructure.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {
        private readonly IConnectionFactory _connectionFactory;

        public TransactionRepository(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<Transaction?> GetByIdAsync(Guid id, CancellationToken token = default)
        {
            return await _connectionFactory.ReadSingleAsync<Transaction, Guid>(
                "sp_GetTransactionById",
                id);
        }

        public async Task<IEnumerable<Transaction>> GetByAccountIdAsync(Guid accountId, CancellationToken token = default)
        {
            return await _connectionFactory.ReadListAsync<Transaction>(
                "sp_GetTransactionsByAccountId",
                new { AccountId = accountId });
        }

        public async Task AddAsync(Transaction transaction, CancellationToken token = default)
        {
            await _connectionFactory.ExecuteNonQueryAsync(
                "sp_CreateTransaction",
                new
                {
                    transaction.Id,
                    transaction.AccountId,
                    transaction.CategoryId,
                    Amount = transaction.Amount.Amount,
                    Currency = transaction.Amount.Currency.ToString(),
                    Type = transaction.Type.ToString(),
                    transaction.Date,
                    transaction.Description,
                    transaction.CreatedAt
                });
        }

        public async Task UpdateAsync(Transaction transaction, CancellationToken token = default)
        {
            await _connectionFactory.ExecuteNonQueryAsync(
                "sp_UpdateTransaction",
                new
                {
                    transaction.Id,
                    transaction.CategoryId,
                    Amount = transaction.Amount.Amount,
                    Currency = transaction.Amount.Currency.ToString(),
                    transaction.Date,
                    transaction.Description,
                    transaction.UpdatedAt
                });
        }

        public async Task DeleteAsync(Transaction transaction, CancellationToken token = default)
        {
            await _connectionFactory.ExecuteNonQueryAsync(
                "sp_DeleteTransaction",
                new { transaction.Id });
        }
    }
}