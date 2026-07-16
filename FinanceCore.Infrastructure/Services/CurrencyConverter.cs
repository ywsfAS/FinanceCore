using FinanceCore.Application.Abstractions;
using FinanceCore.Domain.Enums;

namespace FinanceCore.Infrastructure.Services
{
    public class CurrencyConverter : ICurrencyConverter
    {
        private readonly IExchangeRateRepository _repo;
        public CurrencyConverter(IExchangeRateRepository repo)
        {
            _repo = repo;
        }
        public async Task<decimal> Convert(decimal amount ,  EnCurrency fromCurrency , EnCurrency toCurrency,CancellationToken token = default)
        {
            // same currency no conversion 
            if (fromCurrency == toCurrency) return amount;

            var rate = await _repo.GetRateAsync(fromCurrency,toCurrency,token);

            return amount * rate;

        }
    }
}
