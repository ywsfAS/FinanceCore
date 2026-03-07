using FinanceCore.Domain.Audit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Application.Abstractions
{
    public interface IAuditRepository
    {
        Task LogAsync(AuditLog audit,CancellationToken token = default);
    }
}
