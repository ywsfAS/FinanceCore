using FinanceCore.Application.Abstractions;
using FinanceCore.Application.Features.Budgets.Commands.Create;
using FinanceCore.Domain.Budgets;
using FinanceCore.Domain.Categories;
using FinanceCore.Domain.Enums;
using FinanceCore.Domain.Exceptions;
using FluentAssertions;
using Moq;
using System;

namespace FinanceCore.Application.Tests.Budgets
{
    public class CreateBudgetCommandHandlerTests
    {
        private readonly Mock<IBudgetRepository> _budgetRepositoryMock;
        private readonly Mock<ICategoryRepository> _categoryRepositoryMock;
        private readonly CreateBudgetCommandHandler _handler;
        public CreateBudgetCommandHandlerTests()
        {
            _budgetRepositoryMock = new Mock<IBudgetRepository>();
            _categoryRepositoryMock = new Mock<ICategoryRepository>();
            _handler = new CreateBudgetCommandHandler(_budgetRepositoryMock.Object, _categoryRepositoryMock.Object);
        }
        [Fact]
        public async Task Handle_ShouldCreateBudget_WhenCategoryExists()
        {
            // Arrange
            var categoryId = Guid.NewGuid();
            var category = Category.Create(
                categoryId,
                Guid.NewGuid(),
                "Test Category",
                Domain.Enums.CategoryType.Expense,
                true,
                false,
                "Test Description",
                DateTime.UtcNow
                );
            _categoryRepositoryMock.Setup(repo => repo.GetByIdAsync(categoryId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(category);
            var command = new CreateBudgetCommand(
                Guid.NewGuid(),
                categoryId,
                "Test Budget",
                1000,
                EnCurrency.USD,
                BudgetPeriod.Monthly,
                DateTime.UtcNow
                );
            // Act
            var result = await _handler.Handle(command, default);
            // Assert
            _budgetRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<Budget>(), It.IsAny<CancellationToken>()), Times.Once);
            result.Should().NotBeNull();
            result.Name.Should().Be(command.name);
            result.Amount.Should().Be(command.Amount);
            result.Currency.Should().Be(command.Currency);


        }
        [Fact]
        public async Task Handle_ShouldNotCreateBudget_WhenCategoryNotExists()
        {
            // Arrange
            var categoryId = Guid.NewGuid();
            var category = Category.Create(
                categoryId,
                Guid.NewGuid(),
                "Test Category",
                Domain.Enums.CategoryType.Expense,
                true,
                false,
                "Test Description",
                DateTime.UtcNow
                );
            _categoryRepositoryMock.Setup(repo => repo.GetByIdAsync(categoryId, It.IsAny<CancellationToken>()))
                .ReturnsAsync((Category)null);
            var command = new CreateBudgetCommand(
                Guid.NewGuid(),
                categoryId,
                "Test Budget",
                1000,
                EnCurrency.USD,
                BudgetPeriod.Monthly,
                DateTime.UtcNow
                );
            // Act
            await Assert.ThrowsAsync<CategoryNotFoundException>(() => _handler.Handle(command, default));
            // Assert
            _budgetRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<Budget>(), It.IsAny<CancellationToken>()), Times.Never);


        }
        
    }
}
