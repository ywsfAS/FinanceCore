using FinanceCore.Domain.Enums;

namespace FinanceCore.Application.DTOs
{
    public class SubscriptionGrowthDto
    {
        public decimal CurrentPeriodTotal { get; set; }
        public decimal PreviousPeriodTotal { get; set; }
        public decimal? ChangePercent { get; set; }
        public EnCurrency Currency { get; set; }
    }
}
