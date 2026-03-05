using FinanceCore.Application.Abstractions;
using FinanceCore.Application.Features.Accounts.Commands.Create;
using FinanceCore.Domain.Accounts;
using FinanceCore.Domain.Enums;
using FluentAssertions;
using MediatR;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Application.Tests.Accounts
{
    public class CreateAccountCommandHandlerTests
    {
        private readonly Mock<IAccountRepository> _accountRepositoryMock;
        private readonly Mock<IMediator> _mediatorMock;
        private readonly CreateAccountCommandHandler _handler;

        
        public CreateAccountCommandHandlerTests()
        {
            _accountRepositoryMock = new Mock<IAccountRepository>();
            _mediatorMock = new Mock<IMediator>();
            _handler = new CreateAccountCommandHandler(_accountRepositoryMock.Object, _mediatorMock.Object);
        }
        [Fact]
        public async Task Handle_Should_CreateAccount_And_Save()
        {
            // Arrange
            var command = new CreateAccountCommand(
                Guid.NewGuid(),
                "Test Account",
                EnAccountType.Checking,
                EnCurrency.USD,
                1000
                );
            // Act 
            var result = await _handler.Handle(command,default);

            // Assert
            _accountRepositoryMock.Verify(
                repo => repo.AddAsync(It.IsAny<Account>(), It.IsAny<CancellationToken>()), Times.Once
                );
            
            result.Should().NotBeNull();
            result.Name.Should().Be(command.Name);
            result.Balance.Should().Be(command.InitialBalance);
            result.Currency.Should().Be(command.Currency);

        }
        [Fact]
        public async Task Handle_Should_Dispatch_AccountCreatedEvent()
        {
            // Arange
            var command = new CreateAccountCommand(
                Guid.NewGuid(),
                "Test Account",
                EnAccountType.Checking,
                EnCurrency.USD,
                1000
                );
            // Act 
            var result = await _handler.Handle(command, default);
            // Assert
            _mediatorMock.Verify(med => med.Publish(It.IsAny<INotification>(),It.IsAny<CancellationToken>()),Times.AtLeastOnce);
        }
    }
}
