using FinanceCore.Application.Abstractions;
using FinanceCore.Application.DTOs.Auth;
using FinanceCore.Domain.Common;
using FinanceCore.Domain.Exceptions;
using FinanceCore.Domain.RefreshToken;
using MediatR;

namespace FinanceCore.Application.Features.Auth.Commands.Login
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand,LoginDto>
    {
        private readonly IUserRepository _userRepository;
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IJwtTokenGenerator _JwtGenerator;
        private readonly IPasswordHasher _hasher;
        private readonly IRefreshTokenGenerator _refreshTokenGenerator;

        public LoginUserCommandHandler(IUserRepository userRepository , IRefreshTokenRepository tokenRepository,IPasswordHasher hasher , IJwtTokenGenerator jwtGenerator , IRefreshTokenGenerator generator)
        {
            _userRepository = userRepository;
            _hasher = hasher;
            _JwtGenerator = jwtGenerator;
            _refreshTokenGenerator = generator;
            _refreshTokenRepository = tokenRepository;
        }
        public async Task<LoginDto> Handle(LoginUserCommand command , CancellationToken token = default)
        {
            var email = new Email(command.Email);
            var user = await _userRepository.GetByEmailAsync(email);
            if (user == null) {
                throw new InvalidCredentialsException();  
            }
            var IsPasswordValid = _hasher.Verify(command.Password, user.PasswordHash);
            if (!IsPasswordValid) { 
                throw new InvalidCredentialsException();  
            }
            var JwtToken =  _JwtGenerator.GenerateToken(user);
            var rawRefreshToken = _refreshTokenGenerator.GenerateRefreshToken();
            var refreshTokenHash = _hasher.Hash(rawRefreshToken);
            DateTime expiresAt = DateTime.UtcNow.AddDays(7);

            var refreshToken = RefreshToken.Create(
            userId: user.Id,
            tokenHash : refreshTokenHash,
            expiresAt: expiresAt 
            );

            await _refreshTokenRepository.AddAsync(refreshToken, token);
        

            return new LoginDto(user.Id, user.Email.Address, JwtToken,rawRefreshToken,expiresAt);

        }

    }
}
