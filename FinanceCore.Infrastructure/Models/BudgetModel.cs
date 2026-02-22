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
        public string Name { get; set; }
        public Guid UserId { get; set; }
        public Guid CategoryId { get; set; }
        public MoneyModel Amount { get; set; }
        public MoneyModel SpentMoney { get; set; }
        public byte Period { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }

}
