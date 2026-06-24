using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
