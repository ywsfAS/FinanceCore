using FinanceCore.Application.DTOs.Goal;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Application.Features.SavingGoals.Queries.GetSavingGoalsPerUser
{
    public sealed record GetSavingsGoalPerUserQuery(Guid userId) : IRequest<IEnumerable<SavingsGoalDto>>;
}
