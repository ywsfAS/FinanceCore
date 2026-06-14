using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Application.Features.Recurring.queries.GetRecurring
{
    public class GetRecurringValidator : AbstractValidator<GetRecurringQuery>
    {
        public GetRecurringValidator() {
            RuleFor(x => x.UserId).NotEmpty();
        }
    }
}
