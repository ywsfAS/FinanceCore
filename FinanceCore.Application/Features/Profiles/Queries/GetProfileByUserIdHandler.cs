using FinanceCore.Application.Abstractions;
using FinanceCore.Application.DTOs;
using MediatR;


namespace FinanceCore.Application.Features.Profiles.Queries
{
    public class GetProfileByUserIdHandler : IRequestHandler<GetProfileByUserIdQuery, ProfileDto?>
    {
        private readonly IProfileRepository _profileRepository;
        public GetProfileByUserIdHandler(IProfileRepository profileRepository)
        {
            _profileRepository = profileRepository;
        }
        public async Task<ProfileDto?> Handle(GetProfileByUserIdQuery query , CancellationToken token)
        {
            var profile = await _profileRepository.GetProfileByUserIdAsync(query.userId);
            if (profile is null) { 
                return null;
            }
            return new ProfileDto
            {
                UserId = profile.UserId,
                LastName = profile.LastName,
                FirstName = profile.FirstName,
                Currency = profile.Currency,
                Bio = profile.Bio,
                AvatarUrl = profile.AvatarUrl,
            };
        }
    }

}

