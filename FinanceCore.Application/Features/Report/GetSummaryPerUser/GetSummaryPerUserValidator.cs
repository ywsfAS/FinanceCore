using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Application.Features.Report.GetSummaryPerUser
{
    public class GetSummaryPerUserValidator : AbstractValidator<GetSummaryPerUserQuery>
    {
        public GetSummaryPerUserValidator() {
            RuleFor(x => x.userId)
                .NotEmpty();

        }
    }
}
