using FinanceCore.Application.DTOs;
using FinanceCore.Domain.Common;
using FinanceCore.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Application.Features.Budgets.Commands.Create
{
    public record CreateBudgetCommand(
        Guid UserId,
        Guid CategoryId,
        string name ,
        Money Amount,
        EnPeriod Period,
        DateTime StartDate
        ) : IRequest<BudgetDto>;
}
