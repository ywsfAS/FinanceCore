using MediatR;

namespace FinanceCore.Application.Features.Categories.Commands.Delete
{
    public record DeleteCategoryCommand(Guid UserId , Guid Id) : IRequest;
}
