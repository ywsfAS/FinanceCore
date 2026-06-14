using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Application.Features.Recurring.queries.GetRecurringById
{
    class GetRecurringValidator : AbstractValidator<GetRecurringByIdQuery>
    {
        public GetRecurringValidator() { 
            RuleFor(x => x.UserId).NotEmpty();
            RuleFor(x => x.Id).NotEmpty();
        }
    }
}
