using FinanceCore.API.Requests.Profile;
using FinanceCore.Application.Abstractions;
using FinanceCore.Application.DTOs;
using FinanceCore.Application.Features.Profiles.Commands.Create;
using FinanceCore.Application.Features.Profiles.Commands.Delete;
using FinanceCore.Application.Features.Profiles.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FinanceCore.API.Controllers
{
    [ApiController]
    [Route("api/v1/profile")]
    [Authorize]
    public class ProfileController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IImageStorage _imageStorage;
        public ProfileController(IMediator mediator,IImageStorage imageStorage)
        {
            _mediator = mediator;
            _imageStorage = imageStorage;
        }
        private Guid GetUserId()
        {
            return Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        }
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ProfileDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ValidationErrorDto), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetProfileById()
        {
            var UserId = GetUserId();
            var query = new GetProfileByUserIdQuery(UserId);
            var profile = await _mediator.Send(query);
            return Ok(profile);
        }
        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ValidationErrorDto), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> CreateProfile(createProfileRequest request)
        {
            var UserId = GetUserId();
            var command = new CreateProfileCommand(UserId, request.firstName , request.lastName , request.bio , request.avatarUrl , request.curreny);
            var profile = await _mediator.Send(command);
            return Ok(profile);
        }

        [HttpPost("profile-image")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ValidationErrorDto), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> UploadProfileImage(IFormFile file)
        {
            var userId = GetUserId();
            if(file is null || file.Length == 0)
            {
                return BadRequest("Invalid file");
            }
            using var stream = file.OpenReadStream();
            var path = await _imageStorage.SaveImage(stream, file.FileName, userId);

            return Ok(new {path});
        }


        [HttpDelete]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ValidationErrorDto), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> DeleteProfile()
        {
            var UserId = GetUserId();
            var command = new DeleteProfileCommand(UserId);
            await _mediator.Send(command);
            return NoContent();
        }

    }
}
