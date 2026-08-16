using FinanceCore.Domain.Common;
using FinanceCore.Domain.Enums;

public sealed class CreditStatement : Entity
{
    public Guid AccountId { get; private set; }

    public DateTime PeriodStart { get; private set; }
    public DateTime PeriodEnd { get; private set; }
    public DateTime PaymentDueDate { get; private set; }

    public Money StatementBalance { get; private set; }
    public Money MinimumPayment { get; private set; }
    public Money PaidAmount { get; private set; }

    public EnCreditStatementStatus Status { get; private set; }

    public DateTime CreatedAt { get; private set; }
    public DateTime? MinimumPaymentSatisfiedAt { get; private set; }
    public DateTime? PaidAt { get; private set; }

    private CreditStatement() { }

    public CreditStatement(
        Guid accountId,
        DateTime periodStart,
        DateTime periodEnd,
        DateTime paymentDueDate,
        Money statementBalance,
        Money minimumPayment)
    {
        if (accountId == Guid.Empty)
            throw new ArgumentException(
                "Account ID is required.",
                nameof(accountId));

        if (periodEnd <= periodStart)
            throw new ArgumentException(
                "Period end must be after period start.",
                nameof(periodEnd));

        if (paymentDueDate < periodEnd)
            throw new ArgumentException(
                "Payment due date cannot be before the statement period ends.",
                nameof(paymentDueDate));

        if (minimumPayment.IsGreaterThan(statementBalance))
            throw new ArgumentException(
                "Minimum payment cannot exceed statement balance.",
                nameof(minimumPayment));

        if (minimumPayment.Currency != statementBalance.Currency)
            throw new ArgumentException(
                "Minimum payment must use the same currency as the statement balance.",
                nameof(minimumPayment));

        AccountId = accountId;

        PeriodStart = periodStart;
        PeriodEnd = periodEnd;
        PaymentDueDate = paymentDueDate;

        StatementBalance = statementBalance;
        MinimumPayment = minimumPayment;
        PaidAmount = Money.Zero(statementBalance.Currency);

        Status = EnCreditStatementStatus.Open;

        CreatedAt = DateTime.UtcNow;
    }

    public void RegisterPayment(Money amount, DateTime paymentDate)
    {
        if (amount.IsLessOrEqual(Money.Zero(amount.Currency)))
            throw new ArgumentException(
                "Payment amount must be greater than zero.",
                nameof(amount));

        if (amount.Currency != StatementBalance.Currency)
            throw new ArgumentException(
                "Payment must use the same currency as the statement.",
                nameof(amount));

        if (Status == EnCreditStatementStatus.Paid)
            throw new InvalidOperationException(
                "The statement is already fully paid.");

        PaidAmount = PaidAmount.Add(amount);

        if (PaidAmount.IsGreaterOrEqual(StatementBalance))
        {
            PaidAmount = StatementBalance;

            if (!MinimumPaymentSatisfiedAt.HasValue)
                MinimumPaymentSatisfiedAt = paymentDate;

            PaidAt = paymentDate;
            Status = EnCreditStatementStatus.Paid;

            return;
        }

        if (PaidAmount.IsGreaterOrEqual(MinimumPayment)
            && !MinimumPaymentSatisfiedAt.HasValue)
        {
            MinimumPaymentSatisfiedAt = paymentDate;
            Status = EnCreditStatementStatus.MinimumPaymentSatisfied;
        }
    }

    public bool IsMinimumPaymentSatisfied()
    {
        return PaidAmount.IsGreaterOrEqual(MinimumPayment);
    }

    public bool IsOverdue(DateTime currentDate)
    {
        return !IsMinimumPaymentSatisfied()
            && currentDate.Date > PaymentDueDate.Date;
    }
}