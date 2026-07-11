using FinanceCore.API.Requests.Profile;
using FinanceCore.Application.DTOs;
using FinanceCore.Application.Features.Profiles.Commands.Create;
using FinanceCore.Application.Features.Profiles.Commands.Delete;
using FinanceCore.Application.Features.Profiles.Commands.ProfileImage;
using FinanceCore.Application.Features.Profiles.Commands.Update;
using FinanceCore.Application.Features.Profiles.Queries;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using System.Security.Claims;

namespace FinanceCore.API.Controllers
{

    [EnableRateLimiting("Default")]
    [ApiController]
    [Route("api/v1/profile")]
    [Authorize]
    public class ProfileController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ProfileController(IMediator mediator)
        {
            _mediator = mediator;
        }

        private Guid GetUserId()
        {
            return Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        }

        /// <summary>
        /// Get the current user's profile.
        /// </summary>
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ProfileDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationErrorDto), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetProfile()
        {
            var userId = GetUserId();
            var query = new GetProfileByUserIdQuery(userId);
            var profile = await _mediator.Send(query);

            return Ok(profile);
        }

        /// <summary>
        /// Create a profile for the current user.
        /// </summary>
        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(typeof(ProfileDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ValidationErrorDto), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> CreateProfile(
            [FromBody] CreateProfileRequest request)
        {
            var userId = GetUserId();
            var command = new CreateProfileCommand(userId, request.FirstName, request.LastName, request.Bio, request.Currency);
            var profile = await _mediator.Send(command);

            return CreatedAtAction(
                nameof(GetProfile),
                null,
                profile);
        }

        /// <summary>
        /// Update the current user's profile.
        /// </summary>
        [HttpPut]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ValidationErrorDto), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> UpdateProfile(
            [FromBody] UpdateProfileRequest request)
        {
            var userId = GetUserId();
            var command = new UpdateProfileCommand(userId, request.FirstName, request.LastName, request.Bio, request.Currency);
            await _mediator.Send(command);

            return NoContent();
        }

        /// <summary>
        /// Upload or replace the profile image.
        /// </summary>
        [HttpPut("image")]
        [Consumes("multipart/form-data")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> UploadProfileImage(
            [FromForm] UploadProfileImageRequest request)
        {
            var file = request.File;
            if (file is null || file.Length == 0)
                return BadRequest("Invalid file.");

            await using var stream = file.OpenReadStream();
            var command = new UploadProfileImageCommand(GetUserId(), stream, file.FileName);
            var path = await _mediator.Send(command);
            return Ok(new { path });
        }

        /// <summary>
        /// Delete the current user's profile.
        /// </summary>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ValidationErrorDto), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> DeleteProfile()
        {
            var userId = GetUserId();
            var command = new DeleteProfileCommand(userId);
            await _mediator.Send(command);

            return NoContent();
        }
    }
}