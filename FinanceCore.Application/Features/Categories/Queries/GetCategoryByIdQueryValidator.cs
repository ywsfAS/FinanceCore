using System;
using FluentValidation;
namespace FinanceCore.Application.Features.Categories.Queries
{
    public class GetCategoryByIdQueryValidator  : AbstractValidator<GetCategoryByIdQuery>
    {
        public GetCategoryByIdQueryValidator() {
            RuleFor(x => x.Id).NotEmpty();  
        
        
        }
    }
}
