namespace FinanceCore.API.Configuration
{
    public class ReportingRateLimitOptions
    {
        public int PermitLimit { get; set; }
        public int QueueLimit { get; set; }
    }
}
