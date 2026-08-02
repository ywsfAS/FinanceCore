
using FinanceCore.Domain.Enums;
using MediatR;

namespace FinanceCore.Application.Features.Transactions.Commands.TransactionImports
{
    public sealed record Command(Guid UserId , Guid AccountId ,Stream Stream , EnFileType Type) : IRequest;
}
