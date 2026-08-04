using Dapper;
using FinanceCore.Application.Abstractions;
using FinanceCore.Application.DTOs;
using FinanceCore.Domain.Enums;
using FinanceCore.Domain.LoginHistory;

using FinanceCore.Infrastructure.Context;
using System.Data.Common;

namespace FinanceCore.Infrastructure.Persistence
{
    public class LoginHistoryRepository : ILoginHistoryRepository   
    {

        private readonly IConnectionFactory _connectionFactory;

        public LoginHistoryRepository(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }
        public async Task AddAsync(
            LoginHistory history,
            IUnitOfWork? unitOfWork,
            CancellationToken token = default)
        {
            const string sql = """
            INSERT INTO LoginHistory
            (
            Id,
            UserId,
            LoginAt,
            IpAddress,
            UserAgent,
            DeviceName,
            Os,
            Status,
            FailureReason
            )
            VALUES
            (
            @Id,
            @UserId,
            @LoginAt,
            @IpAddress,
            @UserAgent,
            @DeviceName,
            @Os,
            @Status,
            @FailureReason
            );
        """;

            var parameters = new
            {
                history.Id,
                history.UserId,
                history.LoginAt,
                history.IpAddress,
                history.UserAgent,
                history.DeviceName,
                history.Os,
                history.Status,
                history.FailureReason
            };

            if (unitOfWork is not null)
            {
                await unitOfWork.Connection.ExecuteAsync(
                    new CommandDefinition(
                        sql,
                        parameters,
                        unitOfWork.Transaction,
                        cancellationToken: token));

                return;
            }
            using var connection = _connectionFactory.GetConnection();
            await connection.ExecuteAsync(
                new CommandDefinition(
                    sql,
                    parameters,
                    cancellationToken: token));
        }
        public async Task<PagedResult<LoginHistoryDto>> GetLoginHistoriesFilteredAsync(
            Guid userId,
            EnLoginStatus? status,
            string? search,
            DateTime? from,
            DateTime? to,
            int page,
            int pageSize,
            CancellationToken token = default)
        {
            const string sql = """
            SELECT
            Id,
            LoginAt,
            IpAddress,
            UserAgent,
            DeviceName,
            Os,
            Status,
            FailureReason
            FROM LoginHistory
                WHERE UserId = @UserId
                    AND (@Status IS NULL OR Status = @Status)
                    AND (@From IS NULL OR LoginAt >= @From)
                    AND (@To IS NULL OR LoginAt < @To)
                    AND (
                    @Search IS NULL
                    OR IpAddress LIKE '%' + @Search + '%'
                    OR DeviceName LIKE '%' + @Search + '%'
                    OR Os LIKE '%' + @Search + '%'
                    OR UserAgent LIKE '%' + @Search + '%'
                ) ORDER BY LoginAt DESC
            OFFSET @Offset ROWS
            FETCH NEXT @PageSize ROWS ONLY; 

            SELECT COUNT(*)
            FROM LoginHistory
                WHERE UserId = @UserId
                    AND (@Status IS NULL OR Status = @Status)
                    AND (@From IS NULL OR LoginAt >= @From)
                    AND (@To IS NULL OR LoginAt < @To)
                    AND (
                        @Search IS NULL
                        OR IpAddress LIKE '%' + @Search + '%'
                        OR DeviceName LIKE '%' + @Search + '%'
                        OR Os LIKE '%' + @Search + '%'
                        OR UserAgent LIKE '%' + @Search + '%'
                        );
            """;

            page = Math.Max(page, 1);
            pageSize = Math.Clamp(pageSize, 1, 100);

            var parameters = new
            {
                UserId = userId,
                Status = status,
                Search = string.IsNullOrWhiteSpace(search)
                    ? null
                    : search.Trim(),
                From = from,
                To = to,
                Offset = (page - 1) * pageSize,
                PageSize = pageSize
            };

            var command = new CommandDefinition(
                sql,
                parameters,
                cancellationToken: token);
            using var connection = _connectionFactory.GetConnection();
            using var multi = await connection.QueryMultipleAsync(command);

            var items = (await multi.ReadAsync<LoginHistoryDto>()).ToList();

            var totalCount = await multi.ReadSingleAsync<int>();

            return new PagedResult<LoginHistoryDto>(
                items,
                totalCount,
                page,
                pageSize);
        }
    }
}
