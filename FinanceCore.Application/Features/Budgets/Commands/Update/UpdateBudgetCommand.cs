using FinanceCore.Domain.Common;
using FinanceCore.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Application.Features.Budgets.Commands.Update
{
    public record UpdateBudgetCommand(
        Guid UserId,
        Guid Id,
        string Name,
        Money Amount,
        EnPeriod Period,
        DateTime StartDate
        ) : IRequest;
}
