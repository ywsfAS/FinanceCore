using FinanceCore.Application.Abstractions;
using MediatR;
using FinanceCore.Domain.Exceptions;
namespace FinanceCore.Application.Features.Users.Command.Delete
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand>
    {
        private readonly IUserRepository _userRepository;

        public DeleteUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task Handle(DeleteUserCommand command, CancellationToken cancellationToken)
        {
            var user = await _userRepository.IsExists(command.Id, cancellationToken);

            if (!user)
                throw new UserNotFoundException(command.Id);

            await _userRepository.DeleteAsync(command.Id, cancellationToken);
        }
    }
}
