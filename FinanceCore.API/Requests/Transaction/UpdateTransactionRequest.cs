namespace FinanceCore.API.Requests.Transaction
{
    public record UpdateTransactionRequest(  
        Guid Id,
        Guid? CategoryId,
        decimal Amount,
        DateTime Date,
        string? Description = null);

}
