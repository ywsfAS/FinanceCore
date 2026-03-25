using FinanceCore.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Application.Models
{
    public class RecurringTransactionModel
    {
        public Guid Id { get; set; }

        public Guid AccountId { get; set; }
        public Guid CategoryId { get; set; }

        public decimal Amount { get; set; }
        public string Description { get; set; } = string.Empty;

        public EnTransactionType Type { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public EnPeriod Period { get; set; }
        public int Interval { get; set; }

        public bool IsActive { get; set; }
        public DateTime? LastExecutedDate { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
