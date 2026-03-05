using FinanceCore.Application.Abstractions;
using FinanceCore.Application.Features.Accounts.Commands.Delete;
using FinanceCore.Application.Features.Accounts.Commands.Update;
using FinanceCore.Domain.Accounts;
using FinanceCore.Domain.Enums;
using FinanceCore.Domain.Exceptions;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Application.Tests.Accounts
{
    public class UpdateAccountCommandHandlerTests
    {
        private readonly Mock<IAccountRepository> _accountRepository;
        private readonly UpdateAccountCommandHandler _handler;
        public UpdateAccountCommandHandlerTests()
        {
            _accountRepository = new();
            _handler = new UpdateAccountCommandHandler(_accountRepository.Object);
        }
        [Fact]
        public async Task Handle_Should_UpdateAccount_WhenAccountExists()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var accountId = Guid.NewGuid();

            var account = Account.Create(
                accountId,
                userId,
                "Old Name",
                EnAccountType.Checking,
                EnCurrency.USD,
                0,
                1000,
                true,
                DateTime.UtcNow);

            _accountRepository
                .Setup(r => r.GetByIdAndUserIdAsync(userId, accountId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(account);

            var command = new UpdateAccountCommand(userId, accountId, "New Name");

            // Act
            await _handler.Handle(command,default);
            // Assert
            _accountRepository.Verify(
                repo => repo.UpdateAsync(account, It.IsAny<CancellationToken>()),
                Times.Once);

            account.Name.Should().Be("New Name");
        }
        [Fact]
        public async Task Handle_Should_NotUpdateAccount_WhenAccountNotExists()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var accountId = Guid.NewGuid();

            var account = Account.Create(
                accountId,
                userId,
                "Old Name",
                EnAccountType.Checking,
                EnCurrency.USD,
                0,
                1000,
                true,
                DateTime.UtcNow);

            _accountRepository
                .Setup(r => r.GetByIdAndUserIdAsync(userId, accountId, It.IsAny<CancellationToken>()))
                .ReturnsAsync(account);

            var command = new UpdateAccountCommand(userId, accountId, "New Name");

            // Act
            await Assert.ThrowsAsync<AccountNotFoundException>(() => _handler.Handle(command, default));
            // Assert
            _accountRepository.Verify(
                repo => repo.UpdateAsync(account, It.IsAny<CancellationToken>()),
                Times.Never);

            account.Name.Should().Be("New Name");
        }
    }
}
