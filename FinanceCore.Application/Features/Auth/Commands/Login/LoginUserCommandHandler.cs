using FinanceCore.Application.Abstractions;
using FinanceCore.Application.DTOs.Auth;
using FinanceCore.Domain.Exceptions;
using MediatR;

namespace FinanceCore.Application.Features.Auth.Commands.Login
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand,LoginDto>
    {
        private readonly IUserRepository _userRepository;
        private readonly IJwtTokenGenerator _JwtGenerator;
        private readonly IPasswordHasher _hasher;

        public LoginUserCommandHandler(IUserRepository userRepository , IPasswordHasher hasher , IJwtTokenGenerator jwtGenerator)
        {
            _userRepository = userRepository;
            _hasher = hasher;
            _JwtGenerator = jwtGenerator;
        }
        public async Task<LoginDto> Handle(LoginUserCommand command , CancellationToken token = default)
        {
            var user = await _userRepository.GetByEmailAsync(command.Email);
            if (user == null) {
                throw new InvalidCredentialsException();  
            }
            var IsPasswordValid = _hasher.Verify(command.Password, user.PasswordHash);
            if (!IsPasswordValid) { 
                throw new InvalidCredentialsException();  
            }
            var JwtToken =  _JwtGenerator.GenerateToken(user);
            return new LoginDto(user.Id, user.Email.Address, JwtToken);

        }

    }
}
