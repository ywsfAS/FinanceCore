
using FinanceCore.Domain.Accounts;

namespace FinanceCore.Application.Abstractions
{
    public interface IReconciliationRepository
    {
        Task AddAsync(Reconciliation reconciliation,IUnitOfWork? unitOfWork = null, CancellationToken token = default);
    }
}
