using FinanceCore.API.Requests.ContactMessage;
using FinanceCore.Application.DTOs;
using FinanceCore.Application.Features.Contact.Commands.Create;
using FinanceCore.Application.Features.Contact.Commands.Mark;
using FinanceCore.Domain.Common;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace FinanceCore.API.Controllers
{
    [EnableRateLimiting("Default")]
    [ApiController]
    [Authorize]
    [Route("api/v1/contacts")]
    public class ContactMessageController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ContactMessageController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Create a new contact message.
        /// </summary>
        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ValidationErrorDto), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateMessage([FromBody] CreateContactMessageRequest request)
        {
            var command = new CreateContactMessageCommand(
                request.FullName,
                new Email(request.Email),
                request.Subject,
                request.Message);

            await _mediator.Send(command);

            return StatusCode(StatusCodes.Status201Created);
        }

        /// <summary>
        /// Mark a contact message as seen.
        /// </summary>
        [Authorize(Roles = "Admin")]
        [HttpPatch("{id}/seen")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ValidationErrorDto), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> MarkMessage([FromRoute] Guid id)
        {
            await _mediator.Send(new MarkContactMessageCommand(id));

            return NoContent();
        }
    }
}