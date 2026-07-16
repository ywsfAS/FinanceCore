using FinanceCore.Application.DTOs;
using MediatR;

namespace FinanceCore.Application.Features.Categories.Queries.GetCategoriesByUserOptions
{
    public record GetCategoriesByUserOptionsQuery(Guid UserId, int Page = 1 , int PageSize = 10) : IRequest<IEnumerable<CategoryOptionDto>>;
}
