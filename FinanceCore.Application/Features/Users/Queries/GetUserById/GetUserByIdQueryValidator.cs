using System;
using System.Collections.Generic;
using FluentValidation;
namespace FinanceCore.Application.Features.Users.Queries.GetUserById
{
    public class GetUserByIdQueryValidator : AbstractValidator<GetUserByIdQuery>
    {
        public GetUserByIdQueryValidator() {
            RuleFor(x => x.Id).NotEmpty();
        
        }

    }

}
