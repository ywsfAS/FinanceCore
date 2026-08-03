using FinanceCore.Domain.Common;

namespace FinanceCore.Domain.Batch
{
    public sealed class Batch : Entity
    {
        public Guid AccountId { get; set; }
        public string FileName { get; set; }
        public DateTime ImportedAt { get; set; }
        public int TransactionCount { get; set; }




    }
}
