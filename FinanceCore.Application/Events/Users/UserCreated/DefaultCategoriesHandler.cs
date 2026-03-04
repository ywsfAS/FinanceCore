using FinanceCore.Application.Abstractions;
using FinanceCore.Application.Features.Categories.Commands.Create;
using FinanceCore.Domain.Categories;
using FinanceCore.Domain.Enums;
using FinanceCore.Domain.Events.User;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Application.Events.Users.UserCreated
{
    public class DefaultCategoriesHandler : INotificationHandler<UserCreatedEvent>
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMediator _eventBus;
        public DefaultCategoriesHandler(ICategoryRepository categoryRepository, IMediator eventBus)
        {
            _categoryRepository = categoryRepository;
            _eventBus = eventBus;
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
                await _categoryRepository.AddAsync(category, cancellationToken);
                await DomainEventDispatcher.DispatchAsync(_eventBus, category, cancellationToken);
            }
            

        }
    }
}
