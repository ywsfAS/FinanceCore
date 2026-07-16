using FinanceCore.Domain.Enums;
using MediatR;
using FinanceCore.Application.DTOs.RecurringTransaction;

namespace FinanceCore.Application.Features.Recurring.Commands.Update
{
    public record UpdateRecurringCommand(
        Guid Id,
        Guid UserId,
        Guid AccountId,
        Guid CategoryId,
        decimal Amount,
        EnPeriod Period,
        string? Description,
        DateTime StartDate,
        DateTime? EndDate,
        bool IsActive
    ) : IRequest<RecurringTransactionDto>;
}
