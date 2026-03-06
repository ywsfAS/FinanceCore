using FinanceCore.Application.Abstractions;
using FinanceCore.Application.Features.Budgets.Commands.Update;
using FinanceCore.Domain.Budgets;
using FinanceCore.Domain.Enums;
using FinanceCore.Domain.Exceptions;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Application.Tests.Budgets
{
    public class UpdateBudgetCommandHandlerTests
    {
        private readonly Mock<IBudgetRepository> _budgetRepositoryMock;
        private readonly UpdateBudgetCommandHandler _handler;
        public UpdateBudgetCommandHandlerTests()
        {
            _budgetRepositoryMock = new();
            _handler = new UpdateBudgetCommandHandler(_budgetRepositoryMock.Object);

        }
        [Fact]
        public async Task Handle_Should_UpdateBudget_WhenBudgetExists()
        {
            // Arrange 
            var userId = Guid.NewGuid();
            var budgetId = Guid.NewGuid();
            var categoryId = Guid.NewGuid();
            var budget = Budget.Create(
               budgetId,
               userId,
               categoryId,
               "Old Name",
               EnCurrency.USD,
               1000,
               BudgetPeriod.Monthly,
               DateTime.UtcNow,
               DateTime.UtcNow.AddMonths(1),
               DateTime.UtcNow
               );
            var command = new UpdateBudgetCommand(userId, budgetId, "New Name", 2000, EnCurrency.USD, BudgetPeriod.Weekly, DateTime.UtcNow);
            _budgetRepositoryMock.Setup(repo => repo.GetByIdAndUserIdAsync(userId, budgetId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(budget);
            // Act
            await _handler.Handle(command,default);
            // Assert
            _budgetRepositoryMock.Verify(repo => repo.UpdateAsync(budget, It.IsAny<CancellationToken>()), Times.Once);
            budget.Name.Should().Be("New Name");
            budget.Amount.Amount.Should().Be(2000);
            budget.Currency.Should().Be(EnCurrency.USD);
            budget.Period.Should().Be(BudgetPeriod.Weekly);

        }
        [Fact]
        public async Task Handle_Should_NotUpdateBudget_WhenBudgetNotExists()
        {
            // Arrange 
            var userId = Guid.NewGuid();
            var budgetId = Guid.NewGuid();
            var categoryId = Guid.NewGuid();
            var budget = Budget.Create(
               budgetId,
               userId,
               categoryId,
               "Old Name",
               EnCurrency.USD,
               1000,
               BudgetPeriod.Monthly,
               DateTime.UtcNow,
               DateTime.UtcNow.AddMonths(1),
               DateTime.UtcNow
               );
            var command = new UpdateBudgetCommand(userId, budgetId, "New Name", 2000, EnCurrency.USD, BudgetPeriod.Weekly, DateTime.UtcNow);
            _budgetRepositoryMock.Setup(repo => repo.GetByIdAndUserIdAsync(userId, budgetId, It.IsAny<CancellationToken>()))
                .ReturnsAsync((Budget)null);
            // Act
            await Assert.ThrowsAsync<BudgetNotFoundException>(() => _handler.Handle(command, default));
            // Assert
            _budgetRepositoryMock.Verify(repo => repo.UpdateAsync(budget, It.IsAny<CancellationToken>()), Times.Never);
            budget.Name.Should().Be("Old Name");
            budget.Amount.Amount.Should().Be(1000);
            budget.Currency.Should().Be(EnCurrency.USD);
            budget.Period.Should().Be(BudgetPeriod.Monthly);

        }

    }
}
