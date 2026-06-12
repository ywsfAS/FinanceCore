using FinanceCore.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Application.DTOs
{
    public record AccountInfoDto(Guid Id , string Name , EnAccountType Type , decimal Balance , EnCurrency Currency);
}
