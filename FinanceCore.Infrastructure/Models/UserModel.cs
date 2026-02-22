using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Infrastructure.Models
{
    public class UserModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public EmailModel Email { get; set; }
        public string PasswordHash { get; set; } = string.Empty;
        public byte DefaultCurrency { get; set; }
        public string? TimeZone { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
    }
}
