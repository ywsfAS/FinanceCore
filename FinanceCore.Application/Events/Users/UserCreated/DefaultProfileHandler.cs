using FinanceCore.Application.Abstractions;
using FinanceCore.Application.Features.Profiles.Commands.Create;
using FinanceCore.Domain.Enums;
using FinanceCore.Domain.Events.User;
using MediatR;
using FinanceCore.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Application.Events.Users.UserCreated
{
    public class DefaultProfileHandler : INotificationHandler<UserCreatedEvent>
    {
        private readonly IProfileRepository _profileRepository;
        private readonly IMediator _mediator;
        public DefaultProfileHandler(IProfileRepository profileRepository , IMediator mediator) { 
            _profileRepository = profileRepository;
            _mediator = mediator;
        }
        public async Task Handle(UserCreatedEvent notification , CancellationToken token)
        {
            var command = new CreateProfileCommand(notification.UserId,"New", "User","New User Bio","avatarUrl",EnCurrency.USD);
            var profile = Domain.Profile.Profile.Create(command.userId,command.firstName,command.lastName,command.bio,command.avatarUrl,command.curreny);

           await _profileRepository.AddAsync(profile,token);
            await DomainEventDispatcher.DispatchAsync(_mediator, profile,token);
        }
    }
}
