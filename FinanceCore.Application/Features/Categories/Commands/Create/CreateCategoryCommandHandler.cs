using FinanceCore.Application.Abstractions;
using FinanceCore.Domain.Categories;
using MediatR;
using FinanceCore.Application.DTOs;
namespace FinanceCore.Application.Features.Categories.Commands.Create
{
    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, CategoryDto>
    {
        private readonly ICategoryRepository _categoryRepository;

        public CreateCategoryCommandHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<CategoryDto> Handle(CreateCategoryCommand command, CancellationToken cancellationToken)
        {
            var category =  Category.Create(
                command.UserId,
                command.Name,
                command.Type,
                command.Description);

            await _categoryRepository.AddAsync(category, cancellationToken);

            return new CategoryDto(category.Id,category.UserId,category.Name,category.Type,category.Description);
        }
    }

}
