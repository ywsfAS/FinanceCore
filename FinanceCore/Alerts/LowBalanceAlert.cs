namespace FinanceCore.Accounts;

public class LowBalanceAlert
{
    public Guid Id { get; private set; }
    public Guid AccountId { get; private set; }

    public decimal ThresholdAmount { get; private set; }
    public bool IsEnabled { get; private set; }
    public bool IsTriggered { get; private set; }

    public DateTime? LastTriggeredAt { get; private set; }

    public DateTime CreatedAt { get; private set; }
    public DateTime? UpdatedAt { get; private set; }

    private LowBalanceAlert()
    {
    }

    public LowBalanceAlert(
        Guid id,
        Guid accountId,
        decimal thresholdAmount)
    {
        if (accountId == Guid.Empty)
            throw new ArgumentException("Account ID cannot be empty.", nameof(accountId));

        if (thresholdAmount <= 0)
            throw new ArgumentOutOfRangeException(
                nameof(thresholdAmount),
                "Threshold amount must be greater than zero.");

        Id = id;
        AccountId = accountId;
        ThresholdAmount = thresholdAmount;

        IsEnabled = false;
        IsTriggered = false;

        CreatedAt = DateTime.UtcNow;
    }

    public void UpdateThreshold(decimal thresholdAmount)
    {
        if (thresholdAmount <= 0)
            throw new ArgumentOutOfRangeException(
                nameof(thresholdAmount),
                "Threshold amount must be greater than zero.");

        ThresholdAmount = thresholdAmount;
        IsTriggered = false;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Enable()
    {
        IsEnabled = true;
        UpdatedAt = DateTime.UtcNow;
    }

    public void Disable()
    {
        IsEnabled = false;
        IsTriggered = false;
        UpdatedAt = DateTime.UtcNow;
    }

    public bool ShouldTrigger(decimal currentBalance)
    {
        return IsEnabled
            && !IsTriggered
            && currentBalance <= ThresholdAmount;
    }

    public void Trigger(DateTime triggeredAt)
    {
        if (!IsEnabled)
            throw new InvalidOperationException(
                "Cannot trigger a disabled low-balance alert.");

        IsTriggered = true;
        LastTriggeredAt = triggeredAt;
        UpdatedAt = triggeredAt;
    }

    public void Reset(DateTime resetAt)
    {
        if (IsTriggered && !IsEnabled)
            return;

        IsTriggered = false;
        UpdatedAt = resetAt;
    }
}
