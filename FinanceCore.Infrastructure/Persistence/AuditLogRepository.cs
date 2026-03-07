using Dapper;
using FinanceCore.Application.Abstractions;
using FinanceCore.Domain.Audit;
using FinanceCore.Infrastructure.context;
using FinanceCore.Infrastructure.Persistence.Mappers;

namespace FinanceCore.Infrastructure.Persistence
{
    public class AuditLogRepository : IAuditRepository
    {
        private readonly IConnectionFactory _connectionFactory;
        public AuditLogRepository(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }

        public async Task LogAsync(AuditLog audit, CancellationToken token = default)
        {
            const string sql = @"
        INSERT INTO AuditLogs
        (
            Id,
            Action,
            UserId,
            EntityName,
            Details,
            CreatedAt
        )
        VALUES
        (
            @Id,
            @Action,
            @UserId,
            @EntityName,
            @Details,
            @CreatedAt
        );";

            using var connection = _connectionFactory.GetConnection();
            var model = AuditLogMapper.MapToModel(audit);

            await connection.ExecuteAsync(sql, model);
        }
    }
}
