
using FinanceCore.Domain.Enums;
using MediatR;

namespace FinanceCore.Application.Features.Transactions.Commands.TransactionImports
{
    public sealed record ImportTransactionCommand(Guid UserId , Guid AccountId ,Stream Stream , EnFileType Type , string FileName) : IRequest;
}
