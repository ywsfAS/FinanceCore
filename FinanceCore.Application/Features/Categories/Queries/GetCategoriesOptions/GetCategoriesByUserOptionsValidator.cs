using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Application.Features.Categories.Queries.GetCategoriesByUserOptions
{
    public class GetCategoriesByUserOptionsValidator : AbstractValidator<GetCategoriesByUserOptionsQuery>
    {
        public GetCategoriesByUserOptionsValidator() {
            RuleFor(x => x.UserId).NotEmpty();
        }
    }
}
