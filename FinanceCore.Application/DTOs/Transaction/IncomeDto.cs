using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Application.DTOs.Transaction
{
    public record IncomeDto(Guid AccountId , Guid? CategoryId , decimal Amount , string Description);
}
