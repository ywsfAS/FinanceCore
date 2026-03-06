using FinanceCore.Application.Abstractions;
using FinanceCore.Application.Features.Categories.Commands.Delete;
using FinanceCore.Domain.Exceptions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Application.Tests.Categories
{
    public class DeleteCategoryCommandHandlerTests
    {
        private readonly Mock<ICategoryRepository> _categoryRepositoryMock;
        private readonly DeleteCategoryCommandHandler _handler;
        public DeleteCategoryCommandHandlerTests()
        {
            _categoryRepositoryMock = new Mock<ICategoryRepository>();
            _handler = new DeleteCategoryCommandHandler(_categoryRepositoryMock.Object);
        }
        [Fact]
        public async Task Handle_ShouldDeleteCategory_WhenCategoryExists()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var categoryId = Guid.NewGuid();
            _categoryRepositoryMock.Setup(repo => repo.IsExists(userId, categoryId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(true);
            var command = new DeleteCategoryCommand(userId, categoryId);
            // Act
            await _handler.Handle(command, default);
            // Assert
            _categoryRepositoryMock.Verify(repo => repo.DeleteAsync(categoryId, It.IsAny<CancellationToken>()), Times.Once);
        }
        [Fact]
        public async Task Handle_ShouldNotDeleteCategory_WhenCategoryNotExists()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var categoryId = Guid.NewGuid();
            _categoryRepositoryMock.Setup(repo => repo.IsExists(userId, categoryId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);
            var command = new DeleteCategoryCommand(userId, categoryId);
            // Act
            await Assert.ThrowsAsync<CategoryNotFoundException>(() => _handler.Handle(command, default));
            // Assert
            _categoryRepositoryMock.Verify(repo => repo.DeleteAsync(categoryId, It.IsAny<CancellationToken>()), Times.Never);

        }
    }
}
