using FinanceCore.Domain.PasswordRestToken;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
