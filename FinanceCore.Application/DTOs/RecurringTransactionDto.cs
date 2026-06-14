using FinanceCore.Domain.Enums;
using System;

namespace FinanceCore.Application.DTOs.RecurringTransaction
{
    public class RecurringTransactionDto
    {
        public Guid Id { get; set; }
        public Guid AccountId { get; set; }
        public Guid CategoryId { get; set; }

        public decimal Amount { get; set; }
        public EnCurrency Currency { get; set; }
        public string Description { get; set; } = string.Empty;

        public EnTransactionType Type { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public EnPeriod Period { get; set; }
    }
}
