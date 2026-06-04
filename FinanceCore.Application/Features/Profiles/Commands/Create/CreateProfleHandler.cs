using FinanceCore.Application.Abstractions;
using FinanceCore.Application.DTOs;
using FinanceCore.Domain.Profile;
using FluentValidation.Internal;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Application.Features.Profiles.Commands.Create
{
    public class CreateProfleHandler : IRequestHandler<CreateProfileCommand, ProfileDto>
    {
        private readonly IProfileRepository _profileRepository;
        public CreateProfleHandler(IProfileRepository profileRepository)
        {
            _profileRepository = profileRepository;
        }
        public async Task<ProfileDto> Handle(CreateProfileCommand command , CancellationToken token)
        {
            var profile = Profile.Create(command.UserId, command.FirstName, command.LastName, command.Bio, "Not Selected", command.Curreny);
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
