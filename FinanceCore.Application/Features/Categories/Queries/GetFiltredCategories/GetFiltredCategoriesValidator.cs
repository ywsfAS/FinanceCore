using FluentValidation;

namespace FinanceCore.Application.Features.Categories.Queries.GetFiltredCategories
{
    public class GetFiltredCategoriesValidator : AbstractValidator<GetFiltredCategoriesQuery>
    {
        public GetFiltredCategoriesValidator() {
            RuleFor(x => x.UserId).NotEmpty();
        
        }
    }
}
