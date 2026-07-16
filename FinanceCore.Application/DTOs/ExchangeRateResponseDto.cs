
namespace FinanceCore.Application.DTOs
{
    public class ExchangeRateResponseDto
    {
        public decimal Amount { get; set; }
        public string Base { get; set; } = default!;
        public DateTime Date { get; set; }
        public Dictionary<string, decimal> Rates { get; set; } = new();
    }
}
