using MediatR;

namespace FinanceCore.Application.Features.Budgets.Commands.Delete
{
    public record DeleteBudgetCommand(Guid UserId ,Guid Id) : IRequest;
}
