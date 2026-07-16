using FinanceCore.Application.DTOs.Goal;
using MediatR;

namespace FinanceCore.Application.Features.SavingGoals.Queries.GetSavingGoalById
{
    public sealed record GetSavingGoalQuery(Guid userId,Guid id) : IRequest<SavingsGoalDto>;
}
