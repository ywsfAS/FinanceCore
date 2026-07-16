using FluentValidation;


namespace FinanceCore.Application.Features.Budgets.Queries.GetBudgetById
{
    public class GetBudgetByIdQueryValidator : AbstractValidator<GetBudgetByIdQuery>
    {
        public GetBudgetByIdQueryValidator() { 
            RuleFor(x => x.Id).NotEmpty();  
        }
    }
}
