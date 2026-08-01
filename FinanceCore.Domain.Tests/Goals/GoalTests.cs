using FinanceCore.Domain.Goals;
using FinanceCore.Domain.Enums;
using FinanceCore.Domain.Common;
using FluentAssertions;
using FinanceCore.Domain.Exceptions;

namespace FinanceCore.Domain.Tests.Goals
{
    public class SavingsGoalTests
    {
        [Fact]
        public void CreateGoal_ShouldInitializeProperly()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var targetAmount = new Money(1000, EnCurrency.USD);
            var name = "Vacation Fund";
            var description = "Trip to Japan";
            var targetDate = DateTime.UtcNow.AddMonths(6);

            // Act
            var goal = SavingsGoal.Create(null,userId, name, targetAmount, targetDate, description);

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
            var amount = new Money(100, EnCurrency.USD);

            Action actEmptyUser = () => SavingsGoal.Create(null,Guid.Empty, "Test", amount);
            actEmptyUser.Should().Throw<UserIdNotProvidedException>();

            Action actEmptyName = () => SavingsGoal.Create(null,userId, "", amount);
            actEmptyName.Should().Throw<InvalidGoalName>();

            Action actNegativeAmount = () => SavingsGoal.Create(null,userId, "Test", new Money(-10,EnCurrency.USD));
            actNegativeAmount.Should().Throw<MoneyIsNegativeException>();
        }

        [Fact]
        public void AddContribution_ShouldIncreaseCurrentAmount()
        {
            var goal = SavingsGoal.Create(null, Guid.NewGuid(), "Goal", new Money(1000,EnCurrency.USD));

            goal.AddContribution(Guid.NewGuid(),DateTime.UtcNow,new Money(200, EnCurrency.USD));

            goal.CurrentAmount.Amount.Should().Be(200);
            goal.GetPercentageComplete().Should().Be(20);
            goal.UpdatedAt.Should().NotBeNull();
        }

        [Fact]
        public void AddContribution_WhenExceedingTarget_ShouldCompleteGoal()
        {
            var goal = SavingsGoal.Create(null, Guid.NewGuid(), "Goal", new Money(500,EnCurrency.USD));

            goal.AddContribution(Guid.NewGuid(),DateTime.UtcNow,new Money(500, EnCurrency.USD));

            goal.Status.Should().Be(EnGoalStatus.Completed);
            goal.CurrentAmount.Amount.Should().Be(500);
            goal.CompletedAt.Should().NotBeNull();
        }

        [Fact]
        public void AddContribution_ToInactiveOrCompletedGoal_ShouldThrow()
        {
            var goal = SavingsGoal.Create(null, Guid.NewGuid(), "Goal", new Money(500,EnCurrency.USD));
            goal.AddContribution(Guid.NewGuid(),DateTime.UtcNow,new Money(500, EnCurrency.USD)); // completes goal

            Action act = () => goal.AddContribution(Guid.NewGuid(),DateTime.UtcNow,new Money(100, EnCurrency.USD));
            act.Should().Throw<InvalidOperationException>();
        }

        [Fact]
        public void AddContribution_WithNonPositiveAmount_ShouldThrow()
        {
            var goal = SavingsGoal.Create(null, Guid.NewGuid(), "Goal", new Money(500,EnCurrency.USD));
            Action act = () => goal.AddContribution(Guid.NewGuid(),DateTime.UtcNow,new Money(0, EnCurrency.USD));
            act.Should().Throw<InvalidContributionAmountException>();
        }

        [Fact]
        public void WithdrawContribution_ShouldReduceCurrentAmount()
        {
            var goal = SavingsGoal.Create(null, Guid.NewGuid(), "Goal", new Money(1000,EnCurrency.USD));
            goal.AddContribution(Guid.NewGuid(),DateTime.UtcNow,new Money(300, EnCurrency.USD));

            goal.WithdrawContribution(Guid.NewGuid(),new Money(100, EnCurrency.USD));

            goal.CurrentAmount.Amount.Should().Be(200);
            goal.UpdatedAt.Should().NotBeNull();
        }

        [Fact]
        public void WithdrawContribution_OverCurrentAmount_ShouldThrow()
        {
            var goal = SavingsGoal.Create(null, Guid.NewGuid(), "Goal", new Money(1000,EnCurrency.USD));
            goal.AddContribution(Guid.NewGuid(),DateTime.UtcNow,new Money(200, EnCurrency.USD));

            Action act = () => goal.WithdrawContribution(Guid.NewGuid(),new Money(300, EnCurrency.USD));
            act.Should().Throw<InsufficientGoalFundsException>();
        }

        [Fact]
        public void WithdrawContribution_FromCompletedGoal_ShouldThrow()
        {
            var goal = SavingsGoal.Create(null, Guid.NewGuid(), "Goal", new Money(500,EnCurrency.USD));
            goal.AddContribution(Guid.NewGuid(),DateTime.UtcNow,new Money(500, EnCurrency.USD));

            Action act = () => goal.WithdrawContribution(Guid.NewGuid(),new Money(100, EnCurrency.USD));
            act.Should().Throw<CannotWithdrawFromCompletedGoalException>();
        }

        [Fact]
        public void GetRemainingAmount_ShouldReturnCorrectValue()
        {
            var goal = SavingsGoal.Create(null, Guid.NewGuid(), "Goal", new Money(1000,EnCurrency.USD));
            goal.AddContribution(Guid.NewGuid(),DateTime.UtcNow,new Money(300, EnCurrency.USD));

            var remaining = goal.GetRemainingAmount();
            remaining.Amount.Should().Be(700);
        }

        [Fact]
        public void GetPercentageComplete_ShouldReturnCorrectValue()
        {
            var goal = SavingsGoal.Create(null, Guid.NewGuid(), "Goal", new Money(1000,EnCurrency.USD));
            goal.AddContribution(Guid.NewGuid(),DateTime.UtcNow,new Money(250, EnCurrency.USD));

            goal.GetPercentageComplete().Should().Be(25);
        }
    }
}
