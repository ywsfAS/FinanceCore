using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Application.Features.Auth.Commands.ResetPassword
{
    public sealed record ResetPasswordCommand(string Token , string NewPassword) : IRequest;
}
