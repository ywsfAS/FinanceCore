using FinanceCore.Application.Abstractions;
using FinanceCore.Domain.Transactions;
using FinanceCore.Infrastructure.context;
using FinanceCore.Infrastructure.Mappers;
using FinanceCore.Infrastructure.Models;

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
            var model =  await _connectionFactory.ReadSingleAsync<TransactionModel, Guid>(
                "sp_GetTransactionById",
                id);
            if (model == null) { 
                return null;
            }
            return TransactionMapper.MapToDomain(model);
        }

        public async Task<IEnumerable<Transaction>> GetByAccountIdAsync(Guid accountId, CancellationToken token = default)
        {
            var models =  await _connectionFactory.ReadListAsync<TransactionModel>(
                "sp_GetTransactionsByAccountId",
                new {AccountId = accountId});
            return models.Select(model => TransactionMapper.MapToDomain(model));
        }

        public async Task AddAsync(Transaction transaction, CancellationToken token = default)
        {
            var model = TransactionMapper.MapToModel(transaction);
             await _connectionFactory.ExecuteNonQueryAsync(
                "sp_CreateTransaction",
                model
                );
        }

        public async Task UpdateAsync(Transaction transaction, CancellationToken token = default)
        {
           var model = TransactionMapper.MapToModel(transaction);
            await _connectionFactory.ExecuteNonQueryAsync(
                "sp_UpdateTransaction",
                model
               );
        }

        public async Task DeleteAsync(Transaction transaction, CancellationToken token = default)
        {
            await _connectionFactory.ExecuteNonQueryAsync(
                "sp_DeleteTransaction",
                new { transaction.Id });
        }
    }
}