using FinanceCore.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Application.Features.Budgets.Queries.GetBudgetById
{
    public record GetBudgetByIdQuery(Guid UserId,Guid Id) : IRequest<BudgetDto>;
}
