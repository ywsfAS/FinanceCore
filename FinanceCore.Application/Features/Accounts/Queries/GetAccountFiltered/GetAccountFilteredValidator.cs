using FluentValidation;

namespace FinanceCore.Application.Features.Accounts.Queries.GetAccountFiltered
{
    public class GetAccountFilteredValidator : AbstractValidator<GetAccountFilteredQuery>
    {
        public GetAccountFilteredValidator() { 
           RuleFor(x => x.UserId).NotEmpty();
           RuleFor(x => x.Type).IsInEnum();
           RuleFor(x => x.Currency).IsInEnum();

            
        }
    }
}
