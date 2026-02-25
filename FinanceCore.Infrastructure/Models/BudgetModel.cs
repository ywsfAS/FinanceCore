using FinanceCore.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Infrastructure.Models
{
    public class BudgetModel
    {
        public Guid Id { get; set; }

        public Guid UserId { get; set; }
        public Guid CategoryId { get; set; }

        public string Name { get; set; } = string.Empty;

        public decimal Amount { get; set; }
        public byte CurrencyId { get; set; }   

        public BudgetPeriod Period { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
