using FinanceCore.Domain.Common;
using FinanceCore.Domain.Enums;

namespace FinanceCore.Domain.Accounts
{
    public sealed class CashReconciliation : Entity
    {
        public Guid AccountId { get; private set; }
        public Money ExpectedBalance { get; private set; }
        public Money ActualBalance { get; private set; }
        public Guid? AdjustmentTransactionId { get; private set; }
        public EnAdjustmentStatus Status { get; private set; }
        public EnReconciliationReason Reason {  get; private set; }
        public string? Notes { get; private set; }

        public DateTime ReconciledAt { get; private set; }
        public void MarkAdjusted(Guid transactionId)
        {
            Status = EnAdjustmentStatus.Applied;
            AdjustmentTransactionId = transactionId;
        }
    }
}
