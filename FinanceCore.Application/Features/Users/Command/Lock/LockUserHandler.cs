using FinanceCore.Domain.Exceptions;
using FinanceCore.Application.Abstractions;
using MediatR;

namespace FinanceCore.Application.Features.Users.Command.Lock
{
    public class LockUserHandler : IRequestHandler<LockUserCommand>
    {
        private readonly IUserRepository _userRepository;
        public LockUserHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task Handle(LockUserCommand command , CancellationToken token)
        {
            var user = await _userRepository.IsExistsAsync(command.UserId);
            if (!user) throw new UserNotFoundException(command.UserId);
            await _userRepository.UpdateLoginSecurityStateAsync(command.UserId,0,command.LockedUntil,token);
        }
    }
}
