using FinanceCore.Application.DTOs;
using FinanceCore.Domain.Enums;
using MediatR;

namespace FinanceCore.Application.Features.Profiles.Commands.Create
{
    public record CreateProfileCommand(Guid UserId, string FirstName, string LastName, string Bio ,EnCurrency Curreny) : IRequest<ProfileDto>;


    
}
