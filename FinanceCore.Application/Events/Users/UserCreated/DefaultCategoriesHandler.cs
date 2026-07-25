using FinanceCore.Application.Abstractions;
using FinanceCore.Application.Features.Categories.Commands.Create;
using FinanceCore.Domain.Categories;
using FinanceCore.Domain.Enums;
using FinanceCore.Domain.Events.User;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FinanceCore.Application.Events.Users.UserCreated
{
    public class DefaultCategoriesHandler : INotificationHandler<UserCreatedEvent>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMediator _eventBus;
        private readonly ILogger<DefaultCategoriesHandler> _logger;
        public DefaultCategoriesHandler(ICategoryRepository categoryRepository, IMediator eventBus, ILogger<DefaultCategoriesHandler> logger)
        {
            _categoryRepository = categoryRepository;
            _eventBus = eventBus;
            _logger = logger;
        }
        public async Task Handle(UserCreatedEvent notification, CancellationToken cancellationToken)
        {
            var list = new List<CreateCategoryCommand>
            {
                new CreateCategoryCommand(
                    notification.UserId,
                    "Salary",
                    CategoryType.Income,
                    "Default category for salary income"
                ),
                new CreateCategoryCommand(
                    notification.UserId,
                    "Food",
                    CategoryType.Expense,
                    "Default category for food expenses"
                ),
                new CreateCategoryCommand(
                    notification.UserId,
                    "Transportation",
                    CategoryType.Expense,
                    "Default category for transportation expenses"
                ),
                 new CreateCategoryCommand(
                    notification.UserId,
                    "Entertainment",
                    CategoryType.Expense,
                    "Default category for entertainment expenses"
                )
            };

            foreach (var command in list)
            {
            
                var category = Category.Create(
                command.UserId,
                command.Name,
                command.Type,
                command.Description);

                try
                {
                    await _categoryRepository.AddAsync(category, cancellationToken);
                    await DomainEventDispatcher.DispatchAsync(_eventBus, category, cancellationToken);
                }
                catch(Exception ex)
                {
                    _logger.LogCritical(ex, "Failed to create default categories for {email}", notification.Email);
                    throw;
                }

            }
            

        }
    }
}
