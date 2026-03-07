using FinanceCore.Application.Abstractions;
using FinanceCore.Application.DTOs.Transaction;
using FinanceCore.Application.Features.Transactions.Commands.Transfer;
using FinanceCore.Domain.Accounts;
using FinanceCore.Domain.Users;
using Moq;

namespace FinanceCore.Application.Tests.Tranactions
{

    public class TransferTransactionCommandHandlerTests
    {
        private readonly Mock<ITransactionRepository> _transactionRepositoryMock;
        private readonly Mock<IAccountRepository> _accountRepositoryMock;
        private readonly TransferTransactionCommandHandler _handler;

        public TransferTransactionCommandHandlerTests()
        {
            _transactionRepositoryMock = new Mock<ITransactionRepository>();
            _accountRepositoryMock = new Mock<IAccountRepository>();

            _handler = new TransferTransactionCommandHandler(
                _transactionRepositoryMock.Object,
                _accountRepositoryMock.Object
            );
        }

        [Fact]
        public async Task Handle_ShouldReturnNull_WhenAccountDoesNotExist()
        {
            // Arrange
            var UserId = Guid.NewGuid();
            var accountId = Guid.NewGuid();
            var ToAccountId = Guid.NewGuid();
            var command = new CreateTransferCommand(UserId, accountId, ToAccountId, 100, "Transfer test", "Test notes");

            _accountRepositoryMock
                .Setup(x => x.GetByIdAndUserIdAsync(command.UserId, command.accountId, It.IsAny<CancellationToken>()))
                .ReturnsAsync((Account)null);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.Null(result);
        }
    }
}
