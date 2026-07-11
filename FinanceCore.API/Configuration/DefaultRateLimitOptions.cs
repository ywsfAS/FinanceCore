using Microsoft.AspNetCore.Routing.Constraints;

namespace FinanceCore.API.Configuration
{
    public class DefaultRateLimitOptions
    {
        public int SegmentsPerWindow { get; set; }
        public int PermitLimit { get; set; }
        public int WindowInMinutes {  get; set; }
    }
}
