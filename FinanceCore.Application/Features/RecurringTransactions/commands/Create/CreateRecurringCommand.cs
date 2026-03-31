using FinanceCore.Application.DTOs.RecurringTransaction;
using FinanceCore.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Application.Features.RecurringTransactions.commands.Create
{
    public record CreateRecurringCommand(
        Guid userId,
        Guid accountId,
        Guid categoryId,
        decimal amount,
        EnTransactionType type,
        EnPeriod period,
        int interval,
        bool isActive,
        string? description,
        DateTime startDate,
        DateTime? endDate
    ) : IRequest<CreateRecurringTransactionDto>;
}
