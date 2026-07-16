using FinanceCore.Domain.Enums;

namespace FinanceCore.Application.DTOs
{
    public record PaginationDto(int total , int PageNumber, int PageSize);
    public class BudgetHealthDataDto {
            public Guid Id { get; set; }
            public string Name { get ; set; }
            public decimal Amount { get; set; }
            public decimal Spent { get; set; }
            public EnCurrency Currency {  get; set; }
            public decimal UsagePercentage {  get; set; }
            public EnBudgetHealthStatus Status { get; set; } = EnBudgetHealthStatus.Unknown;
    };
    public record BudgetHealthDto(Guid UserId , decimal score, IEnumerable<BudgetHealthDataDto> budgets , PaginationDto pagination );
}
