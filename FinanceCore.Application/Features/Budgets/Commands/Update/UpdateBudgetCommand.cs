using FinanceCore.Domain.Common;
using FinanceCore.Domain.Enums;
using MediatR;

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
