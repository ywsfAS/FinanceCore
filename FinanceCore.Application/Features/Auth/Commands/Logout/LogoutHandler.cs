using FinanceCore.Application.Abstractions;
using FinanceCore.Domain.Exceptions;
using MediatR;

namespace FinanceCore.Application.Features.Auth.Commands.Logout
{
    public sealed class LogoutHandler : IRequestHandler<LogoutCommand>
    {
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IPasswordHasher _hasher;
        public LogoutHandler(IRefreshTokenRepository refreshTokenRepository , IPasswordHasher hasher)
        {
            _refreshTokenRepository = refreshTokenRepository;
            _hasher = hasher;
        }

        public async Task Handle(LogoutCommand cmd , CancellationToken token)
        {
            var hash = _hasher.Hash(cmd.RefreshToken);
            var refreshToken = await _refreshTokenRepository.GetByTokenHashAsync(hash,token);
            if (refreshToken is null) { 
                throw new InvalidCredentialsException();
            }

            await _refreshTokenRepository.RevokeRefreshTokenAsync(refreshToken.Id,DateTime.UtcNow,token);

        }
    }
}
