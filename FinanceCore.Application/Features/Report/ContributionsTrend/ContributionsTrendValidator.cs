using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
