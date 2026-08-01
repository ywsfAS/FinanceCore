using FinanceCore.Application.Abstractions;
using FinanceCore.Application.Features.Transactions.Commands.Transactions;
using FinanceCore.Domain.Accounts;
using FinanceCore.Domain.Enums;
using MediatR;
using Moq;

namespace FinanceCore.Application.Tests.Tranactions
{

    public class TransferTransactionCommandHandlerTests
    {
        private readonly Mock<ITransactionRepository> _transactionRepositoryMock;
        private readonly Mock<IAccountRepository> _accountRepositoryMock;
        private readonly Mock<IMediator> _eventBusMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<ICategoryRepository> _categoryRepositoryMock;

        private readonly TransactionHandler _handler;

        public TransferTransactionCommandHandlerTests()
        {
            _transactionRepositoryMock = new Mock<ITransactionRepository>();
            _accountRepositoryMock = new Mock<IAccountRepository>();
            _eventBusMock = new Mock<IMediator>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _categoryRepositoryMock = new Mock<ICategoryRepository>();

            _handler = new TransactionHandler(
                _transactionRepositoryMock.Object,
                _accountRepositoryMock.Object,
                _categoryRepositoryMock.Object,
                _eventBusMock.Object,
                _unitOfWorkMock.Object
            );
        }

        [Fact]
        public async Task Handle_ShouldReturnNull_WhenAccountDoesNotExist()
        {
        }
    }
}
