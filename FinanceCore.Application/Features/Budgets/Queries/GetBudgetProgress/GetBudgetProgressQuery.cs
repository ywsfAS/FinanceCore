using FinanceCore.Application.DTOs;
using MediatR;

namespace FinanceCore.Application.Features.Budgets.Queries.GetBudgetProgress
{
    public record GetBudgetProgressQuery(Guid UserId,Guid Id) : IRequest<BudgetProgressDto>;
   
}
