namespace FinanceCore.API.Requests.Profile
{
    public sealed class UploadProfileImageRequest
    {
        public IFormFile File { get; set; } = default!;
    }
}
