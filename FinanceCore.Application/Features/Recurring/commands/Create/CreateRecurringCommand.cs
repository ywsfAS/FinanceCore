using FinanceCore.Application.DTOs.RecurringTransaction;
using FinanceCore.Domain.Common;
using FinanceCore.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Application.Features.Recurring.commands.Create
{
    public record CreateRecurringCommand(
        Guid UserId,
        Guid AccountId,
        Guid CategoryId,
        decimal Amount,
        EnPeriod Period,
        string? Description,
        DateTime StartDate,
        DateTime? EndDate
    ) : IRequest<RecurringTransactionDto>;
}
