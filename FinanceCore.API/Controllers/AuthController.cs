using FinanceCore.Application.Features.Auth.Commands.Register;
using FinanceCore.Application.Features.Auth.Commands.Login;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FinanceCore.API.Controllers
{
    /// <summary>
    /// Controller for authentication operations including user registration and login
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
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
        /// <returns>Returns the newly created user ID and success message</returns>
        /// <response code="200">User registered successfully</response>
        /// <response code="400">Invalid input or email already exists</response>
        [HttpPost("register")]
        [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Register([FromBody] RegisterUserCommand command)
        {
            var userId = await _mediator.Send(command);
            return Ok(new { UserId = userId, Message = "User registered successfully" });
        }

        /// <summary>
        /// Authenticates a user and generates a JWT token
        /// </summary>
        /// <param name="command">Login credentials including email and password</param>
        /// <returns>Returns a JWT token for authenticated requests</returns>
        /// <response code="200">Login successful, JWT token returned</response>
        /// <response code="401">Invalid credentials</response>
        [HttpPost("login")]
        [ProducesResponseType(typeof(object), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> Login([FromBody] LoginUserCommand command)
        {
            var token = await _mediator.Send(command);
            return Ok(new { Token = token });
        }
    }
}