using MediatR;

namespace FinanceCore.Application.Features.Profiles.Commands.Delete
{
     public record DeleteProfileCommand(Guid id) : IRequest;
    
    
}
