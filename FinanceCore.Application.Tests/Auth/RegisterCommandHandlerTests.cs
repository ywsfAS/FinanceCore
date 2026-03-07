using FinanceCore.Application.Abstractions;
using FinanceCore.Application.Features.Auth.Commands.Register;
using FinanceCore.Domain.Common;
using FinanceCore.Domain.Events.User;
using FinanceCore.Domain.Exceptions;
using FinanceCore.Domain.Users;
using FluentAssertions;
using MediatR;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Application.Tests.Auth
{
    public class RegisterCommandHandlerTests
    {
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly Mock<IPasswordHasher> _hasherMock;
        private readonly Mock<IMediator> _mediatorMock;
        private readonly RegisterUserCommandHandler _handler;
        public RegisterCommandHandlerTests()
        {
            _userRepositoryMock = new();
            _hasherMock = new();
            _mediatorMock = new();
            _handler = new RegisterUserCommandHandler(_userRepositoryMock.Object, _hasherMock.Object, _mediatorMock.Object);
        }
        [Fact]
        public async Task Handle_Should_CreateUser_And_Save()
        {
            // Arrange
            var email = "Test@gmail.com";
            var name = "Test User";
            var password = "password";
            var command = new RegisterUserCommand(name, email, password);
            _hasherMock.Setup(hasher => hasher.Hash(password)).Returns("hashed_password");
            _userRepositoryMock.Setup(repo => repo.GetByEmailAsync(email, It.IsAny<CancellationToken>())).ReturnsAsync((User)null);
            // Act
            var result = await _handler.Handle(command, default);
            // Assert
            _userRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<User>(), It.IsAny<CancellationToken>()), Times.Once);
            result.Should().NotBeNull();
            result.Name.Should().Be(command.Name);
            result.Email.Should().Be(command.Email);
            _hasherMock.Verify(hasher => hasher.Hash(password), Times.Once);
            _mediatorMock.Verify(mediator => mediator.Publish(It.IsAny<UserCreatedEvent>(), It.IsAny<CancellationToken>()), Times.Never);

        }
        [Fact]
        public async Task Handle_Should_NotCreateUser_WhenEmailFound()
        {
            // Arrange
            var email = "Test@gmail.com";
            var name = "Test User";
            var password = "password";
            var command = new RegisterUserCommand(name, email, password);
            var user = User.Create(name, new Email(email), password);
            _hasherMock.Setup(hasher => hasher.Hash(password)).Returns("hashed_password");
            _userRepositoryMock.Setup(repo => repo.GetByEmailAsync(email, It.IsAny<CancellationToken>())).ReturnsAsync(user);
            // Act
            await Assert.ThrowsAsync<DuplicateEmailException>(() => _handler.Handle(command, default));
            // Assert
            _userRepositoryMock.Verify(repo => repo.AddAsync(It.IsAny<User>(), It.IsAny<CancellationToken>()), Times.Never);
            _hasherMock.Verify(hasher => hasher.Hash(password), Times.Never);
            _mediatorMock.Verify(mediator => mediator.Publish(It.IsAny<UserCreatedEvent>(), It.IsAny<CancellationToken>()), Times.Never);

        }

    }
}
