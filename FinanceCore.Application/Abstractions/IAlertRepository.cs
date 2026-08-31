
using FinanceCore.Accounts;

namespace FinanceCore.Application.Abstractions
{
    public interface IAlertRepository
    {
        Task<IReadOnlyCollection<LowBalanceAlert>> GetActiveAlertsAsync(Guid accountId , CancellationToken token);
        Task UpdateAsync(LowBalanceAlert alert, CancellationToken token);
        Task CreateAsync(LowBalanceAlert alert, CancellationToken token);
    }
}
