using FinanceCore.Application.Abstractions;
using FinanceCore.Application.Features.Budgets.Commands.Update;
using FinanceCore.Domain.Budgets;
using FinanceCore.Domain.Common;
using FinanceCore.Domain.Enums;
using FinanceCore.Domain.Exceptions;
using FluentAssertions;
using MediatR;
using Moq;

namespace FinanceCore.Application.Tests.Budgets
{
    public class UpdateBudgetCommandHandlerTests
    {
        private readonly Mock<IBudgetRepository> _budgetRepository;
        private readonly Mock<IMediator> _eventBus;
        private readonly UpdateBudgetCommandHandler _handler;

        public UpdateBudgetCommandHandlerTests()
        {
            _budgetRepository = new();
            _eventBus = new();

            _handler = new UpdateBudgetCommandHandler(
                _budgetRepository.Object,
                _eventBus.Object);
        }

        [Fact]
        public async Task Handle_Should_UpdateBudget_WhenBudgetExists()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var categoryId = Guid.NewGuid();

            var budget = Budget.Create(
                userId,
                categoryId,
                "Old Name",
                new Money(1000m, EnCurrency.USD),
                EnPeriod.Monthly,
                DateTime.UtcNow.AddMonths(1)
            );

            var command = new UpdateBudgetCommand(
                userId,
                budget.Id,
                "New Name",
                new Money(2000m, EnCurrency.USD),
                EnPeriod.Weekly,
                DateTime.UtcNow
            );

            _budgetRepository
                .Setup(repo => repo.GetByIdAndUserIdAsync(
                    userId,
                    budget.Id,
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync(budget);

            // Act
            await _handler.Handle(command, default);

            // Assert
            _budgetRepository.Verify(
                repo => repo.UpdateAsync(
                    budget,
                    It.IsAny<CancellationToken>()),
                Times.Once);

            budget.Name.Should().Be("New Name");
            budget.Amount.Should().Be(new Money(2000m, EnCurrency.USD));
            budget.Period.Should().Be(EnPeriod.Weekly);
        }

        [Fact]
        public async Task Handle_Should_NotUpdateBudget_WhenBudgetNotExists()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var budgetId = Guid.NewGuid();

            _budgetRepository
                .Setup(repo => repo.GetByIdAndUserIdAsync(
                    userId,
                    budgetId,
                    It.IsAny<CancellationToken>()))
                .ReturnsAsync((Budget)null);

            var command = new UpdateBudgetCommand(
                userId,
                budgetId,
                "New Name",
                new Money(2000m, EnCurrency.USD),
                EnPeriod.Weekly,
                DateTime.UtcNow
            );

            // Act
            await Assert.ThrowsAsync<BudgetNotFoundException>(
                () => _handler.Handle(command, default));

            // Assert
            _budgetRepository.Verify(
                repo => repo.UpdateAsync(
                    It.IsAny<Budget>(),
                    It.IsAny<CancellationToken>()),
                Times.Never);
        }
    }
}