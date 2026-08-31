using FinanceCore.Application.Abstractions;
using FinanceCore.Domain.Exceptions;
using MediatR;

namespace FinanceCore.Application.Features.Users.Command.Unlock
{
    public class UnlockUserHandler : IRequestHandler<UnlockUserCommand> 
    {
        private readonly IUserRepository _userRepository;
        public UnlockUserHandler(IUserRepository userRepository) { 
            _userRepository = userRepository;
        }

        public async Task Handle(UnlockUserCommand command , CancellationToken token)
        {
            var user = await _userRepository.GetByIdAsync(command.UserId);
            if (user is null) throw new UserNotFoundException(command.UserId);
            user.ResetLoginAttempts();
            await _userRepository.UpdateAsync(user);
        }
    }
}
