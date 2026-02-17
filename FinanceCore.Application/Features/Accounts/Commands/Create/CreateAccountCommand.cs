using FinanceCore.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Application.Features.Accounts.Commands.Create
{
    public record CreateAccountCommand(
        Guid UserId,
        string Name,
        EnAccountType Type,
        EnCurrency Currency,
        decimal InitialBalance = 0) : IRequest<Guid>;

}
