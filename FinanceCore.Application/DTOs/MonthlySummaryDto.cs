using FinanceCore.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Application.DTOs
{
    public record MonthlySummaryDto(Guid AccountId , decimal TotalIncome , decimal TotalExpense , decimal NetSavings, EnCurrency Currency);
    
    
}
