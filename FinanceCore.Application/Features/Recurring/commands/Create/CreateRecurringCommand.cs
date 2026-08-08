using FinanceCore.Application.DTOs.RecurringTransaction;
using FinanceCore.Domain.Enums;
using MediatR;

namespace FinanceCore.Application.Features.Recurring.Commands.Create
{
    public record CreateRecurringCommand(
        Guid UserId,
        Guid AccountId,
        Guid CategoryId,
        decimal Amount,
        EnPeriod Period,
        string? Description,
        EnExecutionType ExecutionType,
        DateTime StartDate,
        DateTime? EndDate
    ) : IRequest<RecurringTransactionDto>;
}
