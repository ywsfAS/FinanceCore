using FinanceCore.Domain.Enums;

namespace FinanceCore.Application.DTOs
{
    public class SubsriptionDataDto {
        public string Name { get; set; }
        public decimal Amount { get; set; }
        public EnPeriod Frequency { get; set; } 
        public EnCurrency Currency { get; set; }
        public DateTime PerviousCharge {  get; set; }
        public DateTime NextCharge {  get; set; }
        public EnTransactionType Type { get; set; }
            
     };
     public record SubscriptionDto(decimal totalSubscription , IEnumerable<SubsriptionDataDto> Subscriptions , PaginationDto pagination);
}
