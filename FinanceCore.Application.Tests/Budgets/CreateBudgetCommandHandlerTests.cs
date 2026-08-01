using FinanceCore.Application.Abstractions;
using FinanceCore.Application.Features.Budgets.Commands.Create;
using FinanceCore.Domain.Budgets;
using FinanceCore.Domain.Categories;
using FinanceCore.Domain.Common;
using FinanceCore.Domain.Enums;
using FinanceCore.Domain.Exceptions;
using FluentAssertions;
using MediatR;
using Moq;

namespace FinanceCore.Application.Tests.Budgets
{
    public class CreateBudgetCommandHandlerTests
    {
        private readonly Mock<IBudgetRepository> _budgetRepositoryMock;
        private readonly Mock<ICategoryRepository> _categoryRepositoryMock;
        private readonly Mock<IMediator> _eventBusMock;
        private readonly CreateBudgetCommandHandler _handler;
        public CreateBudgetCommandHandlerTests()
        {
            _budgetRepositoryMock = new Mock<IBudgetRepository>();
            _categoryRepositoryMock = new Mock<ICategoryRepository>();
            _eventBusMock = new Mock<IMediator>();
            _handler = new CreateBudgetCommandHandler(_budgetRepositoryMock.Object,  _eventBusMock.Object,_categoryRepositoryMock.Object );
        }
        [Fact]
        public async Task Handle_ShouldCreateBudget_WhenCategoryExists()
        {
            // Arrange
            var categoryId = Guid.NewGuid();
            var userId = Guid.NewGuid();    
            var category = Category.Create(
                categoryId,
                userId,
                "Test Category",
                CategoryType.Expense,
                true,
                false,
                "Test Description",
                DateTime.UtcNow
                );
            _categoryRepositoryMock.Setup(repo => repo.GetCategoryByIdAndUserIdAsync(userId,categoryId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(category);
            var command = new CreateBudgetCommand(
                userId,
                categoryId,
                "Test Budget",
                new Money(1000m , EnCurrency.USD),
                EnPeriod.Monthly,
                DateTime.UtcNow
                );
            // Act
            var result = await _handler.Handle(command, default);
            var resultMoney = new Money(result.Amount,result.Currency);
            // Assert
            _budgetRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<Budget>(), It.IsAny<CancellationToken>()), Times.Once);
            result.Should().NotBeNull();
            result.Name.Should().Be(command.name);
            resultMoney.Should().Be(command.Amount);

        }
        [Fact]
        public async Task Handle_ShouldNotCreateBudget_WhenCategoryNotExists()
        {
            // Arrange
            var categoryId = Guid.NewGuid();
            var userId = Guid.NewGuid();
            var category = Category.Create(
                categoryId,
                userId,
                "Test Category",
                CategoryType.Expense,
                true,
                false,
                "Test Description",
                DateTime.UtcNow
                );
            _categoryRepositoryMock.Setup(repo => repo.GetCategoryByIdAndUserIdAsync(userId,categoryId, It.IsAny<CancellationToken>()))
                .ReturnsAsync((Category)null);
            var command = new CreateBudgetCommand(
                Guid.NewGuid(),
                categoryId,
                "Test Budget",
                new Money(1000m , EnCurrency.USD),
                EnPeriod.Monthly,
                DateTime.UtcNow
                );
            // Act
            await Assert.ThrowsAsync<CategoryNotFoundException>(() => _handler.Handle(command, default));
            // Assert
            _budgetRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<Budget>(), It.IsAny<CancellationToken>()), Times.Never);


        }
        
    }
}
