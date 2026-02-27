using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Application.DTOs
{
    public record AccountBalanceDto(Guid AccountId , string Name , decimal Balance);
}
