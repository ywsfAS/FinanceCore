using FinanceCore.Domain.Common;
using FinanceCore.Domain.Enums;

namespace FinanceCore.Domain.Accounts
{
    public sealed class Reconciliation : Entity
    {
        public Guid AccountId { get; private set; }
        public Money ExpectedBalance { get; private set; }
        public Money ActualBalance { get; private set; }
        public Guid? AdjustmentTransactionId { get; private set; }
        public EnAdjustmentStatus Status { get; private set; }
        public EnReconciliationReason Reason {  get; private set; }
        public string? Notes { get; private set; }

        public DateTime? ReconciledAt { get; private set; }
        public DateTime CreatedAt { get; private set; }
        private Reconciliation(Guid accountId , Money expectedBalance , Money actualBalance ,
            EnAdjustmentStatus status ,EnReconciliationReason reason 
            ,string? notes  , DateTime? reconciledAt )
        {
            AccountId = accountId;  
            ExpectedBalance = expectedBalance;
            ActualBalance = actualBalance;
            Status = status;
            Reason = reason;
            Notes = notes;
            ReconciledAt = reconciledAt;
            CreatedAt = DateTime.UtcNow;

        }
        public void MarkAdjusted(Guid transactionId)
        {
            Status = EnAdjustmentStatus.Applied;
            AdjustmentTransactionId = transactionId;
        }
        public static Reconciliation Create(Guid accountId , Money expectedBalance , Money actualBalance , EnAdjustmentStatus status = EnAdjustmentStatus.None , EnReconciliationReason reason = EnReconciliationReason.CountingCorrection, string? notes = null , DateTime? reconciledAt = null) {
            return new Reconciliation(accountId, expectedBalance, actualBalance, status, reason, notes, reconciledAt);

        }
    }
}
