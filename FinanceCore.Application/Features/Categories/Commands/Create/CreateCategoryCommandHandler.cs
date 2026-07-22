using FinanceCore.Application.Abstractions;
using FinanceCore.Domain.Categories;
using MediatR;
using FinanceCore.Application.DTOs;
using FinanceCore.Application.Events;
namespace FinanceCore.Application.Features.Categories.Commands.Create
{
    public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommand, CategoryDto>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMediator _eventBus;

        public CreateCategoryCommandHandler(ICategoryRepository categoryRepository , IMediator eventBus)
        {
            _categoryRepository = categoryRepository;
            _eventBus = eventBus;
        }

        public async Task<CategoryDto> Handle(CreateCategoryCommand command, CancellationToken cancellationToken)
        {
            var category =  Category.Create(
                command.UserId,
                command.Name,
                command.Type,
                command.Description);

            await DomainEventDispatcher.DispatchAsync(_eventBus, category,cancellationToken);
            await _categoryRepository.AddAsync(category, cancellationToken);

            return new CategoryDto(category.Id,category.UserId,category.Name,category.Type,category.Description);
        }
    }

}
