using FinanceCore.Application.Abstractions;
using FinanceCore.Application.DTOs;
using FinanceCore.Domain.Enums;
using System.Net.Http.Json;
using Microsoft.Extensions.Options;

namespace FinanceCore.Infrastructure.Services
{
    public class ExchangeRateApiService : IExchangeRateApiService
    {
        private readonly HttpClient _httpClient;
        private readonly ExchangeRateApiSettings _settings;
        public ExchangeRateApiService(HttpClient httpClient , IOptions<ExchangeRateApiSettings> option)
        {
            _httpClient = httpClient;
            _settings = option.Value;
        }

        public async Task<decimal> GetRateAsync(EnCurrency from, EnCurrency to, CancellationToken token)
        {
            var url = $"{_settings.BaseUrl}/latest?from={from}&to={to}";

            var response = await _httpClient.GetFromJsonAsync<ExchangeRateResponseDto>(url, token);

            if (response == null)
                throw new Exception("Failed to get exchange rate");

            if (!response.Rates.TryGetValue(to.ToString(), out var rate))
                throw new Exception($"Rate for {to} not found");

            return rate;
        }
    }
}
