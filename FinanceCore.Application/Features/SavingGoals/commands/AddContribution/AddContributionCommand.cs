using FinanceCore.Domain.Enums;
using MediatR;

namespace FinanceCore.Application.Features.SavingGoals.Commands.AddContribution
{
    public record AddContributionCommand(Guid UserId ,Guid GoalId,Guid AccountId , decimal Amount , EnCurrency Currency , DateTime ContributionDate,string? Description = null) : IRequest;
}
