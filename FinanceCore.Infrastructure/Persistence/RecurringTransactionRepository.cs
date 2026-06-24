using Dapper;
using FinanceCore.Application.Abstractions;
using FinanceCore.Application.DTOs;
using FinanceCore.Application.DTOs.RecurringTransaction;
using FinanceCore.Application.Models;
using FinanceCore.Domain.Enums;
using FinanceCore.Domain.RecurringTransaction;
using FinanceCore.Infrastructure.context;
using FinanceCore.Infrastructure.Mappers;
using System.Collections;
using System.Data;
using System.Text;

namespace FinanceCore.Infrastructure.Persistence
{
    public class RecurringTransactionRepository : IRecurringTransactionRepository
    {
        private readonly IConnectionFactory _connectionFactory;

        public RecurringTransactionRepository(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task<RecurringTransaction?> GetByIdAsync(Guid userId,Guid id)
        {
            using var connection = _connectionFactory.GetConnection();
            var sql = "SELECT rt.Id , rt.AccountId , rt.CategoryId , rt.Amount , a.CurrencyId AS Currency , rt.Description , rt.Type , rt.StartDate , rt.EndDate , rt.Period , rt.IsActive , rt.LastExecutedDate   FROM RecurringTransactions rt INNER JOIN Accounts a ON rt.AccountId = a.Id WHERE rt.Id = @Id AND a.UserId = @UserId ";
            var model = await connection.QuerySingleOrDefaultAsync<RecurringTransactionModel>(sql, new { Id = id , UserId = userId });
            return model == null ? null : RecurringTransactionMapper.MapToDomain(model);
        }
        public async Task<IEnumerable<RecurringTransactionDto>> GetRecurringTransactionsAsync(Guid userId , Guid? accountId , Guid? categoryId , bool? isActive , EnPeriod? period , DateTime? start , DateTime? end ,int page, int pageSize, CancellationToken token)
        {
            using var connection = _connectionFactory.GetConnection();
            var sql = new StringBuilder(@"SELECT rt.Id , rt.AccountId , rt.CategoryId , rt.Amount , a.CurrencyId AS Currency , rt.Description , rt.Type , rt.StartDate , rt.EndDate , rt.Period , rt.IsActive , rt.LastExecutedDate   FROM RecurringTransactions rt INNER JOIN Accounts a ON rt.AccountId = a.Id WHERE a.UserId = @UserId");
            if (accountId.HasValue) sql.Append(" AND rt.AccountId = @AccountId");
            if (categoryId.HasValue) sql.Append(" AND rt.CategoryId =  @CategoryId");
            if (isActive.HasValue) sql.Append(" AND rt.IsActive = @IsActive");
            if(period.HasValue) sql.Append(" AND rt.Period = @Period");
            if (start.HasValue) sql.Append(" AND rt.StartDate <= @Start");
            if (end.HasValue) sql.Append(" AND rt.EndDate > @End");
            sql.Append(" ORDER BY Id DESC OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY");
            var command = new CommandDefinition(sql.ToString(), new { UserId = userId, AccountId = accountId, CategoryId = categoryId, IsActive = isActive, Period = period, Start = start, End = end, Offset = (page - 1) * pageSize, PageSize = pageSize }, cancellationToken: token);
            return await connection.QueryAsync<RecurringTransactionDto>(command);

        }

        public async Task<RecurringTransactionDto?> GetDtoByIdAsync(Guid userId , Guid id)
        {
            using var connection = _connectionFactory.GetConnection();
            var sql = "SELECT rt.Id , rt.AccountId , rt.CategoryId , rt.Amount , a.CurrencyId AS Currency , rt.Description , rt.Type , rt.StartDate , rt.EndDate , rt.Period  FROM RecurringTransactions rt INNER JOIN Accounts a ON rt.AccountId = a.Id WHERE rt.Id = @Id AND a.UserId = @UserId ";
            return await connection.QuerySingleOrDefaultAsync<RecurringTransactionDto>(sql, new { Id = id  , UserId = userId });
        }

        public async Task AddAsync(RecurringTransaction recurringTransaction)
        {
            var model = RecurringTransactionMapper.MapToModel(recurringTransaction);
            const string sql = @"
            INSERT INTO RecurringTransactions (
            Id,
            AccountId,
            CategoryId,
            Amount,
            Description,
            Type,
            StartDate,
            EndDate,
            Period,
            IsActive,
            LastExecutedDate
            )
            VALUES (
            @Id,
            @AccountId,
            @CategoryId,
            @Amount,
            @Description,
            @Type,
            @StartDate,
            @EndDate,
            @Period,
            @IsActive,
            @LastExecutedDate
            );";

            using var connection = _connectionFactory.GetConnection();
            await connection.ExecuteAsync(sql, model);
        }

        public async Task UpdateAsync(RecurringTransaction recurringTransaction)
        {
            var model = RecurringTransactionMapper.MapToModel(recurringTransaction);
            const string sql = @"
            UPDATE RecurringTransactions
            SET
            AccountId = @AccountId,
            CategoryId = @CategoryId,
            Amount = @Amount,
            Description = @Description,
            Type = @Type,
            StartDate = @StartDate,
            EndDate = @EndDate,
            Period = @Period,
            IsActive = @IsActive,
            LastExecutedDate = @LastExecutedDate
            WHERE Id = @Id;";

            using var connection = _connectionFactory.GetConnection();
            await connection.ExecuteAsync(sql, model);
        }

        public async Task DeleteAsync(Guid id)
        {
            using var connection = _connectionFactory.GetConnection();
            const string sql = "DELETE FROM RecurringTransactions WHERE Id = @Id";
            await connection.ExecuteAsync(sql, new { Id = id });
        }
        public async Task<IEnumerable<RecurringTransaction>> GetActiveAsync()
        {
            using var connection = _connectionFactory.GetConnection();
            var sql = "SELECT rt.Id , rt.AccountId , rt.CategoryId , rt.Amount , a.CurrencyId AS Currency , rt.Description ,rt.Type, rt.StartDate , rt.EndDate , rt.Period , rt.IsActive , rt.LastExecutedDate   FROM RecurringTransactions rt INNER JOIN Accounts a ON rt.AccountId = a.Id WHERE rt.IsActive = 1 ";
            var models = await connection.QueryAsync<RecurringTransactionModel>(sql);
            return models.Select(RecurringTransactionMapper.MapToDomain);
        }
        public async Task<IEnumerable<SubsriptionDataDto>> GetSubscriptions(Guid userId , Guid? accountId , Guid? categoryId , string? name , EnPeriod? period , EnTransactionType? type ,int page , int pageSize, CancellationToken token)
        {
            using var connection = _connectionFactory.GetConnection();
            var sql = new StringBuilder(@"
             SELECT 
                rt.Name,
                rt.Amount,
                rt.Period AS Frequency,
                rt.Type,
                a.CurrencyId AS Currency,
                rt.LastExecutedDate AS PreviousCharge
             FROM RecurringTransactions tr
                INNER JOIN Accounts a
                    ON rt.AccountId = a.Id
                WHERE rt.UserId = @UserId 
            ");
            if (accountId.HasValue) sql.Append(" AND AccountId = @AccountId");
            if (categoryId.HasValue) sql.Append(" AND CategoryId = @CategoryId");
            if (name is null) sql.Append(" AND Name LIKE @Name");
            if (period.HasValue) sql.Append(" AND Period = @Period");
            if (type.HasValue) sql.Append(" AND Type = @Type");
            sql.Append(" ORDER BY Id DESC OFFSET @Offset ROWS FETCH NEXT @PageSize ROWS ONLY");

            var command = new CommandDefinition(sql.ToString(), new {UserId = userId , AccountId = accountId , CategoryId = categoryId , Name = $"%{name}%" , Period = period , Type = type , Page = (page - 1) * pageSize , PageSize = pageSize} , cancellationToken : token);
            return await connection.QueryAsync<SubsriptionDataDto>(command);

        }
    }
}
