using FinanceCore.Application.Abstractions;
using FinanceCore.Application.Events;
using FinanceCore.Domain.Exceptions; 
using MediatR;

namespace FinanceCore.Application.Features.Profiles.Commands.ProfileImage
{
    public class UploadProfileImageHandler : IRequestHandler<UploadProfileImageCommand, string>
    {
        private readonly IImageStorage _imageStorage;
        private readonly IProfileRepository _profileRepository;
        private readonly IMediator _eventBus;
        private readonly IImageProcessor _imageProcessor;

        public UploadProfileImageHandler(
            IMediator eventBus,
            IImageStorage imageStorage, IProfileRepository profileRepository, IImageProcessor imageProcessor)
        {
            _imageStorage = imageStorage;
            _profileRepository = profileRepository;
            _eventBus = eventBus;
            _imageProcessor = imageProcessor;
        }

        public async Task<string> Handle(UploadProfileImageCommand request, CancellationToken token)
        {
            var IsprofileExist = await _profileRepository.ExistsByUserIdAsync(request.UserId);

            if (!IsprofileExist)
            {
                throw new ProfileException.ProfileNotFoundException(request.UserId);
            }

            var image = await _imageProcessor.ProcessAsync(request.FileStream, token);
            var profile = await _profileRepository.GetProfileByUserIdAsync(request.UserId);

            var oldPath = profile.AvatarUrl;

            var path = await _imageStorage.SaveAsync(
               image,
               token);
            profile.UpdateAvatar(path);

            try
            {
                await _profileRepository.UpdateAsync(profile , token);
            }
            catch
            {
                await _imageStorage.DeleteAsync(path,token);
                throw;
            }

            if (!string.IsNullOrEmpty(oldPath))
            {
                await _imageStorage.DeleteAsync(oldPath,token);
            }

            await DomainEventDispatcher.DispatchAsync(_eventBus,profile, token);
            return path;
        }
    }
}
