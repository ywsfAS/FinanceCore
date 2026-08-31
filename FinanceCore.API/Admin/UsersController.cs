using Asp.Versioning;
using FinanceCore.Application.DTOs;
using FinanceCore.Application.Features.Users.Command.AssignRole;
using FinanceCore.Application.Features.Users.Command.Lock;
using FinanceCore.Application.Features.Users.Command.Unlock;
using FinanceCore.Application.Features.Users.Queries.GetFilteredUsers;
using FinanceCore.Application.Features.Users.Queries.GetUserById;
using FinanceCore.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinanceCore.API.Admin
{
    [ApiController]
    [Route("api/v{version:apiVersion}/admin/users")]
    [ApiVersion("1.0")]
    [Authorize(Roles = "Admin")]
    public class UsersController : ControllerBase
    {
        private readonly IMediator _mediator;
        public UsersController(IMediator mediator) {
            _mediator = mediator;
        }
        [HttpGet("{userId}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(UserDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationErrorDto), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetUserById([FromRoute] Guid userId)
        {
            var query = new GetUserByIdQuery(userId);
            var result = await _mediator.Send(query);   
            return Ok(result);
        }
        [HttpPatch("{userId}/lock")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationErrorDto), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> LockUser([FromRoute] Guid userId , [FromBody] DateTime until)
        {
            var command = new LockUserCommand(userId,until);
             await _mediator.Send(command);   
            return Ok($"[User {userId}] is locked until {until} ");
        }

        [HttpPatch("{userId}/unlock")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationErrorDto), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> UnLockUser([FromRoute] Guid userId )
        {
            var command = new UnlockUserCommand(userId); 
             await _mediator.Send(command);   
            return Ok($"[User {userId}] is unlocked ");
        }

        [HttpPatch("{userId}/role")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationErrorDto), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> AssignRoleUser([FromRoute] Guid userId , [FromBody] UserRole role)
        {
            var command = new AssignRoleCommand(userId, role); 
             await _mediator.Send(command);   
            return Ok($"[User {userId}] is assigned to role {role.ToString()}");
        }

        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(typeof(PagedResult<UserDto>),StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationErrorDto), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetUsers([FromQuery] GetUsersQuery query)
        {
            var result = await _mediator.Send(query);   
            return Ok(result);
        }



    }
}
