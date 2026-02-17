using FinanceCore.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Application.DTOs
{
    public record AccountDto(
        Guid Id,
        Guid UserId,
        string Name,
        EnAccountType Type,
        decimal Balance,
        EnCurrency Currency,
        DateTime CreatedAt);
}
