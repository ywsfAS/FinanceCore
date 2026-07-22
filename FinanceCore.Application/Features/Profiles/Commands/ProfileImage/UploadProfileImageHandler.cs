using FinanceCore.Application.Abstractions;
using MediatR;
using FinanceCore.Domain.Exceptions;
using FinanceCore.Application.Events;

namespace FinanceCore.Application.Features.Profiles.Commands.ProfileImage
{
    public class UploadProfileImageHandler : IRequestHandler<UploadProfileImageCommand, string>
    {
        private readonly IImageStorage _imageStorage;
        private readonly IProfileRepository _profileRepository;
        private readonly IMediator _eventBus;

        public UploadProfileImageHandler(
            IMediator eventBus,
            IImageStorage imageStorage, IProfileRepository profileRepository)
        {
            _imageStorage = imageStorage;
            _profileRepository = profileRepository;
            _eventBus = eventBus;
        }

        public async Task<string> Handle(UploadProfileImageCommand request, CancellationToken token)
        {
            var IsprofileExist = await _profileRepository.ExistsByUserIdAsync(request.UserId);
            if (!IsprofileExist)
            {
                throw new ProfileException.ProfileNotFoundException(request.UserId);
            }
            var profile = await _profileRepository.GetProfileByUserIdAsync(request.UserId);
            var path = await _imageStorage.SaveImage(
               request.FileStream,
               request.FileName,
               request.UserId);
            profile.UpdateAvatar(path);
            await DomainEventDispatcher.DispatchAsync(_eventBus,profile, token);
            await _profileRepository.UpdateAsync(profile , token);
            return path;
        }
    }
}
