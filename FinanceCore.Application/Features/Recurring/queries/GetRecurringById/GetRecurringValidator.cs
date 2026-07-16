using FluentValidation;

namespace FinanceCore.Application.Features.Recurring.Queries.GetRecurringById
{
    class GetRecurringValidator : AbstractValidator<GetRecurringByIdQuery>
    {
        public GetRecurringValidator() { 
            RuleFor(x => x.UserId).NotEmpty();
            RuleFor(x => x.Id).NotEmpty();
        }
    }
}
