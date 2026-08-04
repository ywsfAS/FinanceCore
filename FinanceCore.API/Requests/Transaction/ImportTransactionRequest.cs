namespace FinanceCore.API.Requests.Transaction
{
    public sealed class ImportTransactionRequest
    {
        public IFormFile File { get; set; } = null!;
        public Guid AccountId { get; set; }
    }
}
