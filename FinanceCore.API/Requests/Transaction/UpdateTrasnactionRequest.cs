namespace FinanceCore.API.Requests.Transaction
{
    public record UpdateTrasnactionRequest(  
        Guid Id,
        Guid? CategoryId,
        decimal Amount,
        DateTime Date,
        string? Description = null);

}
