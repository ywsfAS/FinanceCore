using FinanceCore.Domain.PasswordRestToken;

namespace FinanceCore.Application.Abstractions
{
    public interface IPasswordResetTokenRepository
    {
        Task AddAsync(
            PasswordResetToken token,
            CancellationToken cancellationToken = default);

        Task<PasswordResetToken?> GetByTokenAsync(
            string token,
            CancellationToken cancellationToken = default);

        Task MarkAsUsedAsync(
            PasswordResetToken token,
            CancellationToken cancellationToken = default);
    }
}
