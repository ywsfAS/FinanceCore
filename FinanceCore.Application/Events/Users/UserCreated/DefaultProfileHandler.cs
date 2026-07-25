using FinanceCore.Application.Abstractions;
using FinanceCore.Application.Features.Profiles.Commands.Create;
using FinanceCore.Domain.Enums;
using FinanceCore.Domain.Events.User;
using MediatR;
using Microsoft.Extensions.Logging;

namespace FinanceCore.Application.Events.Users.UserCreated
{
    public class DefaultProfileHandler : INotificationHandler<UserCreatedEvent>
    {
        private readonly IProfileRepository _profileRepository;
        private readonly IMediator _mediator;
        private readonly ILogger<DefaultProfileHandler> _logger;
        public DefaultProfileHandler(IProfileRepository profileRepository , IMediator mediator , ILogger<DefaultProfileHandler> logger) { 
            _profileRepository = profileRepository;
            _mediator = mediator;
            _logger = logger;
        }
        public async Task Handle(UserCreatedEvent notification , CancellationToken token)
        {
            var command = new CreateProfileCommand(notification.UserId,notification.Name,"AS","No Bio",EnCurrency.USD);
            var profile = Domain.Profile.Profile.Create(command.UserId,command.FirstName,command.LastName,command.Bio,"Not Selected",command.Curreny);
            try
            {
                await _profileRepository.AddAsync(profile,token);
                await DomainEventDispatcher.DispatchAsync(_mediator, profile,token);
            }
            catch(Exception ex)
            {
                _logger.LogCritical(ex, "Failed to create default profile for {email}", notification.Email);
                throw;
            }
        }
    }
}
