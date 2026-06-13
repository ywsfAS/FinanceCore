using FinanceCore.Application.Abstractions;
using FinanceCore.Domain.Enums;
using Microsoft.Extensions.Logging;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Infrastructure.BackgroundJobs
{
    public class ExchangeRateSyncJob : IJob
    {
        private readonly IExchangeRateApiService _api;
        private readonly IExchangeRateRepository _repo;
        private readonly ILogger<ExchangeRateSyncJob> _logger;

        public ExchangeRateSyncJob(
            IExchangeRateApiService api,
            IExchangeRateRepository repo,
            ILogger<ExchangeRateSyncJob> logger)
        {
            _api = api;
            _repo = repo;
            _logger = logger;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var now = DateTime.UtcNow;

            _logger.LogInformation("ExchangeRateSyncJob started at {Time}", now);

            try
            {
                var currencies = Enum.GetValues<EnCurrency>();

                foreach (var from in currencies)
                {
                    foreach (var to in currencies)
                    {
                        if (from == to) continue;

                        var rate = await _api.GetRateAsync(from, to, context.CancellationToken);

                        await _repo.UpsertRateAsync(from, to, rate, context.CancellationToken);

                        _logger.LogInformation("Updated rate {From}->{To} = {Rate}", from, to, rate);
                    }
                }

                _logger.LogInformation("ExchangeRateSyncJob completed successfully");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "ExchangeRateSyncJob failed");
            }
        }
    }
}
