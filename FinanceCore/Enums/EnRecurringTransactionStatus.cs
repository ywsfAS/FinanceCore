
namespace FinanceCore.Domain.Enums
{
    public enum EnRecurringTransactionStatus : byte
    {
        Scheduled = 0,
        Due = 1,
        Paused = 2,
        Cancel = 3,
        Completed = 4,
    }
}
