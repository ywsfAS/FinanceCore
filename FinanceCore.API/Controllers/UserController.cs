using Asp.Versioning;
using FinanceCore.API.Requests.User;
using FinanceCore.Application.DTOs;
using FinanceCore.Application.Features.Users.Command.Delete;
using FinanceCore.Application.Features.Users.Command.Update;
using FinanceCore.Application.Features.Users.Queries.GetUserById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using System.Security.Claims;

namespace FinanceCore.API.Controllers
{
    /// <summary>
    /// Controller for managing user profile operations
    /// </summary>
    [EnableRateLimiting("Default")]
    [ApiController]
    [Route("api/v{version:apiVersion}/users")]
    [ApiVersion("1.0")]
    [Authorize]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;

        /// <summary>
        /// Initializes a new instance of the UsersController
        /// </summary>
        /// <param name="mediator">MediatR instance for sending commands and queries</param>
        public UsersController(IMediator mediator)
        {
            _mediator = mediator;
        }
        private Guid GetUserId()
        {
            return Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        }
        /// <summary>
        /// Retrieves a user by their unique identifier
        /// </summary>
        /// <param name="id">The unique identifier of the user</param>
        /// <returns>Returns the user details including name, email, and preferences</returns>
        /// <response code="200">User found and returned successfully</response>
        /// <response code="404">User not found</response>
        [HttpGet("me")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationErrorDto),StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetCurrentUser()
        {
            var UserId = GetUserId();
            var query = new GetUserByIdQuery(UserId);
            var user = await _mediator.Send(query);
            return Ok(user);
        }

        /// <summary>
        /// Updates an existing user's profile information
        /// </summary>
        /// <param name="id">The unique identifier of the user to update</param>
        /// <param name="command">Updated user details including name, currency, and timezone</param>
        /// <returns>No content on successful update</returns>
        /// <response code="204">User updated successfully</response>
        /// <response code="400">Invalid input or ID mismatch</response>
        /// <response code="404">User not found</response>
        [HttpPut("me")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ValidationErrorDto),StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> UpdateUser([FromBody] UpdateUserRequest request)
        {
            var UserId = GetUserId();
            var command = new UpdateUserCommand(UserId,request.Name,request.TimeZone);
            await _mediator.Send(command);
            return NoContent();
        }

        /// <summary>
        /// Deletes a user from the system
        /// </summary>
        /// <param name="id">The unique identifier of the user to delete</param>
        /// <returns>No content on successful deletion</returns>
        /// <response code="204">User deleted successfully</response>
        /// <response code="404">User not found</response>
        [HttpDelete("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ValidationErrorDto),StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> DeleteUser()
        {
            var UserId = GetUserId();
            var command = new DeleteUserCommand(UserId);
            await _mediator.Send(command);
            return NoContent();
        }






    }
}