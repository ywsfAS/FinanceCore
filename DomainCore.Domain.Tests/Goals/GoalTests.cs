using System;
using FinanceCore.Domain.Goals;
using FinanceCore.Domain.Enums;
using FinanceCore.Domain.Common;
using FinanceCore.Domain.Events.Goal;
using Xunit;
using FluentAssertions;

namespace FinanceCore.Domain.Tests.Goals
{
    public class SavingsGoalTests
    {
        [Fact]
        public void CreateGoal_ShouldInitializeProperly()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var targetAmount = new Money(1000);
            var name = "Vacation Fund";
            var description = "Trip to Japan";
            var targetDate = DateTime.UtcNow.AddMonths(6);

            // Act
            var goal = SavingsGoal.Create(userId, name, targetAmount, targetDate, description);

            // Assert
            goal.UserId.Should().Be(userId);
            goal.Name.Should().Be(name);
            goal.Description.Should().Be(description);
            goal.TargetAmount.Amount.Should().Be(1000);
            goal.CurrentAmount.Amount.Should().Be(0);
            goal.TargetDate.Should().Be(targetDate);
            goal.Status.Should().Be(EnGoalStatus.Active);
            goal.CreatedAt.Should().NotBe(default);
        }

        [Fact]
        public void CreateGoal_WithInvalidInputs_ShouldThrow()
        {
            var userId = Guid.NewGuid();
            var amount = new Money(100);

            Action actEmptyUser = () => SavingsGoal.Create(Guid.Empty, "Test", amount);
            actEmptyUser.Should().Throw<ArgumentException>();

            Action actEmptyName = () => SavingsGoal.Create(userId, "", amount);
            actEmptyName.Should().Throw<ArgumentException>();

            Action actNegativeAmount = () => SavingsGoal.Create(userId, "Test", new Money(-10));
            actNegativeAmount.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void AddContribution_ShouldIncreaseCurrentAmount()
        {
            var goal = SavingsGoal.Create(Guid.NewGuid(), "Goal", new Money(1000));

            goal.AddContribution(new Money(200));

            goal.CurrentAmount.Amount.Should().Be(200);
            goal.GetPercentageComplete().Should().Be(20);
            goal.UpdatedAt.Should().NotBeNull();
        }

        [Fact]
        public void AddContribution_WhenExceedingTarget_ShouldCompleteGoal()
        {
            var goal = SavingsGoal.Create(Guid.NewGuid(), "Goal", new Money(500));

            goal.AddContribution(new Money(500));

            goal.Status.Should().Be(EnGoalStatus.Completed);
            goal.CurrentAmount.Amount.Should().Be(500);
            goal.CompletedAt.Should().NotBeNull();
        }

        [Fact]
        public void AddContribution_ToInactiveOrCompletedGoal_ShouldThrow()
        {
            var goal = SavingsGoal.Create(Guid.NewGuid(), "Goal", new Money(500));
            goal.AddContribution(new Money(500)); // completes goal

            Action act = () => goal.AddContribution(new Money(100));
            act.Should().Throw<InvalidOperationException>();
        }

        [Fact]
        public void AddContribution_WithNonPositiveAmount_ShouldThrow()
        {
            var goal = SavingsGoal.Create(Guid.NewGuid(), "Goal", new Money(500));
            Action act = () => goal.AddContribution(new Money(0));
            act.Should().Throw<ArgumentException>();
        }

        [Fact]
        public void WithdrawContribution_ShouldReduceCurrentAmount()
        {
            var goal = SavingsGoal.Create(Guid.NewGuid(), "Goal", new Money(1000));
            goal.AddContribution(new Money(300));

            goal.WithdrawContribution(new Money(100));

            goal.CurrentAmount.Amount.Should().Be(200);
            goal.UpdatedAt.Should().NotBeNull();
        }

        [Fact]
        public void WithdrawContribution_OverCurrentAmount_ShouldThrow()
        {
            var goal = SavingsGoal.Create(Guid.NewGuid(), "Goal", new Money(1000));
            goal.AddContribution(new Money(200));

            Action act = () => goal.WithdrawContribution(new Money(300));
            act.Should().Throw<InvalidOperationException>();
        }

        [Fact]
        public void WithdrawContribution_FromCompletedGoal_ShouldThrow()
        {
            var goal = SavingsGoal.Create(Guid.NewGuid(), "Goal", new Money(500));
            goal.AddContribution(new Money(500));

            Action act = () => goal.WithdrawContribution(new Money(100));
            act.Should().Throw<InvalidOperationException>();
        }

        [Fact]
        public void GetRemainingAmount_ShouldReturnCorrectValue()
        {
            var goal = SavingsGoal.Create(Guid.NewGuid(), "Goal", new Money(1000));
            goal.AddContribution(new Money(300));

            var remaining = goal.GetRemainingAmount();
            remaining.Amount.Should().Be(700);
        }

        [Fact]
        public void GetPercentageComplete_ShouldReturnCorrectValue()
        {
            var goal = SavingsGoal.Create(Guid.NewGuid(), "Goal", new Money(1000));
            goal.AddContribution(new Money(250));

            goal.GetPercentageComplete().Should().Be(25);
        }
    }
}
