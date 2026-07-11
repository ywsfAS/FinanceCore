namespace FinanceCore.API.Configuration
{
    public class RateLimitingOptions
    {
        public AuthenticationRateLimitOptions Authentication { get; set; } = new();
        public DefaultRateLimitOptions Default { get; set; } = new();
        public ReportingRateLimitOptions Reporting { get; set; } = new();
    }
}
