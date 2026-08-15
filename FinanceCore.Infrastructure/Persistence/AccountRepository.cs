using Dapper;
using FinanceCore.Application.Abstractions;
using FinanceCore.Application.DTOs;
using FinanceCore.Application.Exceptions;
using FinanceCore.Application.Models;
using FinanceCore.Domain.Accounts;
using FinanceCore.Domain.Enums;
using FinanceCore.Infrastructure.Context;
using FinanceCore.Infrastructure.Mappers;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Data.Common;
using System.Text;
using Z.Dapper.Plus;

namespace FinanceCore.Infrastructure.Repositories;

public class AccountRepository : IAccountRepository
{
    private readonly IConnectionFactory _connectionFactory;

    public AccountRepository(IConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public async Task<Account?> GetAccountByIdAsync(
        Guid accountId,
        CancellationToken token = default)
    {
        const string sql = """
            SELECT
                a.Id,
                a.UserId,
                a.Name,
                a.AccountTypeId,
                a.Balance,
                a.CurrencyId,
                a.InitialBalance,
                a.IsActive,
                a.CreatedAt,
                a.UpdatedAt,
                a.RowVersion,

                s.InterestRate,
                s.InterestAccruedToDate,
                s.AccrualFrequency,
                s.CreditFrequency,
                s.LastInterestAccrualAt,
                s.NextInterestAccrualAt,
                s.LastInterestCreditAt,
                s.NextInterestCreditAt

            FROM Accounts a
            LEFT JOIN SavingsDetails s
                ON a.Id = s.AccountId

            WHERE a.Id = @Id
              AND a.IsActive = 1;
            """;

        using var connection = _connectionFactory.GetConnection();

        var command = new CommandDefinition(
            sql,
            new { Id = accountId },
            cancellationToken: token);

        var result =
            await connection.QuerySingleOrDefaultAsync<AccountModel>(command);

        if (result is null)
            return null;

        return AccountMapper.MapToDomain(result);
    }

    public async Task<bool> IsExistsAsync(
        Guid userId,
        Guid id,
        CancellationToken token)
    {
        using var connection = _connectionFactory.GetConnection();

        const string sql = """
            SELECT 1
            FROM Accounts
            WHERE UserId = @UserId
              AND Id = @Id
              AND IsActive = 1
            """;

        var parameters = new DynamicParameters();
        parameters.Add("Id", id);
        parameters.Add("UserId", userId);

        var result =
            await connection.ExecuteScalarAsync<int?>(
                new CommandDefinition(
                    sql,
                    parameters,
                    cancellationToken: token));

        return result.HasValue;
    }

    public async Task AddAsync(
        Account account,
        CancellationToken token = default)
    {
        const string accountSql = """
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
            );
            """;

        const string savingsSql = """
            INSERT INTO SavingsDetails (
                AccountId,
                InterestRate,
                LastInterestAccrualAt,
                NextInterestAccrualAt,
                AccrualFrequency,
                LastInterestCreditAt,
                NextInterestCreditAt,
                CreditFrequency,
                InterestAccruedToDate
            )
            VALUES (
                @Id,
                @InterestRate,
                @LastInterestAccrualAt,
                @NextInterestAccrualAt,
                @AccrualFrequency,
                @LastInterestCreditAt,
                @NextInterestCreditAt,
                @CreditFrequency,
                @InterestAccruedToDate
            );
            """;

        var model = AccountMapper.MapToModel(account);

        using var connection = (SqlConnection)_connectionFactory.GetConnection();
        await connection.OpenAsync(token);
        using var transaction = connection.BeginTransaction();

        try
        {
            var affectedRows = await connection.ExecuteAsync(
                new CommandDefinition(
                    accountSql,
                    model,
                    transaction: transaction,
                    cancellationToken: token));

            if (affectedRows == 0)
                throw new InvalidOperationException(
                    "Failed to insert account into the database.");

            if (account.Type == EnAccountType.Savings)
            {
                await connection.ExecuteAsync(
                    new CommandDefinition(
                        savingsSql,
                        model,
                        transaction: transaction,
                        cancellationToken: token));
            }

            transaction.Commit();
        }
        catch
        {
            transaction.Rollback();
            throw;
        }
    }

    public async Task UpdateAsync(
        Account account,
        IUnitOfWork? unitOfWork = null,
        CancellationToken token = default)
    {
        const string accountSql = """
            UPDATE Accounts
            SET Name = @Name,
                AccountTypeId = @AccountTypeId,
                Balance = @Balance,
                CurrencyId = @CurrencyId,
                InitialBalance = @InitialBalance,
                IsActive = @IsActive,
                UpdatedAt = @UpdatedAt
            WHERE Id = @Id
              AND UserId = @UserId
              AND RowVersion = @RowVersion;
            """;

        const string savingsSql = """
            UPDATE SavingsDetails
            SET InterestRate = @InterestRate,
                InterestAccruedToDate = @InterestAccruedToDate,
                AccrualFrequency = @AccrualFrequency,
                CreditFrequency = @CreditFrequency,
                LastInterestAccrualAt = @LastInterestAccrualAt,
                NextInterestAccrualAt = @NextInterestAccrualAt,
                LastInterestCreditAt = @LastInterestCreditAt,
                NextInterestCreditAt = @NextInterestCreditAt
            WHERE AccountId = @Id;
            """;

        var model = AccountMapper.MapToModel(account);

        if (unitOfWork is not null)
        {
            var affectedRows = await unitOfWork.Connection.ExecuteAsync(
                new CommandDefinition(
                    accountSql,
                    model,
                    transaction: unitOfWork.Transaction,
                    cancellationToken: token));

            if (affectedRows == 0)
                throw new ConcurrencyException(
                    "The account was modified by another request.");

            if (account.Type == EnAccountType.Savings)
            {
                await unitOfWork.Connection.ExecuteAsync(
                    new CommandDefinition(
                        savingsSql,
                        model,
                        transaction: unitOfWork.Transaction,
                        cancellationToken: token));
            }

            return;
        }

        using var connection = _connectionFactory.GetConnection();
        using var transaction = connection.BeginTransaction();

        try
        {
            var affectedRows = await connection.ExecuteAsync(
                new CommandDefinition(
                    accountSql,
                    model,
                    transaction: transaction,
                    cancellationToken: token));

            if (affectedRows == 0)
                throw new ConcurrencyException(
                    "The account was modified by another request.");

            if (account.Type == EnAccountType.Savings)
            {
                await connection.ExecuteAsync(
                    new CommandDefinition(
                        savingsSql,
                        model,
                        transaction: transaction,
                        cancellationToken: token));
            }

            transaction.Commit();
        }
        catch
        {
            transaction.Rollback();
            throw;
        }
    }

    public async Task DeleteAsync(
        Guid userId,
        Guid id,
        CancellationToken token = default)
    {
        const string sql = """
            DELETE FROM Accounts
            WHERE UserId = @UserId
              AND Id = @Id;
            """;

        using var connection = _connectionFactory.GetConnection();

        await connection.ExecuteAsync(
            new CommandDefinition(
                sql,
                new
                {
                    Id = id,
                    UserId = userId
                },
                cancellationToken: token));
    }

    public async Task<Account?> GetByIdAndUserIdAsync(
        Guid userId,
        Guid id,
        CancellationToken token = default)
    {
        var model =
            await GetModelByIdAndUserIdAsync(
                userId,
                id,
                token);

        if (model is null)
            return null;

        return AccountMapper.MapToDomain(model);
    }

    public async Task<AccountDto?> GetDtoByIdAndUserIdAsync(
        Guid userId,
        Guid id,
        CancellationToken token = default)
    {
        var model =
            await GetModelByIdAndUserIdAsync(
                userId,
                id,
                token);

        if (model is null)
            return null;

        return new AccountDto(
            model.Id,
            model.UserId,
            model.Name,
            model.AccountTypeId,
            model.Balance,
            (EnCurrency)model.CurrencyId,
            model.CreatedAt);
    }

    public async Task<IEnumerable<AccountOptionsDto>>
        GetByUserAccountsOptionsAsync(
            Guid id,
            int page,
            int pageSize,
            CancellationToken token)
    {
        using var connection = _connectionFactory.GetConnection();

        const string sql = """
            SELECT Id, Name
            FROM Accounts
            WHERE UserId = @Id
            ORDER BY CreatedAt
            OFFSET @Offset ROWS
            FETCH NEXT @PageSize ROWS ONLY;
            """;

        var command = new CommandDefinition(
            sql,
            new
            {
                Id = id,
                Offset = (page - 1) * pageSize,
                PageSize = pageSize
            },
            cancellationToken: token);

        return await connection.QueryAsync<AccountOptionsDto>(command);
    }

    private async Task<AccountModel?>
        GetModelByIdAndUserIdAsync(
            Guid userId,
            Guid id,
            CancellationToken token = default)
    {
        const string sql = """
            SELECT
                a.Id,
                a.UserId,
                a.Name,
                a.AccountTypeId,
                a.Balance,
                a.CurrencyId,
                a.InitialBalance,
                a.IsActive,
                a.CreatedAt,
                a.UpdatedAt,
                a.RowVersion,

                s.InterestRate,
                s.InterestAccruedToDate,
                s.AccrualFrequency,
                s.CreditFrequency,
                s.LastInterestAccrualAt,
                s.NextInterestAccrualAt,
                s.LastInterestCreditAt,
                s.NextInterestCreditAt

            FROM Accounts a
            LEFT JOIN SavingsDetails s
                ON a.Id = s.AccountId

            WHERE a.Id = @Id
              AND a.UserId = @UserId;
            """;

        using var connection = _connectionFactory.GetConnection();

        return await connection.QuerySingleOrDefaultAsync<AccountModel>(
            new CommandDefinition(
                sql,
                new
                {
                    Id = id,
                    UserId = userId
                },
                cancellationToken: token));
    }

    public async Task<IEnumerable<AccountInfoDto>>
        GetAccountsAsync(
            Guid userId,
            EnAccountType? type,
            EnCurrency? currency,
            string? name,
            int page,
            int pageSize,
            CancellationToken token)
    {
        using var connection = _connectionFactory.GetConnection();

        var sql = new StringBuilder("""
            SELECT
                Id,
                Name,
                AccountTypeId AS Type,
                Balance,
                CurrencyId AS Currency
            FROM Accounts
            WHERE UserId = @Id

            """);

        if (name is not null)
            sql.Append(" AND Name LIKE @Name");

        if (type.HasValue)
            sql.Append(" AND AccountTypeId = @Type");

        if (currency.HasValue)
            sql.Append(" AND CurrencyId = @Currency");

        sql.Append("""

            ORDER BY CreatedAt
            OFFSET @Offset ROWS
            FETCH NEXT @PageSize ROWS ONLY
            """);

        var command = new CommandDefinition(
            sql.ToString(),
            new
            {
                Id = userId,
                Name = $"%{name}%",
                Type = type,
                Currency = currency,
                Offset = pageSize * (page - 1),
                PageSize = pageSize
            },
            cancellationToken: token);

        return await connection.QueryAsync<AccountInfoDto>(command);
    }

    public async Task<IDictionary<Guid, Account>>
        GetUserOwnedAccountsAsync(
            Guid userId,
            CancellationToken token = default)
    {
        const string sql = """
            SELECT
                a.Id,
                a.UserId,
                a.Name,
                a.AccountTypeId,
                a.Balance,
                a.CurrencyId,
                a.InitialBalance,
                a.IsActive,
                a.CreatedAt,
                a.UpdatedAt,
                a.RowVersion,

                s.InterestRate,
                s.InterestAccruedToDate,
                s.AccrualFrequency,
                s.CreditFrequency,
                s.LastInterestAccrualAt,
                s.NextInterestAccrualAt,
                s.LastInterestCreditAt,
                s.NextInterestCreditAt

            FROM Accounts a
            LEFT JOIN SavingsDetails s
                ON a.Id = s.AccountId

            WHERE a.UserId = @UserId;
            """;

        using var connection = _connectionFactory.GetConnection();

        var command = new CommandDefinition(
            sql,
            new { UserId = userId },
            cancellationToken: token);

        var accounts =
            await connection.QueryAsync<AccountModel>(command);

        return accounts.ToDictionary(
            x => x.Id,
            x => AccountMapper.MapToDomain(x));
    }

    public async Task UpdateAccountsAsync(
        IEnumerable<Account> accounts,
        IUnitOfWork? unitOfWork = null,
        CancellationToken token = default)
    {
        var accountModels = accounts
            .Select(AccountMapper.MapToModel)
            .ToList();

        if (unitOfWork is not null)
        {
            await unitOfWork.Connection
                .UseBulkOptions(options =>
                {
                    options.Transaction =
                        (DbTransaction)unitOfWork.Transaction;

                    options.CancellationToken = token;
                })
                .BulkUpdateAsync(accountModels);

            return;
        }

        using var connection = _connectionFactory.GetConnection();

        await connection
            .UseBulkOptions(options =>
            {
                options.CancellationToken = token;
            })
            .BulkUpdateAsync(accountModels);
    }

    public async Task<IEnumerable<Account>>
        GetSavingsAccountsForInterestProcessingAsync(
            CancellationToken cancellationToken)
    {
        const string sql = """
            SELECT
                a.Id,
                a.UserId,
                a.Name,
                a.AccountTypeId,
                a.Balance,
                a.InitialBalance,
                a.CurrencyId,
                a.IsActive,
                a.CreatedAt,
                a.UpdatedAt,
                a.RowVersion,

                s.InterestRate,
                s.InterestAccruedToDate,
                s.AccrualFrequency,
                s.CreditFrequency,
                s.LastInterestAccrualAt,
                s.NextInterestAccrualAt,
                s.LastInterestCreditAt,
                s.NextInterestCreditAt

            FROM Accounts a
            INNER JOIN SavingsDetails s
                ON a.Id = s.AccountId

            WHERE a.IsActive = 1
              AND a.AccountTypeId = @Type;
            """;

        using var connection = _connectionFactory.GetConnection();

        var command = new CommandDefinition(
            sql,
            new
            {
                Type = EnAccountType.Savings
            },
            cancellationToken: cancellationToken);

        var result =
            await connection.QueryAsync<AccountModel>(command);

        return result.Select(AccountMapper.MapToDomain);
    }
}