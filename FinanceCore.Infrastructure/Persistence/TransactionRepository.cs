using FinanceCore.Application.Abstractions;
using FinanceCore.Application.DTOs.Transaction;
using Dapper;
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
        public async Task<TransferDto> TransferAsync(Transaction transaction, CancellationToken token)
        {
            var model = TransactionMapper.MapToModel(transaction);

            // Use QuerySingleAsync to get the SP result
            var result = await _connectionFactory.QuerySingleAsync<TransferModel>(
                "sp_Transfer",
                new
                {
                    SourceAccountId = model.AccountId,
                    DestinationAccountId = model.ToAccountId,
                    Amount = model.Amount,
                    Description = model.Description
                }
                );

            return new TransferDto(result.CreditTransactionId,result.DebitTransactionId,result.SourceBalance,result.DestinationBalance,result.TransferDate);
        }
        public async Task<IncomeDto> IncomeAsync(Transaction transaction, CancellationToken token)
        {
            var model = TransactionMapper.MapToModel(transaction);
            var result = await _connectionFactory.QuerySingleAsync<TransactionModel>(
                "sp_CreateIncome",
                new
                {
                    AccountId = model.AccountId,
                    CategoryId = model.CategoryId,
                    Amount = model.Amount,
                    Description = model?.Description,
                }
                );
            return new IncomeDto(result.Id ,result.CategoryId , result.Amount , result.Description );


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