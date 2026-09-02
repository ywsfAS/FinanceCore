using FinanceCore.Domain.Enums;
using MediatR;

namespace FinanceCore.Application.Features.Profiles.Commands.Update
{
    public record UpdateProfileCommand(Guid UserId, string? FirstName, string? LastName, string? Bio,EnCurrency? Currency) : IRequest;
    
    
}
