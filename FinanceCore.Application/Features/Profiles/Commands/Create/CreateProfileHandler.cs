using FinanceCore.Application.Abstractions;
using FinanceCore.Application.DTOs;
using FinanceCore.Application.Events;
using FinanceCore.Domain.Profile;
using MediatR;

namespace FinanceCore.Application.Features.Profiles.Commands.Create
{
    public class CreateProfileHandler : IRequestHandler<CreateProfileCommand, ProfileDto>
    {
        private readonly IProfileRepository _profileRepository;
        private readonly IMediator _eventBus;
        public CreateProfileHandler(IProfileRepository profileRepository , IMediator eventBus)
        {
            _profileRepository = profileRepository;
            _eventBus = eventBus;
        }
        public async Task<ProfileDto> Handle(CreateProfileCommand command , CancellationToken token)
        {
            var profile = Profile.Create(command.UserId, command.FirstName, command.LastName, command.Bio, "Not Selected", command.Curreny);
            
            await DomainEventDispatcher.DispatchAsync(_eventBus, profile,token);
            await _profileRepository.AddAsync(profile,token);
            return new ProfileDto
            {
                UserId = profile.UserId,
                FirstName = profile.FirstName,
                LastName = profile.LastName,
                Bio = profile.Bio,
                AvatarUrl = profile.AvatarUrl,
                Currency = profile.Currency,
            };

        }

    }
}
