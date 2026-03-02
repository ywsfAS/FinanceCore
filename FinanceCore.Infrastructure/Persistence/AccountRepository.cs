using Dapper;
using FinanceCore.Application.Abstractions;
using FinanceCore.Application.DTOs;
using FinanceCore.Application.DTOs.Transaction;
using FinanceCore.Application.Models;
using FinanceCore.Domain.Accounts;
using FinanceCore.Domain.Enums;
using FinanceCore.Infrastructure.context;
using FinanceCore.Infrastructure.Mappers;


namespace FinanceCore.Infrastructure.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly IConnectionFactory _connectionFactory;

        public AccountRepository(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<Account?> GetByIdAsync(Guid id, CancellationToken token = default)
        {
            var model = await _connectionFactory.ReadSingleAsync<AccountModel, Guid>(
                "sp_GetAccountById",
                id);
            if (model == null) {
                return null;
            }
            return AccountMapper.MapToDomain(model);
        }

        public async Task<IEnumerable<Account>> GetByUserIdAsync(Guid userId, CancellationToken token = default)
        {
            var models =  await _connectionFactory.ReadListAsync<AccountModel>(
                "sp_GetAccountsByUserId",
                new { UserId = userId });
            return models.Select(model => AccountMapper.MapToDomain(model));
        }

        public async Task AddAsync(Account account, CancellationToken token = default)
        {
            var model = AccountMapper.MapToModel(account);
            await _connectionFactory.ExecuteNonQueryAsync(
                "sp_CreateAccount",
                model
               );
        }
        public async Task<decimal> GetTotalBalanceAsync(Guid userId)
        {
            using var connection = _connectionFactory.GetConnection();
            var sql = @"
            SELECT ISNULL(SUM(Balance), 0) 
            FROM Accounts
            WHERE UserId = @UserId";

            var total = await connection.ExecuteScalarAsync<decimal>(
                sql,
                new { UserId = userId }
            );

            return total;
        }

        public async Task UpdateAsync(Account account, CancellationToken token = default)
        {
            var model = AccountMapper.MapToModel(account);
            await _connectionFactory.ExecuteNonQueryAsync(
                "sp_UpdateAccount",
                model
             
                );
        }

        public async Task DeleteAsync(Account account, CancellationToken token = default)
        {
            await _connectionFactory.ExecuteNonQueryAsync(
                "sp_DeleteAccount",
                new { account.Id });
        }
        public async Task<Account?> GetByIdAndUserIdAsync(Guid UserId, Guid id, CancellationToken token = default)
        {
            var AccountDto = await GetDtoByIdAndUserIdAsync(UserId,id,token);
            if(AccountDto == null) return null;
            // Souldnt Take InitBalance ? 
            return Account.Create(AccountDto.Id , AccountDto.Name , AccountDto.Type,AccountDto.Currency,AccountDto.Balance);

        }
        public async Task<AccountDto?> GetDtoByIdAndUserIdAsync(Guid UserId, Guid id, CancellationToken token = default)
        {
            using var connection = _connectionFactory.GetConnection();
            var sql = @"SELECT * FROM Accounts WHERE";
            var parameters = new DynamicParameters();
            sql += " Id = @Id";
            parameters.Add("Id", id);
            sql += " AND UserId = @UserId";
            parameters.Add("UserId", UserId);

            var model = await connection.QuerySingleOrDefaultAsync<AccountModel>(sql, parameters);
            if(model == null)
            {
                return null;
            }
            return new AccountDto(model.Id,model.UserId,model.Name,(EnAccountType)model.Type,model.Balance,(EnCurrency)model.CurrencyId,model.CreatedAt);

        }
    }
}