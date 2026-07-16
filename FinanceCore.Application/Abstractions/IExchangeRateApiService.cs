using FinanceCore.Domain.Enums;

namespace FinanceCore.Application.Abstractions
{
    public interface IExchangeRateApiService
    {
        Task<decimal> GetRateAsync(EnCurrency from, EnCurrency to, CancellationToken token);
    }
}
