using FinanceCore.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Application.Features.SavingGoals.commands.AddContribution
{
    public record AddContributionCommand(Guid UserId ,Guid GoalId,Guid AccountId , decimal Amount , EnCurrency Currency , DateTime ContributionDate,string? Description = null) : IRequest;
}
