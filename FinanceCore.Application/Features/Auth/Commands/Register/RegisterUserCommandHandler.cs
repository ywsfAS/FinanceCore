using FinanceCore.Application.Abstractions;
using FinanceCore.Domain.Users;
using FinanceCore.Domain.Exceptions;
using MediatR;
using FinanceCore.Domain.Common;

namespace FinanceCore.Application.Features.Auth.Commands.Register
{

    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, Guid>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _hasher;

        public RegisterUserCommandHandler(IUserRepository userRepository , IPasswordHasher hasher)
        {
            _userRepository = userRepository;
            _hasher = hasher;
        }

        public async Task<Guid> Handle(RegisterUserCommand command, CancellationToken cancellationToken)
        {
            var existingUser = await _userRepository.GetByEmailAsync(command.Email, cancellationToken);

            if (existingUser is not null)
                throw new DuplicateEmailException(command.Email);

            var HashedPassword =  _hasher.Hash(command.Password);

            var user = User.Create(
                command.Name,
                new Email(command.Email),
                HashedPassword);

            await _userRepository.AddAsync(user, cancellationToken);

            return user.Id;
        }
    }
}
