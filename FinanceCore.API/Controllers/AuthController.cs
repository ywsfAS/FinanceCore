using Asp.Versioning;
using FinanceCore.API.Requests;
using FinanceCore.Application.DTOs;
using FinanceCore.Application.DTOs.Auth;
using FinanceCore.Application.Features.Auth.Commands.ForgotPassword;
using FinanceCore.Application.Features.Auth.Commands.Login;
using FinanceCore.Application.Features.Auth.Commands.Logout;
using FinanceCore.Application.Features.Auth.Commands.LogoutAll;
using FinanceCore.Application.Features.Auth.Commands.Refresh;
using FinanceCore.Application.Features.Auth.Commands.Register;
using FinanceCore.Application.Features.Auth.Commands.ResetPassword;
using FinanceCore.Application.Features.Auth.Queries.LoginHistory;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using System.Security.Claims;

namespace FinanceCore.API.Controllers
{
    /// <summary>
    /// Controller for authentication operations including user registration and login
    /// </summary>
    [EnableRateLimiting("Auth")]
    [ApiController]
    [Route("api/v{version:apiVersion}/auth")]
    [ApiVersion("1.0")]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        /// <summary>
        /// Initializes a new instance of the AuthController
        /// </summary>
        /// <param name="mediator">MediatR instance for sending commands</param>
        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        private Guid GetUserId()
        {
            return Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        }
        /// <summary>
        /// Registers a new user in the system
        /// </summary>
        /// <param name="command">User registration details including name, email, password, and preferences</param>
        /// <returns>Returns the newly created user Info</returns>
        /// <response code="201">User registered successfully</response>
        /// <response code="400">Invalid input or email already exists</response>
        [AllowAnonymous]
        [HttpPost("register")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(RegisterDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ValidationErrorDto),StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register([FromBody] RegisterUserCommand command)
        {
            var response = await _mediator.Send(command);

            var url = Url.Action(
                action : "GetByUserId",
                controller: "Users",
                values: new {Id = response.Id}
                
                );
            return Created(url,response);
        }

        /// <summary>
        /// Authenticates a user and generates a JWT token
        /// </summary>
        /// <param name="command">Login credentials including email and password</param>
        /// <returns>Returns a JWT token for authenticated requests</returns>
        /// <response code="200">Login successful, JWT token returned</response>
        /// <response code="400">Invalid credentials</response>
        [AllowAnonymous]
        [HttpPost("login")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(LoginDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationErrorDto),StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Login([FromBody] LoginUserCommand command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [AllowAnonymous]
        [HttpPost("forgot-password")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationErrorDto),StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordCommand command)
        {
            await _mediator.Send(command);
            return Ok("Reset link sent successfuly");
        }
        [AllowAnonymous]
        [HttpPost("reset-password")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationErrorDto),StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordCommand command)
        {
            await _mediator.Send(command);
            return Ok("Password reset successful");
        }

        [AllowAnonymous]
        [HttpPost("refresh")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationErrorDto),StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Refresh([FromBody] RefreshCommand command)
        {
            await _mediator.Send(command);
            return Ok("Refresh token successfully");
        }

        [AllowAnonymous]
        [HttpPost("logout")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationErrorDto),StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Logout([FromBody] LogoutCommand command)
        {
            await _mediator.Send(command);
            return Ok("Logout successfully");
        }
        [AllowAnonymous]
        [HttpPost("logout-all")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationErrorDto),StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> LogoutAll([FromBody] LogoutAllCommand command)
        {
            await _mediator.Send(command);
            return Ok("Logout all successfully");
        }
        [Authorize]
        [HttpGet("login-history")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(PagedResult<LoginHistoryDto>),StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationErrorDto),StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> LoginHistory([FromQuery] GetLoginHistoryRequest req)
        {
            var userId = GetUserId();
            var query = new GetLoginHistoryQuery(userId,req.Status,req.Search,req.From,req.To,req.Page,req.PageSize);
            var result = await _mediator.Send(query);
            return Ok(result);
        }
    }
}