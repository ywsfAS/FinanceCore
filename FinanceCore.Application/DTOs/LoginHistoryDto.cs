using FinanceCore.Domain.Enums;

namespace FinanceCore.Application.DTOs
{
    public sealed class LoginHistoryDto
    {
        public Guid Id { get; init; }

        public DateTime LoginAt { get; init; }

        public string? IpAddress { get; init; }

        public string? UserAgent { get; init; }

        public string? DeviceName { get; init; }

        public string? Os { get; init; }

        public EnLoginStatus Status { get; init; }

        public string? FailureReason { get; init; }

    }
}
