using FinanceCore.Application.Abstractions;
using MediatR;
using FinanceCore.Domain.Exceptions;
namespace FinanceCore.Application.Features.Profiles.Commands.Delete
{
    public class DeleteProfileHandler : IRequestHandler<DeleteProfileCommand>
    {
        private readonly IProfileRepository _profileRepository;
        public DeleteProfileHandler(IProfileRepository profileRepository) { 
            _profileRepository = profileRepository;
        }
        public async Task Handle(DeleteProfileCommand command , CancellationToken token)
        {
            var result = await _profileRepository.ExistsAsync(command.id);
            if (!result)
            {
                throw new ProfileException.ProfileNotFoundException(command.id);
            }
            await _profileRepository.DeleteAsync(command.id);
        }
    }
}
