using FinanceCore.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Domain.Audit
{
    public class AuditLog : Entity
    {
        public string Action { get; private set; } = string.Empty;
        public string EntityName { get; private set; } = string.Empty;
        public Guid EntityId { get; private set; }
        public DateTime CreatedAt { get; private set; }

        public AuditLog(string action, string entityName, Guid entityId, DateTime createdAt)
        {
            Action = action;
            EntityName = entityName;
            EntityId = entityId;
            CreatedAt = createdAt;
        }   
    }
}
