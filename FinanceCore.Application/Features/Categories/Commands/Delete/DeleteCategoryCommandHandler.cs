using FinanceCore.Application.Abstractions;
using MediatR;
using FinanceCore.Domain.Exceptions;

namespace FinanceCore.Application.Features.Categories.Commands.Delete
{
    public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommand>
    {
        private readonly ICategoryRepository _categoryRepository;

        public DeleteCategoryCommandHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task Handle(DeleteCategoryCommand command, CancellationToken cancellationToken)
        {
            var result = await _categoryRepository.IsExistsAsync(command.UserId,command.Id);

            if (!result)
                throw new CategoryNotFoundException(command.Id);

            await _categoryRepository.DeleteAsync(command.UserId,command.Id,cancellationToken);
        }
    }


}
