using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using MediatR;
namespace FinanceCore.Application.Features.Auth.Commands.Register
{
    public record RegisterUserCommand(string Name,string Email ,string Password)
    : IRequest<Guid>;
}
