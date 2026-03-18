using FinanceCore.Application.Models;
using FinanceCore.Domain.Enums;
using FinanceCore.Domain.Profile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Infrastructure.Mappers
{
        public static class ProfileMapper
        {
            public static ProfileModel MapToModel(Profile profile)
            {
                return new ProfileModel
                {
                    UserId =  profile.UserId,
                    FirstName = profile.FirstName,
                    LastName = profile.LastName,
                    Bio = profile.Bio,
                    AvatarUrl = profile.AvatarUrl,
                    Currency = profile.Currency
                };
            }
            public static Profile MapToDomain(ProfileModel model)
            {
                return Profile.Create(
                    model.UserId,
                    model.FirstName,
                    model.LastName,
                    model.Bio,
                    model.AvatarUrl,
                    model.Currency
                );
            }
        }
}
