using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Application.Features.Accounts.Commands.Update
{
    public record UpdateAccountCommand(
        Guid Id,
        string Name) : IRequest;

}
