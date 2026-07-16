using FinanceCore.Application.DTOs;
using FinanceCore.Domain.Common;
using FinanceCore.Domain.Enums;
using MediatR;

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
