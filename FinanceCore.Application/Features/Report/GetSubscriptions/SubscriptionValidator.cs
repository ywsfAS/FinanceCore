using FluentValidation;

namespace FinanceCore.Application.Features.Report.GetSubscriptions
{
    public class SubscriptionValidator :AbstractValidator<SubscriptionQuery>
    {
        public SubscriptionValidator() { 
            RuleFor(x => x.UserId).NotEmpty();
            RuleFor(x => x.Page).GreaterThanOrEqualTo(1).NotEmpty();
            RuleFor(x => x.PageSize).GreaterThanOrEqualTo(1).NotEmpty();
        
        }
    }
}
