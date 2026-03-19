using FinanceCore.Application.DTOs;
using FinanceCore.Domain.Enums;
using MediatR;

namespace FinanceCore.Application.Features.Categories.Commands.Create
{
    public record CreateCategoryCommand(
        Guid UserId,
        string Name,
        CategoryType Type,
        string? Description = null) : IRequest<CategoryDto>;
}
