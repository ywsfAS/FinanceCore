using FinanceCore.Application.DTOs.Goal;
using FinanceCore.Domain.Enums;
using MediatR;

namespace FinanceCore.Application.Features.SavingGoals.Queries.GetSavingGoalsFiltered
{
    public sealed record GetSavingsGoalFilteredQuery(Guid userId,string? name,EnCurrency? Currency, EnGoalStatus? Status , int Page = 1 , int PageSize = 5) : IRequest<IEnumerable<SavingsGoalDto>>;
}
