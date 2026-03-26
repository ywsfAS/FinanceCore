using FinanceCore.Domain.Enums;
using MediatR;
using FinanceCore.Application.DTOs.RecurringTransaction;
using System;

namespace FinanceCore.Application.Features.RecurringTransaction.Commands.Update
{
    public record UpdateRecurringCommand(
        Guid Id,
        Guid AccountId,
        Guid CategoryId,
        decimal Amount,
        EnTransactionType Type,
        EnPeriod Period,
        int Interval,
        string? Description,
        DateTime StartDate,
        DateTime? EndDate,
        bool IsActive
    ) : IRequest<CreateRecurringTransactionDto>;
}
