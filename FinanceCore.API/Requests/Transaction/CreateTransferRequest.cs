namespace FinanceCore.API.Requests.Transaction
{
    public record CreateTransferRequest(Guid AccountId, Guid ToAccountId,decimal Amount ,string? Description, string? notes);
}
