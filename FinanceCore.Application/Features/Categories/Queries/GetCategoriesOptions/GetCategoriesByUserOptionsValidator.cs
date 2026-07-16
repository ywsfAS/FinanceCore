using FluentValidation;

namespace FinanceCore.Application.Features.Categories.Queries.GetCategoriesByUserOptions
{
    public class GetCategoriesByUserOptionsValidator : AbstractValidator<GetCategoriesByUserOptionsQuery>
    {
        public GetCategoriesByUserOptionsValidator() {
            RuleFor(x => x.UserId).NotEmpty();
        }
    }
}
