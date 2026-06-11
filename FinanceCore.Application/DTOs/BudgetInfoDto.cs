using FinanceCore.Domain.Enums;
using FinanceCore.Domain.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Application.DTOs
{
    public record BudgetInfoDto
    (
        Guid Id,
        string Name,
        decimal Amount,
        byte CurrencyId,
        byte Period,
        DateTime StartDate,
        DateTime EndDate,
        string CategoryName
    );
}
