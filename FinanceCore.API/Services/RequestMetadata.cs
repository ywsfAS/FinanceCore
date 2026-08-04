using CsvHelper;
using FinanceCore.Application.Abstractions;
using UAParser;

namespace FinanceCore.API.Services;

public sealed class RequestMetadata : IRequestMetadata
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public RequestMetadata(
        IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    private string? RawUserAgent =>
        _httpContextAccessor.HttpContext?
            .Request
            .Headers["User-Agent"]
            .ToString();

    private ClientInfo? ParsedClient =>
        string.IsNullOrWhiteSpace(RawUserAgent)
            ? null
            : Parser.GetDefault().Parse(RawUserAgent);

    public string? IpAddress =>
        _httpContextAccessor.HttpContext?
            .Connection
            .RemoteIpAddress?
            .ToString();

    public string? UserAgent =>
        RawUserAgent;

    public string? Os =>
        ParsedClient?.OS.Family;

    public string? DeviceName =>
        ParsedClient?.Device.Family;
}