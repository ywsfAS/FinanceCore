using FinanceCore.Domain.Common;
using FinanceCore.Domain.Enums;
using FinanceCore.Domain.Exceptions;
using FinanceCore.Domain.Transactions;
using FluentAssertions;
using System;
using Xunit;

namespace FinanceCore.Domain.Tests.Transactions
{
    public class TransactionTests
    {
        [Fact]
        public void CreateTransaction_ShouldInitializeProperly()
        {
            // Arrange
            var accountId = Guid.NewGuid();
            var categoryId = Guid.NewGuid();
            Money amount = new Money(100m,EnCurrency.USD);
            var type = EnTransactionType.Expense;
            var date = DateTime.UtcNow;
            var description = "Groceries";

            // Act
            var transaction = Transaction.Create(accountId, null, amount, categoryId, type, date, description);

            // Assert
            transaction.AccountId.Should().Be(accountId);
            transaction.CategoryId.Should().Be(categoryId);
            transaction.Amount.Should().Be(amount);
            transaction.Type.Should().Be(type);
            transaction.Date.Should().Be(date);
            transaction.Description.Should().Be(description);
        }

        [Fact]
        public void CreateTransaction_WithInvalidAccount_ShouldThrow()
        {
            var categoryId = Guid.NewGuid();
            Action act = () => Transaction.Create(Guid.Empty, null, new Money(100,EnCurrency.USD), categoryId, EnTransactionType.Expense);
            act.Should().Throw<ArgumentException>().WithMessage("*Account ID cannot be empty*");
        }

        [Fact]
        public void CreateTransaction_WithNegativeAmount_ShouldThrow()
        {
            var accountId = Guid.NewGuid();
            var categoryId = Guid.NewGuid();
            Action act = () => Transaction.Create(accountId, null, new Money(-10,EnCurrency.USD), categoryId, EnTransactionType.Expense);
            act.Should().Throw<MoneyIsNegativeException>();
        }

        [Fact]
        public void CreateTransferTransaction_WithoutToAccount_ShouldThrow()
        {
            var accountId = Guid.NewGuid();
            var categoryId = Guid.NewGuid();
            Action act = () => Transaction.Create(accountId, null, new Money(500,EnCurrency.USD), categoryId, EnTransactionType.Transfer);
            act.Should().Throw<ArgumentException>().WithMessage("*ToAccountId is required*");
        }

        [Fact]
        public void CreateTransferTransaction_ToSameAccount_ShouldThrow()
        {
            var accountId = Guid.NewGuid();
            var categoryId = Guid.NewGuid();
            Action act = () => Transaction.Create(accountId, accountId, new Money(50, EnCurrency.USD), categoryId, EnTransactionType.Transfer);
            act.Should().Throw<SelfTransferException>();
        }

        [Fact]
        public void CreateTransaction_WithFutureDate_ShouldThrow()
        {
            var accountId = Guid.NewGuid();
            var categoryId = Guid.NewGuid();
            var futureDate = DateTime.UtcNow.AddDays(2);
            Action act = () => Transaction.Create(accountId, null, new Money(50, EnCurrency.USD), categoryId, EnTransactionType.Expense, futureDate);
            act.Should().Throw<FutureTransactionDateException>();
        }

        [Fact]
        public void UpdateTransaction_ShouldUpdateFields()
        {
            var accountId = Guid.NewGuid();
            var categoryId = Guid.NewGuid();
            var transaction = Transaction.Create(accountId, null, new Money(100, EnCurrency.USD), categoryId, EnTransactionType.Expense);

            var newAmount = new Money(150m,EnCurrency.USD);
            var newCategory = Guid.NewGuid();
            var newDate = DateTime.UtcNow;
            var newDescription = "Updated description";

            transaction.Update(newAmount, newCategory, newDate, newDescription);

            transaction.Amount.Should().Be(newAmount);
            transaction.CategoryId.Should().Be(newCategory);
            transaction.Date.Should().Be(newDate);
            transaction.Description.Should().Be(newDescription);
            transaction.UpdatedAt.Should().NotBeNull();
        }

        [Fact]
        public void UpdateTransaction_WithInvalidAmount_ShouldThrow()
        {
            var transaction = Transaction.Create(Guid.NewGuid(), null, new Money(100, EnCurrency.USD), Guid.NewGuid(), EnTransactionType.Expense);
            Action act = () => transaction.Update(amount: new Money(-50, EnCurrency.USD));
            act.Should().Throw<MoneyIsNegativeException>();
        }

        [Fact]
        public void UpdateTransaction_WithEmptyCategory_ShouldThrow()
        {
            var transaction = Transaction.Create(Guid.NewGuid(), null, new Money(100, EnCurrency.USD), Guid.NewGuid(), EnTransactionType.Expense);
            Action act = () => transaction.Update(categoryId: Guid.Empty);
            act.Should().Throw<InvalidTransactionCategoryException>();
        }

        [Fact]
        public void UpdateTransaction_WithFutureDate_ShouldThrow()
        {
            var transaction = Transaction.Create(Guid.NewGuid(), null, new Money(100, EnCurrency.USD), Guid.NewGuid(), EnTransactionType.Expense);
            var futureDate = DateTime.UtcNow.AddDays(2);
            Action act = () => transaction.Update(date: futureDate);
            act.Should().Throw<FutureTransactionDateException>();
        }

        [Fact]
        public void UpdateTransaction_WithTooLongDescription_ShouldThrow()
        {
            var transaction = Transaction.Create(Guid.NewGuid(), null, new Money(100, EnCurrency.USD), Guid.NewGuid(), EnTransactionType.Expense);
            var longDescription = new string('x', 501);
            Action act = () => transaction.Update(description: longDescription);
            act.Should().Throw<InvalidTransactionDescriptionException>();
        }

        [Fact]
        public void Transaction_TypeChecks_ShouldWork()
        {
            var expense = Transaction.Create(Guid.NewGuid(), null, new Money(100, EnCurrency.USD), Guid.NewGuid(), EnTransactionType.Expense);
            var income = Transaction.Create(Guid.NewGuid(), null, new Money(100, EnCurrency.USD), Guid.NewGuid(), EnTransactionType.Income);
            var transfer = Transaction.Create(Guid.NewGuid(), Guid.NewGuid(), new Money(100, EnCurrency.USD), Guid.NewGuid(), EnTransactionType.Transfer);

            expense.IsExpense().Should().BeTrue();
            income.IsIncome().Should().BeTrue();
            transfer.IsTransfer().Should().BeTrue();
        }
    }
}
