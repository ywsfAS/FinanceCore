using FinanceCore.Application.Abstractions;
using FinanceCore.Application.DTOs.Auth;
using FinanceCore.Domain.Exceptions;
using FinanceCore.Domain.RefreshToken;
using MediatR;

namespace FinanceCore.Application.Features.Auth.Commands.Refresh
{
    public sealed class RefreshHandler : IRequestHandler<RefreshCommand,LoginDto>
    {
        private readonly IRefreshTokenHasher _hasher;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IUserRepository _userRepository;
        private readonly IRefreshTokenGenerator _refreshTokenGenerator;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        private readonly IUnitOfWork _unitOfWork;
        public RefreshHandler(IRefreshTokenHasher hasher,IUnitOfWork unitOfWork,IJwtTokenGenerator jwtGenerator, IRefreshTokenRepository refreshRepository , IUserRepository repo , IRefreshTokenGenerator generator) {
            _refreshTokenRepository = refreshRepository;
            _userRepository = repo;
            _hasher = hasher; 
            _refreshTokenGenerator = generator;
            _jwtTokenGenerator = jwtGenerator;
            _unitOfWork = unitOfWork;
        }
        public async Task<LoginDto> Handle(RefreshCommand cmd , CancellationToken token)
        {
            var now = DateTime.UtcNow;
            var hash = _hasher.Hash(cmd.refreshToken);
            var refreshToken = await _refreshTokenRepository.GetByTokenHashAsync(hash,token);
            if(refreshToken is null || !refreshToken.IsActive(now))
            {
                throw new InvalidCredentialsException();
            }
            var user = await _userRepository.GetByIdAsync(refreshToken.UserId,token); 
            if(user is null)
            {
                throw new InvalidCredentialsException();
            }
            var newRawRefreshToken =
            _refreshTokenGenerator.GenerateRefreshToken();

            var newTokenHash =
                _hasher.Hash(
                    newRawRefreshToken);

            var newRefreshToken = RefreshToken.Create(
            refreshToken.UserId,
            newTokenHash,
            now.AddDays(7),
            refreshToken.DeviceLabel,
            refreshToken.UserAgent);

            var jwtToken =  _jwtTokenGenerator.GenerateToken(user);
            await _unitOfWork.BeginAsync(token);
            try
            {
                await _refreshTokenRepository.RevokeRefreshTokenAsync(refreshToken.Id,now, token,_unitOfWork);
                await _refreshTokenRepository.AddAsync(newRefreshToken, token,_unitOfWork);

                await _unitOfWork.CommitAsync(token);
            }
            catch{
                await _unitOfWork.RollBackAsync(token); 
            }
            
            return new LoginDto(user.Id, user.Email.Address, jwtToken, newRawRefreshToken,newRefreshToken.ExpiresAt);
        }
    }
}
