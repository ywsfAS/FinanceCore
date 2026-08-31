using FinanceCore.Domain.Enums;

namespace FinanceCore.Application.Models
{
    public class RecurringTransactionModel
    {
        public Guid Id { get; set; }

        public Guid AccountId { get; set; }
        public Guid CategoryId { get; set; }

        public decimal Amount { get; set; }
        public EnCurrency Currency { get; set; }
        public string Description { get; set; } = string.Empty;

        public EnTransactionType Type { get; set; }
        public EnExecutionType ExecutionType { get; set; }
        public EnRecurringTransactionStatus Status { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public EnPeriod Period { get; set; }
 
        public DateTime? LastExecutedDate { get; set; }
        public DateTime? NextExecutionAt { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
