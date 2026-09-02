using FinanceCore.Application.Abstractions;
using FinanceCore.Domain.Profile;
using MediatR;
using FinanceCore.Domain.Exceptions;
using FinanceCore.Application.Events;
namespace FinanceCore.Application.Features.Profiles.Commands.Update
{
    public class UpdateProfileHandler : IRequestHandler<UpdateProfileCommand>
    {
        private readonly IProfileRepository _profileRepository;
        private readonly IMediator _eventBus;
        public UpdateProfileHandler(IProfileRepository profileRepository , IMediator eventBus)
        {
            _profileRepository = profileRepository;
            _eventBus = eventBus;
        }
        public async Task Handle(UpdateProfileCommand command , CancellationToken token)
        {
            var result = await _profileRepository.ExistsByUserIdAsync(command.UserId);
            if (!result)
            {
                throw new ProfileException.ProfileNotFoundException(command.UserId);
            }
            var profile = await _profileRepository.GetProfileByUserIdAsync(command.UserId);
            // update profile infos
            if (command.Currency.HasValue) profile!.ChangeCurrency(command.Currency.Value);
            if (command.FirstName != null) profile!.UpdateFirstName(command.FirstName);
            if (command.LastName != null) profile!.UpdateLasttName(command.LastName);
            if (command.Bio != null) profile.UpdateBio(command.Bio); 
            await DomainEventDispatcher.DispatchAsync(_eventBus, profile ,token);
            await _profileRepository.UpdateAsync(profile, token);
        }
    }
}
