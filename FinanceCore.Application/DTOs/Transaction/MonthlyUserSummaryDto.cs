using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Application.DTOs.Transaction
{
    public record MonthlyUserSummaryDto(Guid userId , decimal totalIncome , decimal totalExpense);
}
