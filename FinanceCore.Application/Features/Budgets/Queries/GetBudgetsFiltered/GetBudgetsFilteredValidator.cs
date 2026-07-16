using FluentValidation;

namespace FinanceCore.Application.Features.Budgets.Queries.GetBudgetsFiltered
{
    public class GetBudgetsFilteredValidator : AbstractValidator<GetBudgetsFilteredQuery>
    {
        public GetBudgetsFilteredValidator() { 
            RuleFor(x => x.userId).NotEmpty();
        
        }
    }
}
