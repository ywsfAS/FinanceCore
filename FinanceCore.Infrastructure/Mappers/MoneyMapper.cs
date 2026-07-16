using FinanceCore.Application.Models;
using FinanceCore.Domain.Common;
using FinanceCore.Domain.Enums;

namespace FinanceCore.Infrastructure.Mappers
{
    public static class MoneyMapper 
    {
        public static MoneyModel MapToModel(Money money)
        {
            return new MoneyModel { Balance = money.Amount , Currency = (byte)money.Currency};

        }

        public static Money MapToDomain(MoneyModel model)
        {
            return new Money(model.Balance,(EnCurrency)model.Currency);

        }
    }
}
