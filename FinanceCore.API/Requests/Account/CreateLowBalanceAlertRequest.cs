namespace FinanceCore.API.Requests.Account
{
    public sealed record CreateLowBalanceAlertRequest(
        decimal ThresholdAmount);
}
