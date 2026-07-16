
namespace FinanceCore.Application.Abstractions
{
    public interface IImageStorage
    {
        Task<string> SaveImage(Stream stream , string filename , Guid id);
    }
}
