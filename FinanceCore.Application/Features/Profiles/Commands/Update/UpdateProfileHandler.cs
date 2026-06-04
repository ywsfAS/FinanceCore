using FinanceCore.Application.Abstractions;
using FinanceCore.Domain.Profile;
using MediatR;
using FinanceCore.Domain.Exceptions;
namespace FinanceCore.Application.Features.Profiles.Commands.Update
{
    public class UpdateProfileHandler : IRequestHandler<UpdateProfileCommand>
    {
        private readonly IProfileRepository _profileRepository;
        public UpdateProfileHandler(IProfileRepository profileRepository)
        {
            _profileRepository = profileRepository;
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
            profile!.ChangeCurrency(command.Currency);
            profile.UpdateName(command.FirstName, command.LastName);
            profile.UpdateBio(command.Bio);

            await _profileRepository.UpdateAsync(profile, token);
        }
    }
}
