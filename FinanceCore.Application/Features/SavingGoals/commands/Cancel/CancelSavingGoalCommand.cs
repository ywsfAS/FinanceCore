using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Application.Features.SavingGoals.commands.Cancel
{
    public record CancelSavingGoalCommand(Guid Id , Guid UserId) : IRequest;
}
