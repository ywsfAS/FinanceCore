using FinanceCore.Application.Abstractions;
using Microsoft.AspNetCore.Hosting;

namespace FinanceCore.Infrastructure.Services
{
    public sealed class LocalImageStorage : IImageStorage
    {
        private readonly string _uploadFolder;

        public LocalImageStorage(IWebHostEnvironment environment)
        {
            _uploadFolder = Path.Combine(
                environment.WebRootPath,
                "uploads",
                "users",
                "profiles");

            Directory.CreateDirectory(_uploadFolder);
        }

        public async Task<string> SaveAsync(
            Stream stream,
            CancellationToken cancellationToken)
        {
            if (stream.CanSeek) stream.Position = 0;
            var newFilename = $"{Guid.NewGuid()}.webp";

            var path = Path.Combine(
                _uploadFolder,
                newFilename);

            await using var file = new FileStream(
                path,
                FileMode.CreateNew,
                FileAccess.Write,
                FileShare.None,
                bufferSize: 81920,
                useAsync: true);

            await stream.CopyToAsync(
                file,
                cancellationToken);

            return newFilename;
        }

        public Task DeleteAsync(
            string filename,
            CancellationToken cancellationToken)
        {
            var path = Path.Combine(
                _uploadFolder,
                filename);

            if (File.Exists(path))
            {
                File.Delete(path);
            }

            return Task.CompletedTask;
        }
    }
}