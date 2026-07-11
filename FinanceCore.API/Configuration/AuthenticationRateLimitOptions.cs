namespace FinanceCore.API.Configuration
{
    public class AuthenticationRateLimitOptions
    {
        public int WindowInMinutes { get; set; }
        public int PermitLimit { get; set; }
    }
}
