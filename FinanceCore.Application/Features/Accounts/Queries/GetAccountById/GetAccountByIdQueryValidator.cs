using FluentValidation;

namespace FinanceCore.Application.Features.Accounts.Queries.GetAccountById
{
    public class GetAccountByIdQueryValidator : AbstractValidator<GetAccountByIdQuery>
    {
        public GetAccountByIdQueryValidator() { 
            RuleFor(x => x.Id).NotEmpty();
        
        }
    }
}
