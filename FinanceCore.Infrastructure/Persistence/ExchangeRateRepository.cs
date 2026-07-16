using Dapper;
using FinanceCore.Application.Abstractions;
using FinanceCore.Domain.Enums;
using FinanceCore.Infrastructure.Context;

namespace FinanceCore.Infrastructure.Persistence
{
    public class ExchangeRateRepository : IExchangeRateRepository
    {
        private readonly IConnectionFactory _connectionFactory;
        public ExchangeRateRepository(IConnectionFactory connectionFactory)
        {
            _connectionFactory = connectionFactory;
        }
        public async Task<decimal> GetRateAsync(EnCurrency from , EnCurrency to,CancellationToken token)
        {
            using var connection = _connectionFactory.GetConnection();
            const string sql = @"SELECT Rate FROM ExchangeRates WHERE SourceCurrencyId = @From AND TargetCurrencyId = @To";
            var command = new CommandDefinition(sql, new { From = from, To = to }, cancellationToken: token);
            var rate = await connection.ExecuteScalarAsync<decimal>(command);
            return rate;
        }

        public async Task UpsertRateAsync(EnCurrency from , EnCurrency to,decimal rate ,CancellationToken token)
        {
            using var connection = _connectionFactory.GetConnection();
            const string sql = @"
            IF EXISTS (
            SELECT 1 FROM ExchangeRates
            WHERE SourceCurrencyId = @From
            AND TargetCurrencyId = @To
            )
            BEGIN
            UPDATE ExchangeRates
            SET Rate = @Rate,
            LastUpdate = SYSUTCDATETIME()
            WHERE SourceCurrencyId = @From
            AND TargetCurrencyId = @To
            END
            ELSE
            BEGIN
            INSERT INTO ExchangeRates (Id, SourceCurrencyId, TargetCurrencyId, Rate, LastUpdate)
            VALUES (NEWID(), @From, @To, @Rate, SYSUTCDATETIME())
            END
            ";
            var command = new CommandDefinition(sql, new { From = from, To = to , Rate = rate}, cancellationToken: token);
             await connection.ExecuteAsync(command);
        }

    }
}
