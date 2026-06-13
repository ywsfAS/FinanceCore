using FinanceCore.Application.Abstractions;
using FinanceCore.Application.DTOs;
using FinanceCore.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
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
            var url = $"{_settings.BaseUrl}/convert?{from}&to={to}";

            var response = await _httpClient.GetFromJsonAsync<ExchangeRateResponseDto>(url, token);

            if (response == null || response.Result == 0)
                throw new Exception("Failed to get exchange rate");

            return response.Result;
        }
    }
}
