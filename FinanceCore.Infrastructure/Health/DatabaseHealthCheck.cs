using Dapper;
using FinanceCore.Infrastructure.Context;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Data;

namespace FinanceCore.Infrastructure.Health
{
    internal sealed class DatabaseHealthCheck(IConnectionFactory _connection) : IHealthCheck
    {
        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context , CancellationToken token = default)
        {
            try
            {
                using IDbConnection connection = _connection.GetConnection();
                await connection.ExecuteScalarAsync("SELECT 1");
                return HealthCheckResult.Healthy();
            }
            catch(Exception e){ 
                return HealthCheckResult.Unhealthy(exception : e);
            }
        }
    }
}
