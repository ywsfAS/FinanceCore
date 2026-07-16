using FluentValidation;

namespace FinanceCore.Application.Features.Recurring.Queries.GetRecurring
{
    public class GetRecurringValidator : AbstractValidator<GetRecurringQuery>
    {
        public GetRecurringValidator() {
            RuleFor(x => x.UserId).NotEmpty();
        }
    }
}
