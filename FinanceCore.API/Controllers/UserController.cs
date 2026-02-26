using FinanceCore.Application.DTOs;
using FinanceCore.Application.Features.Users.Command.Delete;
using FinanceCore.Application.Features.Users.Command.Update;
using FinanceCore.Application.Features.Users.Queries.GetUserById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FinanceCore.API.Controllers
{
    /// <summary>
    /// Controller for managing user profile operations
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
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

        /// <summary>
        /// Retrieves a user by their unique identifier
        /// </summary>
        /// <param name="id">The unique identifier of the user</param>
        /// <returns>Returns the user details including name, email, and preferences</returns>
        /// <response code="200">User found and returned successfully</response>
        /// <response code="404">User not found</response>
        [HttpGet("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationErrorDto),StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetUserById(Guid id)
        {
            var query = new GetUserByIdQuery(id);
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
        [HttpPut("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ValidationErrorDto),StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateUser(Guid id, [FromBody] UpdateUserCommand command)
        {
            if (id != command.Id)
                return BadRequest("ID mismatch");

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
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            var command = new DeleteUserCommand(id);
            await _mediator.Send(command);
            return NoContent();
        }
    }
}