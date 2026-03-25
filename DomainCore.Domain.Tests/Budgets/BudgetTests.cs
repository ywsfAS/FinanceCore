using FinanceCore.Domain.Budgets;
using FinanceCore.Domain.Common;
using FinanceCore.Domain.Enums;
using FinanceCore.Domain.Exceptions;
using FluentAssertions;
using System;
using Xunit;

namespace FinanceCore.Domain.Tests.Budgets
{
    public class BudgetTests
    {
        [Fact]
        public void CreateBudget_ShouldInitializeProperly()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var categoryId = Guid.NewGuid();
            var name = "Monthly Groceries";
            var currency = EnCurrency.USD;
            decimal amount = 500m;
            var period = EnPeriod.Monthly;
            var startDate = DateTime.UtcNow;

            // Act
            var budget = Budget.Create(userId, categoryId, name, currency, amount, period, startDate);

            // Assert
            budget.UserId.Should().Be(userId);
            budget.CategoryId.Should().Be(categoryId);
            budget.Name.Should().Be(name);
            budget.Currency.Should().Be(currency);
            budget.Amount.Amount.Should().Be(amount);
            budget.Period.Should().Be(period);
            budget.StartDate.Should().Be(startDate);
            budget.EndDate.Should().BeCloseTo(startDate.AddMonths(1).AddSeconds(-1), TimeSpan.FromSeconds(1));
            budget.UpdatedAt.Should().BeNull();
        }

        [Fact]
        public void CreateBudget_WithInvalidName_ShouldThrow()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var categoryId = Guid.NewGuid();

            // Act
            Action actEmpty = () => Budget.Create(userId, categoryId, "", EnCurrency.USD, 100, EnPeriod.Monthly, DateTime.UtcNow);
            Action actShort = () => Budget.Create(userId, categoryId, "A", EnCurrency.USD, 100, EnPeriod.Monthly, DateTime.UtcNow);
            Action actLong = () => Budget.Create(userId, categoryId, new string('x', 101), EnCurrency.USD, 100, EnPeriod.Monthly, DateTime.UtcNow);

            // Assert
            actEmpty.Should().Throw<InvalidBudgetNameException>();
            actShort.Should().Throw<InvalidBudgetNameException>();
            actLong.Should().Throw<InvalidBudgetNameException>();
        }

        [Fact]
        public void CreateBudget_WithInvalidAmount_ShouldThrow()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var categoryId = Guid.NewGuid();

            // Act
            Action actZero = () => Budget.Create(userId, categoryId, "Food", EnCurrency.USD, 0, EnPeriod.Monthly, DateTime.UtcNow);
            Action actNegative = () => Budget.Create(userId, categoryId, "Food", EnCurrency.USD, -10, EnPeriod.Monthly, DateTime.UtcNow);

            // Assert
            actZero.Should().Throw<InvalidBudgetAmountException>();
            actNegative.Should().Throw<InvalidBudgetAmountException>();
        }

        [Fact]
        public void UpdateAmount_ShouldChangeAmountAndAddDomainEvent()
        {
            // Arrange
            var budget = Budget.Create(Guid.NewGuid(), Guid.NewGuid(), "Budget", EnCurrency.USD, 100, EnPeriod.Monthly, DateTime.UtcNow);
            var newAmount = new Money(200);

            // Act
            budget.UpdateAmount(newAmount);

            // Assert
            budget.Amount.Amount.Should().Be(200);
            budget.UpdatedAt.Should().NotBeNull();
        }

        [Fact]
        public void UpdateAmount_WithInvalidAmount_ShouldThrow()
        {
            // Arrange
            var budget = Budget.Create(Guid.NewGuid(), Guid.NewGuid(), "Budget", EnCurrency.USD, 100, EnPeriod.Monthly, DateTime.UtcNow);

            // Act
            Action act = () => budget.UpdateAmount(new Money(0));

            // Assert
            act.Should().Throw<InvalidBudgetAmountException>();
        }

        [Fact]
        public void UpdateName_ShouldChangeName()
        {
            // Arrange
            var budget = Budget.Create(Guid.NewGuid(), Guid.NewGuid(), "Budget", EnCurrency.USD, 100, EnPeriod.Monthly, DateTime.UtcNow);

            // Act
            budget.UpdateName("New Budget");

            // Assert
            budget.Name.Should().Be("New Budget");
            budget.UpdatedAt.Should().NotBeNull();
        }

        [Fact]
        public void UpdateName_WithInvalidName_ShouldThrow()
        {
            // Arrange
            var budget = Budget.Create(Guid.NewGuid(), Guid.NewGuid(), "Budget", EnCurrency.USD, 100, EnPeriod.Monthly, DateTime.UtcNow);

            // Act
            Action actEmpty = () => budget.UpdateName("");
            Action actShort = () => budget.UpdateName("A");
            Action actLong = () => budget.UpdateName(new string('x', 101));

            // Assert
            actEmpty.Should().Throw<InvalidBudgetNameException>();
            actShort.Should().Throw<InvalidBudgetNameException>();
            actLong.Should().Throw<InvalidBudgetNameException>();
        }

        [Fact]
        public void ExtendPeriod_ShouldUpdatePeriodAndEndDate()
        {
            // Arrange
            var budget = Budget.Create(Guid.NewGuid(), Guid.NewGuid(), "Budget", EnCurrency.USD, 100, EnPeriod.Monthly, DateTime.UtcNow);
            var oldEndDate = budget.EndDate;

            // Act
            budget.ExtendPeriod(EnPeriod.Quarterly);

            // Assert
            budget.Period.Should().Be(EnPeriod.Quarterly);
            budget.EndDate.Should().BeCloseTo(Budget.Create(Guid.NewGuid(), Guid.NewGuid(), "Temp", EnCurrency.USD, 100, EnPeriod.Quarterly, DateTime.UtcNow).EndDate, TimeSpan.FromSeconds(1));
            budget.UpdatedAt.Should().NotBeNull();
        }

        [Fact]
        public void IsPeriodExpired_ShouldReturnCorrectly()
        {
            // Arrange
            var budget = Budget.Create(Guid.NewGuid(), Guid.NewGuid(), "Budget", EnCurrency.USD, 100, EnPeriod.Monthly, DateTime.UtcNow.AddMonths(-2));

            // Act & Assert
            budget.IsPeriodExpired().Should().BeTrue();
        }

        [Fact]
        public void GetDaysRemaining_ShouldReturnZeroIfExpired()
        {
            // Arrange
            var budget = Budget.Create(Guid.NewGuid(), Guid.NewGuid(), "Budget", EnCurrency.USD, 100, EnPeriod.Monthly, DateTime.UtcNow.AddMonths(-2));

            // Act
            var daysRemaining = budget.GetDaysRemaining();

            // Assert
            daysRemaining.Should().Be(0);
        }
    }
}
