using FinanceCore.Application.Abstractions;
using FinanceCore.Application.Features.Categories.Commands.Update;
using FinanceCore.Domain.Categories;
using FinanceCore.Domain.Enums;
using FinanceCore.Domain.Exceptions;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Application.Tests.Categories
{
    public class UpdateCateogryCommandHandlerTests
    {
        private readonly Mock<ICategoryRepository> _categoryRepositoryMock;
        private readonly UpdateCategoryCommandHandler _handler;

        public UpdateCateogryCommandHandlerTests()
        {
            _categoryRepositoryMock = new Mock<ICategoryRepository>();
            _handler = new UpdateCategoryCommandHandler(_categoryRepositoryMock.Object);
        }
        [Fact]
        public async Task Handle_Should_UpdateCategory_WhenCategoryExists()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var categoryId = Guid.NewGuid();
            var command = new UpdateCategoryCommand(userId, categoryId, "New Name", "New Description");
            var category = Domain.Categories.Category.Create(
                categoryId,
                userId,
                "Old Name",
                CategoryType.Expense,
                true,
                false,
                "Old Description",
                DateTime.UtcNow
                );
            _categoryRepositoryMock.Setup(repo => repo.GetCategoryByIdAndUserIdAsync(userId, categoryId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(category);
            // Act
            await _handler.Handle(command, default);
            // Assert
            _categoryRepositoryMock.Verify(repo => repo.UpdateAsync(category, It.IsAny<CancellationToken>()), Times.Once);
            category.Name.Should().Be("New Name");
            category.Description.Should().Be("New Description");
        }
        [Fact]
        public async Task Handle_Should_NotUpdateCategory_WhenCategoryNotExists()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var categoryId = Guid.NewGuid();
            var command = new UpdateCategoryCommand(userId, categoryId, "New Name", "New Description");
            _categoryRepositoryMock.Setup(repo => repo.GetCategoryByIdAndUserIdAsync(userId, categoryId, It.IsAny<CancellationToken>()))
                .ReturnsAsync((Category)null);
            // Act
            await Assert.ThrowsAsync<CategoryNotFoundException>(() => _handler.Handle(command, default));
            // Assert
            _categoryRepositoryMock.Verify(repo => repo.UpdateAsync(It.IsAny<Domain.Categories.Category>(), It.IsAny<CancellationToken>()), Times.Never);
        }
    }
}
