using FluentValidation;

namespace FinanceCore.Application.Features.Report.ContributionsTrend
{
    public class ContributionsTrendValidator : AbstractValidator<ContributionsTrendQuery>
    {
        public ContributionsTrendValidator() { 
        
            RuleFor(x => x.UserId)
            .NotEmpty();
            RuleFor(x => x.LastNMonth)
                .InclusiveBetween(1, 12);
        
        }
    }
}
