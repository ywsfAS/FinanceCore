using FinanceCore.Application.DTOs;
using FinanceCore.Domain.Common;
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
        Money InitialBalance) : IRequest<AccountDto>;

}
