using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Application.Features.Report.GetMonthlySummary
{
    public class GetMonthlySummaryValidator : AbstractValidator<GetAccountsMonthlySummaryQuery>
    {
       public GetMonthlySummaryValidator() {
            RuleFor(x => x.UserId).NotEmpty();
            RuleFor( x => x.AccountId).NotEmpty();
            RuleFor(x => x.Year).NotEmpty().InclusiveBetween(2026,9999);
            RuleFor(x => x.Month).NotEmpty().InclusiveBetween(1, 12);

        }
    }
}
