using Asp.Versioning;
using FinanceCore.Application.DTOs;
using FinanceCore.Application.DTOs.Auth;
using FinanceCore.Application.Features.Auth.Commands.ForgotPassword;
using FinanceCore.Application.Features.Auth.Commands.Login;
using FinanceCore.Application.Features.Auth.Commands.Logout;
using FinanceCore.Application.Features.Auth.Commands.LogoutAll;
using FinanceCore.Application.Features.Auth.Commands.Refresh;
using FinanceCore.Application.Features.Auth.Commands.Register;
using FinanceCore.Application.Features.Auth.Commands.ResetPassword;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace FinanceCore.API.Controllers
{
    /// <summary>
    /// Controller for authentication operations including user registration and login
    /// </summary>
    [EnableRateLimiting("Auth")]
    [ApiController]
    [Route("api/v{version:apiVersion}/auth")]
    [ApiVersion("1.0")]
    [AllowAnonymous]
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

        /// <summary>
        /// Registers a new user in the system
        /// </summary>
        /// <param name="command">User registration details including name, email, password, and preferences</param>
        /// <returns>Returns the newly created user Info</returns>
        /// <response code="201">User registered successfully</response>
        /// <response code="400">Invalid input or email already exists</response>
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
        [HttpPost("login")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(LoginDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationErrorDto),StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Login([FromBody] LoginUserCommand command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpPost("forgot-password")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationErrorDto),StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPasswordCommand command)
        {
            await _mediator.Send(command);
            return Ok("Reset link sent successfuly");
        }
        [HttpPost("reset-password")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationErrorDto),StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPasswordCommand command)
        {
            await _mediator.Send(command);
            return Ok("Password reset successful");
        }

        [HttpPost("refresh")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationErrorDto),StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Refresh([FromBody] RefreshCommand command)
        {
            await _mediator.Send(command);
            return Ok("Refresh token successfully");
        }

        [HttpPost("logout")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationErrorDto),StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Logout([FromBody] LogoutCommand command)
        {
            await _mediator.Send(command);
            return Ok("Logout successfully");
        }
        [HttpPost("logout-all")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationErrorDto),StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> LogoutAll([FromBody] LogoutAllCommand command)
        {
            await _mediator.Send(command);
            return Ok("Logout all successfully");
        }
    }
}