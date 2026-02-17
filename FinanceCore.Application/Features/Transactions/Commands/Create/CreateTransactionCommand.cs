using FinanceCore.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Application.Features.Transactions.Commands.Create
{
    public record CreateTransactionCommand(Guid accountId , EnCurrency currency ,Guid CategoryId, decimal amount , EnTransactionType type,string? description = null , string? notes = null) : IRequest<Guid>;
}
