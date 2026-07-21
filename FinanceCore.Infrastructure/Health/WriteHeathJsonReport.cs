using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System;
using System.Text.Json;

namespace FinanceCore.Infrastructure.Health
{
    public class WriteHeathJsonReport
    {
        public static Task WriteHealthCheckResponse( HttpContext context, HealthReport report)
        {
            context.Response.ContentType = "application/json";

            var response = new
            {
                status = report.Status.ToString(),
                checks = report.Entries.Select(entry => new
                {
                    name = entry.Key,
                    status = entry.Value.Status.ToString(),
                    duration = entry.Value.Duration,
                    description = entry.Value.Description
                })
            };

            return context.Response.WriteAsync(
                JsonSerializer.Serialize(response));
        }
    }
}
