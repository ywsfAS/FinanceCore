using FinanceCore.Application.Abstractions;
using MediatR;
using FinanceCore.Domain.Exceptions;

namespace FinanceCore.Application.Features.Auth.Commands.Login
{
    public class LoginUserCommandHandler : IRequestHandler<LoginUserCommand,string>
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
        public async Task<string> Handle(LoginUserCommand command , CancellationToken token = default)
        {
            var user = await _userRepository.GetByEmailAsync(command.Email);
            if (user == null) {
                throw new InvalidCredentialsException();  
            }
            var IsPasswordValid = _hasher.Verify(command.Password, user.PasswordHash);
            if (!IsPasswordValid) { 
                throw new InvalidCredentialsException();  
            }

            return _JwtGenerator.GenerateToken(user);

        }

    }
}
