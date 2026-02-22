using FinanceCore.Domain.Common;
using FinanceCore.Domain.Enums;
using FinanceCore.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Infrastructure.Mappers.ToDomain
{
    public static class MoneyMapper
    {
        public static Money MapToDomain(MoneyModel model)
        {
            return new Money(model.Balance , (EnCurrency)model.Currency);

        }

    }
}
