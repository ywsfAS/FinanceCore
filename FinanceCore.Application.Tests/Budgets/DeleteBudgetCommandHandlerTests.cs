using FinanceCore.Application.Abstractions;
using FinanceCore.Application.Features.Budgets.Commands.Delete;
using FinanceCore.Domain.Exceptions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Application.Tests.Budgets
{
    public class DeleteBudgetCommandHandlerTests
    {
        private readonly Mock<IBudgetRepository> _budgetRepository;
        private readonly DeleteBudgetCommandHandler _handler;
        public DeleteBudgetCommandHandlerTests()
        {
            _budgetRepository = new();
            _handler = new DeleteBudgetCommandHandler(_budgetRepository.Object);
        }
         [Fact]
         public async Task Handle_Should_Delete_WhenBudgetExists()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var budgetId = Guid.NewGuid();
            _budgetRepository.Setup(repo => repo.IsExists(userId, budgetId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);
            var command = new DeleteBudgetCommand(userId, budgetId);
            // Act
            await _handler.Handle(command, default);
            // Assert
            _budgetRepository.Verify(repo => repo.DeleteAsync(budgetId, It.IsAny<CancellationToken>()), Times.Once);


        }
        [Fact]
        public async Task Handle_Should_NotDelete_WhenBudgetNotExists()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var budgetId = Guid.NewGuid();
            _budgetRepository.Setup(repo => repo.IsExists(userId, budgetId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);
            var command = new DeleteBudgetCommand(userId, budgetId);
            // Act
            await Assert.ThrowsAsync<BudgetNotFoundException>(() => _handler.Handle(command, default));
            // Assert
            _budgetRepository.Verify(repo => repo.DeleteAsync(budgetId, It.IsAny<CancellationToken>()), Times.Never);


        }
    }
}
