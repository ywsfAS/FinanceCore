using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Application.Models
{
    public class AuditLogModel
    {
        public Guid Id { get; set; }
        public string Action { get; set; } = string.Empty;
        public string EntityName { get; set; } = string.Empty;
        public Guid EntityId { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
