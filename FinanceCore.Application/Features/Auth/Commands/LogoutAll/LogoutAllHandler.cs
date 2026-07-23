using FinanceCore.Application.Abstractions;
using MediatR;

namespace FinanceCore.Application.Features.Auth.Commands.LogoutAll
{
    public sealed class LogoutAllHandler : IRequestHandler<LogoutAllCommand>
    {
        private readonly IRefreshTokenRepository _refreshTokenRepository;
       public LogoutAllHandler( IRefreshTokenRepository refreshTokenRepository) { 
            _refreshTokenRepository = refreshTokenRepository;
       }
        public async Task Handle(LogoutAllCommand cmd , CancellationToken token)
        {
            await _refreshTokenRepository.RevokeAllForUserAsync(cmd.UserId,DateTime.UtcNow,token);
        }
    }
}
