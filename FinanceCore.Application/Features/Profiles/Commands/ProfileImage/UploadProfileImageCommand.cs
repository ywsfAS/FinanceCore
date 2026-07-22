using MediatR;

namespace FinanceCore.Application.Features.Profiles.Commands.ProfileImage
{
    public sealed record UploadProfileImageCommand(Guid UserId , Stream FileStream , string FileName) : IRequest<string>;
}
