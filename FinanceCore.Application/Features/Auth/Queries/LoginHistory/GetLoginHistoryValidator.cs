using FluentValidation;

namespace FinanceCore.Application.Features.Auth.Queries.LoginHistory
{
    public class GetLoginHistoryValidator : AbstractValidator<GetLoginHistoryQuery>
    {
        public GetLoginHistoryValidator() { 
            RuleFor(x => x.UserId).NotEmpty();
            RuleFor(x => x.Page).NotEmpty()
                                .GreaterThanOrEqualTo(1);

            RuleFor(x => x.PageSize).NotEmpty()
                                .GreaterThanOrEqualTo(1);
        
        }
    }
}
