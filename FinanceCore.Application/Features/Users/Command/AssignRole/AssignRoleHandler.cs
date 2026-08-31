using FinanceCore.Application.Abstractions;
using FinanceCore.Domain.Exceptions;
using MediatR;

namespace FinanceCore.Application.Features.Users.Command.AssignRole
{
    public class AssignRoleHandler : IRequestHandler<AssignRoleCommand>
    {
        private readonly IUserRepository _userRepository;
        public AssignRoleHandler(IUserRepository userRepository) {
            _userRepository = userRepository;
        }
        public async Task Handle(AssignRoleCommand command , CancellationToken token)
        {
            var user = await _userRepository.GetByIdAsync(command.UserId);
            if (user is null) throw new UserNotFoundException(command.UserId);
            user.AssignRole(command.Role);
            await _userRepository.UpdateAsync(user);
        }
    }
}
