using FinanceCore.Domain.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Application.Features.PasswordReset
{
    public sealed record ForgotPasswordCommand(Email Email) : IRequest;
    
    
}
