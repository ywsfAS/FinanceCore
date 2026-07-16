using MediatR;

namespace FinanceCore.Application.Features.Categories.Commands.Update
{
    public record UpdateCategoryCommand(
        Guid UserId,
        Guid Id,
        string Name,
        string? Description = null) : IRequest;
}
