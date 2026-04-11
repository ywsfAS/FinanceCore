using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Application.Features.Report.GetMonthlySummary
{
    public class GetMonthlySummaryValidator : AbstractValidator<GetMonthlySummaryQuery>
    {
       public GetMonthlySummaryValidator() {
            RuleFor(x => x.Id)
                .NotEmpty();
            RuleFor(x => x.month)
                .InclusiveBetween(1, 12);

            RuleFor(x => x.year)
                .GreaterThan(2000);

        }
    }
}
