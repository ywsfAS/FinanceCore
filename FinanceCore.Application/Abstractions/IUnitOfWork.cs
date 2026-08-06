
using System.Data;

namespace FinanceCore.Application.Abstractions
{
    public interface IUnitOfWork  : IAsyncDisposable
    {

        Task BeginAsync(CancellationToken token = default);
        IDbTransaction Transaction { get; }
        IDbConnection Connection { get; }
        Task CommitAsync(CancellationToken token = default);
        Task RollBackAsync(CancellationToken token = default);

    }
}
