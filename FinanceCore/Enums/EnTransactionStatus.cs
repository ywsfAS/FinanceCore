
namespace FinanceCore.Domain.Enums
{
    public enum EnTransactionStatus : byte
    {
        Completed = 1,    // Successfully processed
        Voided = 2        // Cancelled/reversed
    }
}
