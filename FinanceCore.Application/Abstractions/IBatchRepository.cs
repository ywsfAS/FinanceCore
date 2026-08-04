
using FinanceCore.Domain.Batch;

namespace FinanceCore.Application.Abstractions
{
    public interface IBatchRepository
    {
        Task AddAsync(Batch batch ,IUnitOfWork? unitOfWork = null ,CancellationToken token = default);
    }
}
