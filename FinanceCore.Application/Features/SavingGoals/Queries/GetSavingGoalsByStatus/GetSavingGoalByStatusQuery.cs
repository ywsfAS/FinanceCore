using FinanceCore.Application.DTOs.Goal;
using FinanceCore.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Application.Features.SavingGoals.Queries.GetSavingGoalsByStatus
{
    public record GetSavingGoalByStatusQuery(Guid UserId , EnGoalStatus Status, int Page = 1 , int PageSize = 5) : IRequest<IEnumerable<SavingsGoalDto>?>;
}
