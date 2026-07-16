using FinanceCore.Application.DTOs;
using MediatR;

namespace FinanceCore.Application.Features.Categories.Queries.GetCategoryById
{
    public record GetCategoryByIdQuery(Guid UserId ,Guid Id) : IRequest<CategoryDto>;
}
