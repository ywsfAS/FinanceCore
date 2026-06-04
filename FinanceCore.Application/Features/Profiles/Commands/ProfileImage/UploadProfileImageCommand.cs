using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Application.Features.Profiles.Commands.ProfileImage
{
    public sealed record UploadProfileImageCommand(Guid UserId , Stream FileStream , string FileName) : IRequest<string>;
}
