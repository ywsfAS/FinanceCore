
namespace FinanceCore.Domain.Enums
{
    public enum EnRecurringTransactionStatus : byte
    {
        Scheduled = 0,
        Due = 1,
        Paused = 2,
        Completed = 3,
    }
}
