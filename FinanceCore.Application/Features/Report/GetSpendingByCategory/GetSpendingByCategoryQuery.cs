using FinanceCore.Application.DTOs;
using MediatR;

namespace FinanceCore.Application.Features.Report.GetSpendingByCategory
{
    public record GetSpendingByCategoryQuery(Guid UserId , Guid? AccountId , int Year , int Month , int Page , int PageSize ) : IRequest<IEnumerable<SpendingByCategoryDto>>;
}
