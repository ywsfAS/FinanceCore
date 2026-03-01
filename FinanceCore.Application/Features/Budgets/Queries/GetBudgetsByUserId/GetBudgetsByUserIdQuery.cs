using FinanceCore.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Application.Features.Budgets.Queries.GetBudgetsByUserId
{
    public record GetBudgetsByUserIdQuery(Guid UserId) : IRequest<IEnumerable<BudgetDto>?>;
    
}
