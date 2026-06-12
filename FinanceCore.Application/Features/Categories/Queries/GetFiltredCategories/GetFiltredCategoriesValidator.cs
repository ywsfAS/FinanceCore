using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Application.Features.Categories.Queries.GetFiltredCategories
{
    public class GetFiltredCategoriesValidator : AbstractValidator<GetFiltredCategoriesQuery>
    {
        public GetFiltredCategoriesValidator() {
            RuleFor(x => x.UserId).NotEmpty();
        
        }
    }
}
