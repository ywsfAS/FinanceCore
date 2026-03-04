using Dapper;
using FinanceCore.Application.Abstractions;
using FinanceCore.Application.DTOs;
using FinanceCore.Application.Models;
using FinanceCore.Domain.Accounts;
using FinanceCore.Domain.Enums;
using FinanceCore.Infrastructure.context;
using FinanceCore.Infrastructure.Mappers;
using System.Data;
namespace FinanceCore.Infrastructure.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly IConnectionFactory _connectionFactory;

        public AccountRepository(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }
        public async Task<bool> IsExists(Guid userId,Guid id,CancellationToken token)
        {
            using var connection = _connectionFactory.GetConnection();
            var sql = @"SELECT 1 FROM Accounts WHERE UserId = @Id AND Id = @Id";
            var parameters = new DynamicParameters();
            parameters.Add("Id", id);
            parameters.Add("UserId", userId);
            var result = await connection.ExecuteScalarAsync<int?>(sql, parameters);
            return result.HasValue;
        }
        public async Task<Account?> GetByIdAsync(Guid id, CancellationToken token = default)
        {
            var model = await _connectionFactory.ReadSingleAsync<AccountModel, Guid>(
                "sp_GetAccountById",
                id);

            if (model is null)
                return null ;

            return AccountMapper.MapToDomain(model);
        }
        private async Task<IEnumerable<AccountModel>?> GetModelByUserIdAsync(Guid id, CancellationToken token = default)
        {
            var models = await _connectionFactory.ReadListAsync<AccountModel>(
            "sp_GetAccountsByUserId",
            new { UserId = id });
            return models;

        }

        public async Task<IEnumerable<Account>?> GetByUserIdAsync(Guid userId, CancellationToken token = default)
        {
            var models = await GetModelByUserIdAsync(userId, token);
            if (models is null)
                    return null;
            return models.Select(model => AccountMapper.MapToDomain(model));
        }
        public async Task<IEnumerable<AccountDto>?> GetDtoByUserIdAsync(Guid userId, CancellationToken token = default)
        {
            var models = await GetModelByUserIdAsync(userId, token);
            if (models is null)
                return null;
            return models.Select(model => new AccountDto(model.Id,model.UserId,model.Name,(EnAccountType)model.AccountTypeId,model.Balance,(EnCurrency)model.CurrencyId,model.CreatedAt));
        }

        public async Task AddAsync(Account account, CancellationToken token = default)
        {
            var model = AccountMapper.MapToModel(account);
            await _connectionFactory.ExecuteNonQueryAsync(
                "sp_CreateAccount",
                model
               );
        }
        public async Task<decimal> GetTotalBalanceAsync(Guid userId, CancellationToken token)
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
            const string sql = @"
            UPDATE Accounts
            SET Name = @Name,
            AccountTypeId = @AccountTypeId,
            Balance = @Balance,
            CurrencyId = @CurrencyId,
            InitialBalance = @InitialBalance,
            IsActive = @IsActive,
            UpdatedAt = @UpdatedAt
            WHERE Id = @Id
          AND UserId = @UserId";

            var model = AccountMapper.MapToModel(account);

            using var connection = _connectionFactory.GetConnection();

            var affectedRows = await connection.ExecuteAsync(
                new CommandDefinition(
                    sql,
                    model,
                    cancellationToken: token,
                    commandType: CommandType.Text));

            if (affectedRows == 0)
                throw new KeyNotFoundException("Account not found or does not belong to the user.");
        }

        // Updated: accept only the Id and use inline SQL (not stored procedure).
        public async Task DeleteAsync(Guid id, CancellationToken token = default)
        {
            const string sql = @"
                DELETE FROM Accounts
                WHERE Id = @Id";

            using var connection = _connectionFactory.GetConnection();
            await connection.ExecuteAsync(
                new CommandDefinition(sql, new { Id = id }, cancellationToken: token, commandType: CommandType.Text));
        }

        public async Task<Account?> GetByIdAndUserIdAsync(Guid userId, Guid id, CancellationToken token = default)
        {
            var model = await GetModelByIdAndUserIdAsync(userId, id, token);
            if (model is null) return null;

            return AccountMapper.MapToDomain(model);
        }

        public async Task<AccountDto?> GetDtoByIdAndUserIdAsync(Guid userId, Guid id, CancellationToken token = default)
        {
            var model = await GetModelByIdAndUserIdAsync(userId, id, token);
            if (model is null) return null;

            return new AccountDto(
                model.Id,
                model.UserId,
                model.Name,
                (EnAccountType)model.AccountTypeId,
                model.Balance,
                (EnCurrency)model.CurrencyId,
                model.CreatedAt);
        }

        private async Task<AccountModel?> GetModelByIdAndUserIdAsync(Guid userId, Guid id, CancellationToken token = default)
        {
            const string sql = @"
                SELECT Id,
                       UserId,
                       Name,
                       AccountTypeId,
                       Balance,
                       CurrencyId,
                       InitialBalance,
                       IsActive,
                       CreatedAt,
                       UpdatedAt
                FROM Accounts
                WHERE Id = @Id AND UserId = @UserId";

            using var connection = _connectionFactory.GetConnection();

            var model = await connection.QuerySingleOrDefaultAsync<AccountModel>(
                new CommandDefinition(sql, new { Id = id, UserId = userId }, cancellationToken: token, commandType: CommandType.Text));

            return model;
        }
    }
}