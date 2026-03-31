using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Application.Features.RecurringTransactions.queries.GetRecurringById
{
    public record GetRecurringByIdQuery(Guid userId , Guid id);
}
