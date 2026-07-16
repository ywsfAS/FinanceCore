using FinanceCore.Domain.Enums;
namespace FinanceCore.Application.DTOs
{

    public record ContributionsTrendDataDto(string Month , decimal SavedAmount  ,decimal TargetAmount , decimal SavedPercentage );
    public class ContributionsTrendDto
    {
        public EnCurrency Currency { get; set; }
        public IEnumerable<ContributionsTrendDataDto> History { get; set; }

    }
}
