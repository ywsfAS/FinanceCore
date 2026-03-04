using System;

namespace FinanceCore.Application.Models
{
    public class AccountModel
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public string Name { get; set; } = string.Empty;
        public byte AccountTypeId { get; set; } 
        public decimal Balance { get; set; }
        public decimal InitialBalance { get; set; }
        public byte CurrencyId { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }

}
