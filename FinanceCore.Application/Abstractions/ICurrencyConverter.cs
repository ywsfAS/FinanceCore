using FinanceCore.Domain.Enums;

namespace FinanceCore.Application.Abstractions
{
    public interface ICurrencyConverter
    {
        Task<decimal> Convert(decimal amout, EnCurrency fromCurreny, EnCurrency toCurrency,CancellationToken token = default);
    }
}
