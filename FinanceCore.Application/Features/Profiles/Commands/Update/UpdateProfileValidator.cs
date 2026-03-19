using FluentValidation;
using System;

namespace FinanceCore.Application.Features.Profiles.Commands.Update
{
     public class UpdateProfileValidator : AbstractValidator<UpdateProfileCommand>
    {
        public UpdateProfileValidator() { 
        }
    }
}
