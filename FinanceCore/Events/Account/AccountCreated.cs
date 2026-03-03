using FinanceCore.Domain.Common;
using FinanceCore.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Domain.Events.Account
{
    public record AccountCreated(
        Guid AccountId,
        string Name,
        EnAccountType Type,
        EnCurrency Currency,
        Money InitialBalance) : DomainEvent;
}
