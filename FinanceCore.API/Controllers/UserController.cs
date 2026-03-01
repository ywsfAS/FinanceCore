using FinanceCore.Application.DTOs;
using FinanceCore.Application.Features.Accounts.Queries.GetAccountById;
using FinanceCore.Application.Features.Accounts.Queries.GetAccountsByUserId;
using FinanceCore.Application.Features.Budgets.Queries.GetBudgetById;
using FinanceCore.Application.Features.Budgets.Queries.GetBudgetsByUserId;
using FinanceCore.Application.Features.Categories.Queries.GetCategoriesByUserId;
using FinanceCore.Application.Features.Users.Command.Delete;
using FinanceCore.Application.Features.Users.Command.Update;
using FinanceCore.Application.Features.Users.Queries.GetUserById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FinanceCore.API.Controllers
{
    /// <summary>
    /// Controller for managing user profile operations
    /// </summary>
    [ApiController]
    [Route("api/v1/users")]
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
        [HttpGet("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationErrorDto),StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetUserById(Guid id)
        {
            var UserId = GetUserId();
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
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> UpdateUser(Guid id, [FromBody] UpdateUserCommand command)
        {
            var UserId = GetUserId();
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
        public async Task<IActionResult> DeleteUser(Guid id)
        {
            var UserId = GetUserId();
            var command = new DeleteUserCommand(id);
            await _mediator.Send(command);
            return NoContent();
        }
        /// <summary>
        /// Get all budgets for a user
        /// </summary>
        [HttpGet("{userId}/budgets")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(IEnumerable<BudgetDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationErrorDto), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetBudgetsByUserId()
        {
            var UserId = GetUserId();
            var query = new GetBudgetsByUserIdQuery(UserId);
            var budgets = await _mediator.Send(query);
            return Ok(budgets);
        }
        /// <summary>
        /// Get all categories for a user
        /// </summary>
        [HttpGet("{userId}/categories")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(CategoryDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationErrorDto), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetCategoriesByUserId(Guid userId,int Page = 1 , int PageSize = 10 )
        {
            var UserId = GetUserId();
            var query = new GetCategoriesByUserIdQuery(userId,Page ,PageSize);
            var categories = await _mediator.Send(query);
            return Ok(categories);
        }

        /// <summary>
        /// Get all accounts for a user
        /// </summary>
        [HttpGet("{userId}/accounts")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(AccountDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ValidationErrorDto), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetAccountsByUserId(Guid userId)
        {
            var UserId = GetUserId();
            var query = new GetAccountsByUserIdQuery(userId);
            var accounts = await _mediator.Send(query);
            return Ok(accounts);
        }
    }
}