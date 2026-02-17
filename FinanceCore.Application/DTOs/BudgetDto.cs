using FinanceCore.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Application.DTOs
{
    public record BudgetDto(
        Guid Id,
        Guid UserId,
        Guid CategoryId,
        decimal Amount,
        EnCurrency Currency,
        BudgetPeriod Period,
        DateTime StartDate,
        DateTime EndDate);
}
