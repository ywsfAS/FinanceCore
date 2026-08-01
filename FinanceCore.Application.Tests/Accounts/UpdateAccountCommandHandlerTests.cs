using FinanceCore.Application.Abstractions;
using FinanceCore.Application.Features.Accounts.Commands.Update;
using FinanceCore.Domain.Accounts;
using FinanceCore.Domain.Common;
using FinanceCore.Domain.Enums;
using FinanceCore.Domain.Exceptions;
using FluentAssertions;
using MediatR;
using Moq;

namespace FinanceCore.Application.Tests.Accounts
{
    public class UpdateAccountCommandHandlerTests
    {
        private readonly Mock<IAccountRepository> _accountRepository;
        private readonly Mock<IMediator> _eventBusMock;
        private readonly UpdateAccountCommandHandler _handler;
        public UpdateAccountCommandHandlerTests()
        {
            _accountRepository = new();
            _eventBusMock = new();
            _handler = new UpdateAccountCommandHandler(_accountRepository.Object , _eventBusMock.Object);
        }
        [Fact]
        public async Task Handle_Should_UpdateAccount_WhenAccountExists()
        {
            // Arrange
            var userId = Guid.NewGuid();

            var account = Account.Create(
                userId,
                "Old Name",
                EnAccountType.Cash,
                new Money(1000m , EnCurrency.USD)
                );

            _accountRepository
                .Setup(r => r.GetByIdAndUserIdAsync(userId, account.Id, It.IsAny<CancellationToken>()))
                .ReturnsAsync(account);

            var command = new UpdateAccountCommand(userId, account.Id, "New Name",EnAccountType.Cash);

            // Act
            await _handler.Handle(command,default);
            // Assert
            _accountRepository.Verify(
                repo => repo.UpdateAsync(account,null,It.IsAny<CancellationToken>()),
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
                userId,
                "Old Name",
                EnAccountType.Checking,
                new Money(1000m , EnCurrency.USD));

            _accountRepository
                .Setup(r => r.GetByIdAndUserIdAsync(userId, accountId, It.IsAny<CancellationToken>()))
                .ReturnsAsync((Account)null);

            var command = new UpdateAccountCommand(userId, accountId, "New Name",EnAccountType.Savings);

            // Act
            await Assert.ThrowsAsync<AccountNotFoundException>(() => _handler.Handle(command, default));
            // Assert
            _accountRepository.Verify(
                repo => repo.UpdateAsync(account,null,It.IsAny<CancellationToken>()),
                Times.Never);

        }
    }
}
