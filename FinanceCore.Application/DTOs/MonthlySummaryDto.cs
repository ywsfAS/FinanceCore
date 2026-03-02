using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Application.DTOs
{
    public record MonthlySummaryDto(Guid  AccountId , int year , int month , decimal totalIncome , decimal totalExpenses , decimal netSavings);
    
    
}
