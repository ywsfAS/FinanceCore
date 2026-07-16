using FinanceCore.Application.DTOs;
using MediatR;

namespace FinanceCore.Application.Features.Budgets.Queries.GetBudgetById
{
    public record GetBudgetByIdQuery(Guid UserId,Guid Id) : IRequest<BudgetDto>;
}
