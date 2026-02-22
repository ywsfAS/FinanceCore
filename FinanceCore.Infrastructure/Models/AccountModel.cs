using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Infrastructure.Models
{
    public class AccountModel
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Name { get; set; } = string.Empty;
        public byte Type { get; set; } 
        public decimal Balance { get; set; }
        public byte BalanceCurrency { get; set; }
        public decimal InitialBalance { get; set; }
        public byte InitialBalanceCurrency { get; set; }
        public byte Currency { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }

}
