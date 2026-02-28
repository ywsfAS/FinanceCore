using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Application.DTOs
{
    public record BudgetProgressDto(decimal BudgetAmount , decimal SpentAmount , decimal Remaining , decimal PercentageUsed , bool IsExceeded);
}
