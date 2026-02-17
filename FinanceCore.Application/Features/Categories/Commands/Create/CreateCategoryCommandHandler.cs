using FinanceCore.Application.Abstractions;
using FinanceCore.Domain.Categories;
using MediatR;
namespace FinanceCore.Application.Features.Categories.Commands.Create
{
    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, Guid>
    {
        private readonly ICategoryRepository _categoryRepository;

        public CreateCategoryCommandHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public async Task<Guid> Handle(CreateCategoryCommand command, CancellationToken cancellationToken)
        {
            var category =  Category.Create(
                command.UserId,
                command.Name,
                command.Type,
                command.Description);

            await _categoryRepository.AddAsync(category, cancellationToken);

            return category.Id;
        }
    }

}
