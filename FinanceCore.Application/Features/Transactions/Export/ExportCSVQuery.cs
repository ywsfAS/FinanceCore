using FinanceCore.Application.DTOs;
using FinanceCore.Domain.Enums;
using MediatR;

namespace FinanceCore.Application.Features.Transactions.Export
{
    public record ExportCSVQuery(Guid UserId , Guid? AccountId , Guid?ToAccountId , Guid? CategoryId , DateTime? Start , DateTime? End , EnTransactionType? Type , int Page , int PageSize): IRequest<ExportCSVDto>;
}
