using FinanceCore.Application.Abstractions;
using FinanceCore.Application.DTOs;
using FinanceCore.Domain.Profile;
using FluentValidation.Internal;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
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
            var profile = Profile.Create(command.userId , command.firstName , command.lastName , command.bio , command.avatarUrl , command.curreny );
            await _profileRepository.AddAsync(profile,token);
            return new ProfileDto
            {
                UserId = command.userId,
                FirstName = command.firstName,
                LastName = command.lastName,
                Bio = command.bio,
                AvatarUrl = command.avatarUrl,
                Currency = command.curreny,
            };

        }

    }
}
