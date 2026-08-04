using FinanceCore.Domain.Enums;

namespace FinanceCore.Application.Models
{
    public class TransactionModel
    {
        public Guid Id { get; set; }
        public Guid AccountId { get; set; } // Only for transfer 
        public Guid? ToAccountId { get; set; }
        public EnTransactionType Type { get; set; }
        public Guid? CategoryId { get; set; }
        public decimal Amount { get; set; }
        public EnCurrency Currency { get; set; }
        public DateTime Date { get; set; }
        public string? Description { get; set; }
        public Guid? BatchId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }

}
