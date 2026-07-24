using FinanceCore.Application.Abstractions;
using ImageMagick;

namespace FinanceCore.Infrastructure.Services
{
    public class ImageProcessor : IImageProcessor
    {
        public async Task<Stream> ProcessAsync(Stream input, CancellationToken token)
        {
            using var image = new MagickImage(input);

            // resize and strip metadata infos
            image.Resize(48, 48);
            image.Strip();
            image.Format = MagickFormat.WebP;
            image.Quality = 90;

            // load the stream from image
            MemoryStream output = new MemoryStream();

            await image.WriteAsync(output, token);
            return output;

        }
    }
}
