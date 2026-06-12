using Dapper;
using FinanceCore.Application.Abstractions;
using FinanceCore.Application.DTOs;
using FinanceCore.Application.Models;
using FinanceCore.Domain.Accounts;
using FinanceCore.Domain.Enums;
using FinanceCore.Infrastructure.context;
using FinanceCore.Infrastructure.Mappers;
using Microsoft.AspNetCore.Routing;
using System.Data;
using System.Linq;
using System.Text;
namespace FinanceCore.Infrastructure.Repositories
{
    public class AccountRepository : IAccountRepository
    {
        private readonly IConnectionFactory _connectionFactory;

        public AccountRepository(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }
        public async Task<bool> IsExistsAsync(Guid userId,Guid id,CancellationToken token)
        {
            using var connection = _connectionFactory.GetConnection();
            var sql = @"SELECT 1 FROM Accounts WHERE UserId = @UserId AND Id = @Id";
            var parameters = new DynamicParameters();
            parameters.Add("Id", id);
            parameters.Add("UserId", userId);
            var result = await connection.ExecuteScalarAsync<int?>(sql, parameters);
            return result.HasValue;
        }
        public async Task AddAsync(Account account, CancellationToken token = default)
        {
            const string sql = @"
        INSERT INTO Accounts (
            Id,
            UserId,
            Name,
            AccountTypeId,
            Balance,
            CurrencyId,
            InitialBalance,
            IsActive,
            CreatedAt,
            UpdatedAt
        )
        VALUES (
            @Id,
            @UserId,
            @Name,
            @AccountTypeId,
            @Balance,
            @CurrencyId,
            @InitialBalance,
            @IsActive,
            @CreatedAt,
            @UpdatedAt
        )";

            // Map domain entity to DB model
            var model = AccountMapper.MapToModel(account);

            using var connection = _connectionFactory.GetConnection();

            var affectedRows = await connection.ExecuteAsync(
                new CommandDefinition(
                    sql,
                    model,
                    cancellationToken: token,
                    commandType: CommandType.Text
                )
            );

            if (affectedRows == 0)
                throw new InvalidOperationException("Failed to insert account into the database.");
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
        public async Task<IEnumerable<AccountOptionsDto>> GetByUserAccountsOptionsAsync(Guid id,int page , int pageSize, CancellationToken token)
        {
            using var connection = _connectionFactory.GetConnection();
            const string sql = @"SELECT Id , Name FROM Accounts WHERE UserId = @Id ORDER BY t.CreatedAt OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY";
            var command = new CommandDefinition(sql, new { Id = id , Offset = (page - 1) * pageSize , PageSize = pageSize }, cancellationToken: token);
            return await connection.QueryAsync<AccountOptionsDto>(command);
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
        public async Task<IEnumerable<AccountInfoDto>> GetAccountsAsync(Guid userId , EnAccountType? type , EnCurrency? currency , string? name ,int page , int pageSize , CancellationToken token)
        {
            using var connection = _connectionFactory.GetConnection();
            var sql = new StringBuilder(@"
                SELECT Id ,
                       Name ,
                       AccountTypeId AS Type,
                       Balance,
                       CurrencyId AS Currency
                FROM Accounts
                WHERE UserId = @Id
            ");
            if (name is not null) sql.Append(" AND Name LIKE @Name");
            if (type.HasValue) sql.Append(" AND AccountTypeId = @Type");
            if (currency.HasValue) sql.Append(" AND CurrencyId = @Currency");

            sql.Append(" ORDER BY t.CreatedAt OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY");

            var command = new CommandDefinition(sql.ToString(), new {Id = userId, Name = $"%{name}%", Type = type, Currency = currency ,Offset = pageSize * (page - 1) , PageSize = pageSize });


            return await connection.QueryAsync<AccountInfoDto>(command);


        }
    }
}