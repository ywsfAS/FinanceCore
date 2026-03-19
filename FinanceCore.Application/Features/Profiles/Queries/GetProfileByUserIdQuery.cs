using FinanceCore.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Application.Features.Profiles.Queries
{
    public record GetProfileByUserIdQuery(Guid userId) : IRequest<ProfileDto?>;
}
