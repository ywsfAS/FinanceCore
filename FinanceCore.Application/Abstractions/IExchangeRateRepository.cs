using FinanceCore.Domain.Enums;

namespace FinanceCore.Application.Abstractions
{
    public interface IExchangeRateRepository
    {
        Task<decimal> GetRateAsync(EnCurrency from , EnCurrency to,CancellationToken token);
        Task UpsertRateAsync(EnCurrency from, EnCurrency to, decimal rate, CancellationToken token);
    }
}
