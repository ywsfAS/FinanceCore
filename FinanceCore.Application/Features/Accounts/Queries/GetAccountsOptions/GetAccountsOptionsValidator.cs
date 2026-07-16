using FluentValidation;

namespace FinanceCore.Application.Features.Accounts.Queries.GetAccountByUserOptions
{
   public class GetAccountsOptionsValidator : AbstractValidator<GetAccountsOptionsQuery>
   {
        public GetAccountsOptionsValidator() {
            RuleFor(x => x.userId).NotEmpty();
        }
   }
}
