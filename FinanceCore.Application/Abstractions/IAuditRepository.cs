using FinanceCore.Domain.Audit;

namespace FinanceCore.Application.Abstractions
{
    public interface IAuditRepository
    {
        Task LogAsync(AuditLog audit,CancellationToken token = default);
    }
}
