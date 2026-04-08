using FinanceCore.Application.Models;
using FinanceCore.Domain.Audit;

namespace FinanceCore.Infrastructure.Persistence.Mappers
{
    public static class AuditLogMapper
    {
        public static AuditLogModel MapToModel(AuditLog domain)
        {
            return new AuditLogModel
            {
                Id = domain.Id,
                Action = domain.Action,
                EntityName = domain.EntityName,
                UserId = domain.EntityId,
                CreatedAt = domain.CreatedAt
            };
        }

        public static AuditLog MapToDomain(AuditLogModel model)
        {
            var audit = new AuditLog(
                model.Action,
                model.EntityName,
                model.UserId,
                model.CreatedAt
            );
            return audit;
        }
    }
}
