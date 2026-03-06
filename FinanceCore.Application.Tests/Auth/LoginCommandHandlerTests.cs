using FinanceCore.Application.Abstractions;
using FinanceCore.Application.Features.Auth.Commands.Login;
using FinanceCore.Domain.Common;
using FinanceCore.Domain.Exceptions;
using FinanceCore.Domain.Users;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Application.Tests.Auth
{
    public class LoginCommandHandlerTests
    {
        private readonly LoginUserCommandHandler _handler;
        private readonly Mock<IPasswordHasher> _hasherMock;
        private readonly Mock<IJwtTokenGenerator> _JwtGeneratorMock;
        private readonly Mock<IUserRepository> _UserRepositoryMock;
        public LoginCommandHandlerTests()
        {
            _UserRepositoryMock = new();
            _JwtGeneratorMock = new();
            _hasherMock = new();
            _handler = new LoginUserCommandHandler(_UserRepositoryMock.Object,_hasherMock.Object,_JwtGeneratorMock.Object);
        }
        [Fact]
        public async Task Handle_Should_ReturnToken_WhenCredentialsAreValid()
        {
            // Arrange
            var email = new Email("testuser@gmail.com");
            var password = "password";
            var command = new LoginUserCommand(email.Address,password);
            var user = User.Create("UserTest",email,password);
            _UserRepositoryMock.Setup(repo => repo.GetByEmailAsync(email.Address, It.IsAny<CancellationToken>()))
                .ReturnsAsync(user);
             _hasherMock.Setup(hasher => hasher.Verify(password, user.PasswordHash)).Returns(true);
             _JwtGeneratorMock.Setup(gen => gen.GenerateToken(user)).Returns("test_token");
            // Act
            var result = await _handler.Handle(command, default);
            // Assert
            _UserRepositoryMock.Verify(repo => repo.GetByEmailAsync(email.Address, It.IsAny<CancellationToken>()), Times.Once);
            _hasherMock.Verify(hasher => hasher.Verify(password, user.PasswordHash), Times.Once);
            _JwtGeneratorMock.Verify(gen => gen.GenerateToken(user), Times.Once);
            result.Should().NotBeNull();
            result.Token.Should().Be("test_token");


        }
        [Fact]
        public async Task Handle_Should_FailedResult_WhenEmailNotFound()
        {
            // Arrange
            var email = new Email("testuser@gmail.com");
            var password = "password";
            var command = new LoginUserCommand(email.Address, password);
            var user = User.Create("UserTest", email, password);
            _UserRepositoryMock.Setup(repo => repo.GetByEmailAsync(email.Address, It.IsAny<CancellationToken>()))
                .ReturnsAsync((User)null);
            _hasherMock.Setup(hasher => hasher.Verify(password, user.PasswordHash)).Returns(true);
            _JwtGeneratorMock.Setup(gen => gen.GenerateToken(user)).Returns("test_token");
            // Act
            await Assert.ThrowsAsync<InvalidCredentialsException>(() => _handler.Handle(command, default));
            // Assert
            _UserRepositoryMock.Verify(repo => repo.GetByEmailAsync(email.Address, It.IsAny<CancellationToken>()), Times.Once);
            _hasherMock.Verify(hasher => hasher.Verify(password, user.PasswordHash), Times.Never);
            _JwtGeneratorMock.Verify(gen => gen.GenerateToken(user), Times.Never);
        }
        [Fact]
        public async Task Handle_Should_FailedResult_WhenPaaswordWrong()
        {
            // Arrange
            var email = new Email("testuser@gmail.com");
            var password = "password";
            var command = new LoginUserCommand(email.Address, password);
            var user = User.Create("UserTest", email, password);
            _UserRepositoryMock.Setup(repo => repo.GetByEmailAsync(email.Address, It.IsAny<CancellationToken>()))
                .ReturnsAsync(user);
            _hasherMock.Setup(hasher => hasher.Verify(password, user.PasswordHash)).Returns(false);
            _JwtGeneratorMock.Setup(gen => gen.GenerateToken(user)).Returns("test_token");
            // Act
            await Assert.ThrowsAsync<InvalidCredentialsException>(() => _handler.Handle(command, default));
            // Assert
            _UserRepositoryMock.Verify(repo => repo.GetByEmailAsync(email.Address, It.IsAny<CancellationToken>()), Times.Once);
            _hasherMock.Verify(hasher => hasher.Verify(password, user.PasswordHash), Times.Once);
            _JwtGeneratorMock.Verify(gen => gen.GenerateToken(user), Times.Never);
        }
    }
}
