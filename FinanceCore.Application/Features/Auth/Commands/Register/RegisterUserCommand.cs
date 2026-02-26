using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using FinanceCore.Application.DTOs.Auth;
using MediatR;
namespace FinanceCore.Application.Features.Auth.Commands.Register
{
    public record RegisterUserCommand(string Name,string Email ,string Password)
    : IRequest<RegisterDto>;
}
