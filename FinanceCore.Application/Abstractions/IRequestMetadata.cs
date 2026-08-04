
namespace FinanceCore.Application.Abstractions
{
    public interface IRequestMetadata
    {
        string? IpAddress { get; }

        string? UserAgent { get; }

        string? DeviceName { get; }

        string? Os { get; }
    }
}
