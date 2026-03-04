using FinanceCore.Application.Abstractions;
using FinanceCore.Domain.Users;
using FinanceCore.Domain.Exceptions;
using MediatR;
using FinanceCore.Domain.Common;
using System.ComponentModel.DataAnnotations;
using FinanceCore.Application.DTOs.Auth;
using FinanceCore.Application.Events;

namespace FinanceCore.Application.Features.Auth.Commands.Register
{

    public class RegisterUserCommandHandler : IRequestHandler<RegisterUserCommand, RegisterDto>
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _hasher;
        private readonly IMediator _eventBus;

        public RegisterUserCommandHandler(IUserRepository userRepository , IPasswordHasher hasher, IMediator eventBus)
        {
            _userRepository = userRepository;
            _hasher = hasher;
            _eventBus = eventBus;
        }

        public async Task<RegisterDto> Handle(RegisterUserCommand command, CancellationToken cancellationToken)
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
            await DomainEventDispatcher.DispatchAsync(_eventBus,user,cancellationToken);
            var Response = new RegisterDto(user.Id, user.Name, user.Email.Address);
            return Response;
        }
    }
}
