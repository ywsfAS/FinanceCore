using FinanceCore.Domain.Common;
using FinanceCore.Domain.Enums;

namespace FinanceCore.Domain.LoginHistory
{
    public sealed class LoginHistory : Entity
    {
        public Guid UserId { get; private set; }

        public DateTime LoginAt { get; private set; }

        public string? IpAddress { get; private set; }

        public string? UserAgent { get; private set; }

        public string? DeviceName { get; private set; }

        public string? Os { get; private set; }

        public EnLoginStatus Status { get; private set; }

        public string? FailureReason { get; private set; }

        private LoginHistory()
        {
        }

        private LoginHistory(
            Guid id,
            Guid userId,
            DateTime loginAt,
            string? ipAddress,
            string? userAgent,
            string? deviceName,
            string? os,
            EnLoginStatus status,
            string? failureReason)
        {
            Id = id;
            UserId = userId;
            LoginAt = loginAt;
            IpAddress = ipAddress;
            UserAgent = userAgent;
            DeviceName = deviceName;
            Os = os;
            Status = status;
            FailureReason = failureReason;
        }

        public static LoginHistory Create(
            Guid userId,
            string? ipAddress,
            string? userAgent,
            string? deviceName,
            string? os,
            EnLoginStatus status,
            string? failureReason = null)
        {
            return new LoginHistory(
                Guid.NewGuid(),
                userId,
                DateTime.UtcNow,
                ipAddress,
                userAgent,
                deviceName,
                os,
                status,
                failureReason);
        }
    }
}
