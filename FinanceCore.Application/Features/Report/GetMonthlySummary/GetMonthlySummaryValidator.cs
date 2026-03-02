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
        public GetMonthlySummaryValidator() { }
    }
}
