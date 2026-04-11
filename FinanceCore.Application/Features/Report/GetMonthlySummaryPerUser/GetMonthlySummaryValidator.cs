using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Application.Features.Report.GetMonthlySummaryPerUser
{
    public class GetMonthlySummaryValidator : AbstractValidator<GetMonthlySummaryQueryUser>
    {
        public GetMonthlySummaryValidator() {
            RuleFor(x => x.userId)
                .NotEmpty();

            RuleFor(x => x.month)
                .InclusiveBetween(1, 12);

            RuleFor(x => x.year)
                .GreaterThan(2000); 
        }
    }
}
