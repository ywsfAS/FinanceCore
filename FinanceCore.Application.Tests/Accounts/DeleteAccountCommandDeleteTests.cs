using FinanceCore.Application.Abstractions;
using FinanceCore.Application.Features.Accounts.Commands.Delete;
using FinanceCore.Domain.Exceptions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Application.Tests.Accounts
{
    public class DeleteAccountCommandDeleteTests
    {
        private readonly Mock<IAccountRepository> _accountRepository;
        private readonly DeleteAccountCommandHandler _handler;
        public DeleteAccountCommandDeleteTests()
        {
            _accountRepository = new();
            _handler = new DeleteAccountCommandHandler(_accountRepository.Object);
        }
        [Fact]
        public async Task Handle_Should_Delete_WhenAccountExists()
        {
            // Arrange
           var userId = Guid.NewGuid();
            var accountId = Guid.NewGuid();
                _accountRepository.Setup(repo => repo.IsExists(userId, accountId, It.IsAny<CancellationToken>()))
                    .ReturnsAsync(true) ;
            var command = new DeleteAccountCommand(userId,accountId);
            // Act
            await _handler.Handle(command,default);
            // Assert
            _accountRepository.Verify(repo => repo.DeleteAsync(accountId, It.IsAny<CancellationToken>()), Times.Once);

        }
        [Fact]
        public async Task Handle_Should_NotDelete_WhenAccountDoesNotExist()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var accountId = Guid.NewGuid();

            _accountRepository
                .Setup(repo => repo.IsExists(It.IsAny<Guid>(),It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(false);

            var command = new DeleteAccountCommand(userId, accountId);

            // Act
            await Assert.ThrowsAsync<AccountNotFoundException>(() => _handler.Handle(command, default));

            // Assert
            _accountRepository.Verify(
                r => r.DeleteAsync(It.IsAny<Guid>(), It.IsAny<CancellationToken>()),
                Times.Never);
        }

    }
}
