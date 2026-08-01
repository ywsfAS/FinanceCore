using FinanceCore.Application.Abstractions;
using FinanceCore.Application.Features.Categories.Commands.Create;
using FinanceCore.Domain.Enums;
using FluentAssertions;
using MediatR;
using Moq;

namespace FinanceCore.Application.Tests.Categories
{
    public class CreateCategoryCommandHandlerTests
    {
        private readonly Mock<ICategoryRepository> _categoryRepositoryMock;
        private readonly CreateCategoryCommandHandler _handler;
        private readonly Mock<IMediator> _eventBusMock;
        public CreateCategoryCommandHandlerTests()
        {
            _categoryRepositoryMock = new();
            _eventBusMock = new();
            _handler = new(_categoryRepositoryMock.Object , _eventBusMock.Object);
        }
        [Fact]
        public async Task Handle_Should_CreateCategory()
        {
            // Arrange
            var command = new CreateCategoryCommand(
                Guid.NewGuid(),
                "Test Category",
                CategoryType.Expense,
                "Test Description"
                );
            // Act
            var result = await _handler.Handle(command, default);
            // Assert
            _categoryRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<Domain.Categories.Category>(), It.IsAny<CancellationToken>()), Times.Once);
            result.Should().NotBeNull();
            result.Name.Should().Be(command.Name);
            result.Type.Should().Be(command.Type);
            result.Description.Should().Be(command.Description);
        }
    }
}
