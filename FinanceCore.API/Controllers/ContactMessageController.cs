using FinanceCore.API.Requests.Account;
using FinanceCore.API.Requests.ContactMessage;
using FinanceCore.Application.DTOs;
using FinanceCore.Application.Features.Accounts.Commands.Create;
using FinanceCore.Application.Features.Contact.Commands.Create;
using FinanceCore.Application.Features.Contact.Commands.Mark;
using FinanceCore.Domain.Common;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FinanceCore.API.Controllers
{
    [ApiController]
    [Route("api/v1/contacts")]
    public class ContactMessageController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ContactMessageController(IMediator mediator)
        {
           _mediator = mediator;
        }
        /// <summary>
        /// Create a new contact Message
        /// </summary>
        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ValidationErrorDto), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> CreateMessage([FromBody] CreateContactMessageRequest request)
        {
            var email = new Email(request.Email);
            var command = new CreateContactMessageCommand(request.FullName,email,request.Subject,request.Message);
            await _mediator.Send(command);
            return Ok("Contact message revcieved!");
        }

        /// <summary>
        /// Mark contact Message as Seen
        /// </summary>
        [HttpPost("mark/{msgId}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ValidationErrorDto), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> MarkMessage(Guid msgId)
        {
            var command = new MarkContactMessageCommand(msgId);
            await _mediator.Send(command);
            return Ok("Contact message is Marked!");
        }
    }
}
