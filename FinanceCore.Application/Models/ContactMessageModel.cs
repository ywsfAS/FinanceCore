using FinanceCore.Domain.Common;
using FinanceCore.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Application.Models
{
    public class ContactMessageModel
    {
        public Guid Id { get; set; }
        public string FullName { get; set; } = string.Empty;
        public string Email { get; set; }
        public byte Subject { get; set; }
        public string Message { get; set; } = string.Empty;
        public bool IsProccessed { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
