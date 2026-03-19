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
            var result = await _profileRepository.ExistsAsync(command.profile.Id);
            if (!result)
            {
                throw new ProfileException.ProfileNotFoundException(command.profile.Id);
            }
            await _profileRepository.UpdateAsync(command.profile, token);
        }
    }
}
