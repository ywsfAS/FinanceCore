using FluentValidation;

namespace FinanceCore.Application.Features.Recurring.Commands.Create
{
    public class createRecurringValidator : AbstractValidator<CreateRecurringCommand>
    {
        public createRecurringValidator() { 
            RuleFor(x => x.UserId).NotEmpty();
            RuleFor(x => x.AccountId).NotEmpty();
            RuleFor(x => x.CategoryId).NotEmpty();

            RuleFor(x => x.Period).IsInEnum();
            RuleFor(x => x.Amount).NotEmpty().GreaterThan(0);
            
        
        
        }
    }
}
