using FinanceCore.Application.Abstractions;
using MediatR;
using FinanceCore.Domain.Exceptions;
namespace FinanceCore.Application.Features.Categories.Commands.Update
{
    public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommand>
    {
        private readonly ICategoryRepository _categoryRepository;

        public UpdateCategoryCommandHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task Handle(UpdateCategoryCommand command, CancellationToken cancellationToken)
        {
            var category = await _categoryRepository.GetCategoryByIdAndUserIdAsync(command.UserId,command.Id , cancellationToken);

            if (category is null)
                throw new CategoryNotFoundException(command.Id);

            category.Update(command.Name, command.Description);

            await _categoryRepository.UpdateAsync(category, cancellationToken);
        }
    }
}
