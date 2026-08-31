using FinanceCore.Domain.Enums;

namespace FinanceCore.API.Requests.Account
{
    public sealed record ReconcileAccountRequest(
        decimal ActualBalance,
        EnReconciliationReason Reason,
        string? Notes,
        bool CreateAdjustment);
}
