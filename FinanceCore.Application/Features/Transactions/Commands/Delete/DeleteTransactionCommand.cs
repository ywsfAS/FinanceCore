using System;
using MediatR;
namespace FinanceCore.Application.Features.Transactions.Commands.Delete
{
    public record DeleteTransactionCommand(Guid UserId , Guid Id) : IRequest;

}
