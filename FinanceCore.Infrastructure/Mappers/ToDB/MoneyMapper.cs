using FinanceCore.Domain.Common;
using FinanceCore.Infrastructure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Infrastructure.Mappers.ToDB
{
    public static class MoneyMapper 
    {
        public static MoneyModel MapToModel(Money money)
        {
            return new MoneyModel { Balance = money.Amount , Currency = (byte)money.Currency };

        }
    }
}
