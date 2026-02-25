using FinanceCore.Domain.Common;
using FinanceCore.Domain.Enums;
using FinanceCore.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Infrastructure.Mappers
{
    public static class MoneyMapper 
    {
        public static MoneyModel MapToModel(Money money)
        {
            return new MoneyModel { Balance = money.Amount};

        }

        public static Money MapToDomain(MoneyModel model)
        {
            return new Money(model.Balance);

        }
    }
}
