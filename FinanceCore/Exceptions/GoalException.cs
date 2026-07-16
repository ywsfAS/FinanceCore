using FinanceCore.Domain.Common;

namespace FinanceCore.Domain.Exceptions
{
    public class GoalNotActiveException : DomainException
    {
        public GoalNotActiveException(Guid goalId)
            : base($"Cannot perform operation. Goal {goalId} is not active.") { }
    }
    public class InvalidContributionAmountException : DomainException
    {
        public InvalidContributionAmountException(decimal amount)
            : base($"Contribution must be greater than zero. Received: {amount}") { }
    }
    public class CannotWithdrawFromCompletedGoalException : DomainException
    {
        public CannotWithdrawFromCompletedGoalException(Guid goalId)
            : base($"Cannot withdraw from completed goal {goalId}.") { }
    }
    public class InsufficientGoalFundsException : DomainException
    {
        public InsufficientGoalFundsException(Guid goalId, decimal requested, decimal available)
            : base($"Insufficient funds in goal {goalId}. Requested: {requested}, Available: {available}") { }
    }
    public class CannotCancelCompletedGoalException : DomainException
    {
        public CannotCancelCompletedGoalException(Guid goalId)
            : base($"Cannot cancel completed goal {goalId}.") { }
    }

    public class InvalidGoalName: DomainException
    {
        public InvalidGoalName(string goalName) : base($"Invalid goal name {goalName}.") { }
    }

    public class InvalidGoalTarget: DomainException
    {
        public InvalidGoalTarget(Money money) : base($"Invalid goal target amount {money.Amount} {money.Currency}.") { }
    }
    public class GoalTargetBelowCurrentAmountException : DomainException
    {
        public GoalTargetBelowCurrentAmountException(Money target , Money current) : base($"Target si below current amount => current : {current.Amount} {current.Currency} | target : {target.Amount} {target.Currency}") { }
    }
    public class InvalidGoalTargetDateException : DomainException
    {
        public InvalidGoalTargetDateException() : base($"Invalid goal target date") { }
    }
    public class GoalNotFoundException : DomainException
    {
        public GoalNotFoundException(Guid id) : base($"Goal Not Found {id}") { }
    }




}
