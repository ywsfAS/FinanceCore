using FinanceCore.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Application.Features.Users.Command.Update
{
    public record UpdateUserCommand(
    Guid Id,
    string Name,
    EnCurrency DefaultCurrency,
    string? TimeZone = null) : IRequest;
}
