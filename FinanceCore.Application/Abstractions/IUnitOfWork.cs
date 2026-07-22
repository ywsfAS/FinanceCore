
using System.Data;

namespace FinanceCore.Application.Abstractions
{
    public interface IUnitOfWork  : IAsyncDisposable
    {

        Task BeginAsync(CancellationToken token);
        IDbTransaction Transaction { get; }
        IDbConnection Connection { get; }
        Task CommitAsync(CancellationToken token);
        Task RollBackAsync(CancellationToken token);

    }
}
