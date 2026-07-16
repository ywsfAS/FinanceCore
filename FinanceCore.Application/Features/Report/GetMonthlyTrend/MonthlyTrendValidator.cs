using FluentValidation;

namespace FinanceCore.Application.Features.Report.GetMonthlyTrend
{
    public class MonthlyTrendValidator : AbstractValidator<MonthlyTrendQuery>
    {
        public MonthlyTrendValidator() {
            RuleFor(x => x.userId)
            .NotEmpty();
            RuleFor(x => x.month)
                .InclusiveBetween(1, 12);

        }
    }
}
