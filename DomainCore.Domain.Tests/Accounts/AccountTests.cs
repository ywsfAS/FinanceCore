using FinanceCore.Domain.Accounts;
using FinanceCore.Domain.Common;
using FinanceCore.Domain.Enums;
using FinanceCore.Domain.Exceptions;
using FluentAssertions;
using System;
using Xunit;

namespace FinanceCore.Domain.Tests.Accounts
{
    public class AccountTests
    {
        [Fact]
        public void CreateAccount_ShouldInitializeProperly()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var name = "Savings Account";
            var type = EnAccountType.Savings;
            var currency = EnCurrency.USD;
            decimal initialBalance = 100m;

            // Act
            var account = Account.Create(userId, name, type, currency, initialBalance);

            // Assert
            account.UserId.Should().Be(userId);
            account.Name.Should().Be(name);
            account.Type.Should().Be(type);
            account.Currency.Should().Be(currency);
            account.Balance.Amount.Should().Be(initialBalance);
            account.InitialBalance.Amount.Should().Be(initialBalance);
            account.IsActive.Should().BeTrue();
            account.UpdatedAt.Should().BeNull();
        }

        [Fact]
        public void CreateAccount_WithEmptyName_ShouldThrowException()
        {
            // Arrange
            var userId = Guid.NewGuid();

            // Act
            Action act = () => Account.Create(userId, "", EnAccountType.Checking, EnCurrency.USD);

            // Assert
            act.Should().Throw<InvalidAccountNameException>()
                .WithMessage("Account name cannot be empty");
        }

        [Fact]
        public void ApplyTransaction_Expense_ShouldReduceBalance()
        {
            // Arrange
            var account = Account.Create(Guid.NewGuid(), "My Account", EnAccountType.Checking, EnCurrency.USD, 200m);
            var expense = new Money(50m);

            // Act
            account.ApplyTransaction(expense, EnTransactionType.Expense);

            // Assert
            account.Balance.Amount.Should().Be(150m);
            account.UpdatedAt.Should().NotBeNull();
        }

        [Fact]
        public void ApplyTransaction_Expense_WhenInsufficientBalance_ShouldThrow()
        {
            // Arrange
            var account = Account.Create(Guid.NewGuid(), "My Account", EnAccountType.Checking, EnCurrency.USD, 30m);
            var expense = new Money(50m);

            // Act
            Action act = () => account.ApplyTransaction(expense, EnTransactionType.Expense);

            // Assert
            act.Should().Throw<InsufficientBalanceException>();
        }

        [Fact]
        public void TransferTo_ShouldMoveFundsBetweenAccounts()
        {
            // Arrange
            var source = Account.Create(Guid.NewGuid(), "Source", EnAccountType.Checking, EnCurrency.USD, 500m);
            var target = Account.Create(Guid.NewGuid(), "Target", EnAccountType.Savings, EnCurrency.USD, 100m);
            var amount = new Money(200m);

            // Act
            source.TransferTo(target, amount);

            // Assert
            source.Balance.Amount.Should().Be(300m);
            target.Balance.Amount.Should().Be(300m);
        }

        [Fact]
        public void TransferTo_SameAccount_ShouldThrow()
        {
            // Arrange
            var account = Account.Create(Guid.NewGuid(), "Account", EnAccountType.Checking, EnCurrency.USD, 100m);
            var amount = new Money(50m);

            // Act
            Action act = () => account.TransferTo(account, amount);

            // Assert
            act.Should().Throw<SelfTransferException>();
        }

        [Fact]
        public void Deactivate_ThenApplyTransaction_ShouldThrow()
        {
            // Arrange
            var account = Account.Create(Guid.NewGuid(), "Account", EnAccountType.Checking, EnCurrency.USD, 100m);
            account.Deactivate();
            var money = new Money(10m);

            // Act
            Action act = () => account.ApplyTransaction(money, EnTransactionType.Expense);

            // Assert
            act.Should().Throw<InactiveAccountException>();
        }
    }
}
