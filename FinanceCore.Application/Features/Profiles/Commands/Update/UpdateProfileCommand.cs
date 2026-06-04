using FinanceCore.Domain.Enums;
using FinanceCore.Domain.Profile;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Application.Features.Profiles.Commands.Update
{
    public record UpdateProfileCommand(Guid UserId, string FirstName, string LastName, string Bio,EnCurrency Currency) : IRequest;
    
    
}
