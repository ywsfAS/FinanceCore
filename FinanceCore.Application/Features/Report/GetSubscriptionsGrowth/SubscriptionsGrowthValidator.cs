using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Application.Features.Report.GetSubscriptionsGrowth
{
    public class SubscriptionsGrowthValidator : AbstractValidator<SubscriptionGrowthQuery>
    {
        public SubscriptionsGrowthValidator() {
            RuleFor(x => x.UserId).NotEmpty();
            RuleFor(x => x.Start).NotEmpty();
            RuleFor(x => x.End).NotEmpty();
            RuleFor(x => x.Type).IsInEnum();
        }
    }
}
