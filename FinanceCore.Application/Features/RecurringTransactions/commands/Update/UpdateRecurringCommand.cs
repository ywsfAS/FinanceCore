using FinanceCore.Domain.Enums;
using MediatR;
using FinanceCore.Application.DTOs.RecurringTransaction;
using System;
using FinanceCore.Domain.Common;

namespace FinanceCore.Application.Features.RecurringTransaction.Commands.Update
{
    public record UpdateRecurringCommand(
        Guid userId,
        Guid Id,
        Guid AccountId,
        Guid CategoryId,
        Money Amount,
        EnTransactionType Type,
        EnPeriod Period,
        int Interval,
        string? Description,
        DateTime StartDate,
        DateTime? EndDate,
        bool IsActive
    ) : IRequest<CreateRecurringTransactionDto>;
}
