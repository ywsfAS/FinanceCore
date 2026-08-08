using Asp.Versioning;
using FinanceCore.API.Requests.ReccuringTransations;
using FinanceCore.Application.DTOs;
using FinanceCore.Application.DTOs.RecurringTransaction;
using FinanceCore.Application.Features.Recurring.Commands.Cancel;
using FinanceCore.Application.Features.Recurring.Commands.Create;
using FinanceCore.Application.Features.Recurring.Commands.Delete;
using FinanceCore.Application.Features.Recurring.Commands.Execute;
using FinanceCore.Application.Features.Recurring.Commands.Pause;
using FinanceCore.Application.Features.Recurring.Commands.Resume;
using FinanceCore.Application.Features.Recurring.Commands.Update;
using FinanceCore.Application.Features.Recurring.Queries.GetRecurring;
using FinanceCore.Application.Features.Recurring.Queries.GetRecurringById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using System.Security.Claims;

namespace FinanceCore.API.Controllers
{
    [EnableRateLimiting("Default")]
    [ApiController]
    [Route("api/v{version:apiVersion}/recurring-transactions")]
    [ApiVersion("1.0")]
    [Authorize]
    public class RecurringTransactionsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public RecurringTransactionsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        private Guid GetUserId()
        {
            return Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        }
        /// <summary>
        /// Create a new recurring transaction
        /// </summary>
        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(typeof(RecurringTransactionDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ValidationErrorDto), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> CreateRecurringTransaction([FromBody] CreateRecurringTransactionRequest request)
        {
            var userId = GetUserId();
            var command = new CreateRecurringCommand(userId, request.AccountId, request.CategoryId,request.Amount,request.Period ,request.Description ,request.ExecutionType, request.StartDate , request.EndDate );
            var reccuring = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetRecurringById), new { id = reccuring.Id }, reccuring);
        }

        /// <summary>
        /// Update an existing recurring transaction
        /// </summary>
        [HttpPut("{id:guid}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ValidationErrorDto), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> UpdateRecurringTransaction([FromRoute] Guid id,[FromBody] UpdateRecurringTransactionRequest request)
        {
            var userId = GetUserId();
            var command = new UpdateRecurringCommand(userId , id , request.AccountId , request.CategoryId , request.Amount , request.Period, request.ExecutionType , request.Description , request.StartDate , request.EndDate);
            await _mediator.Send(command);
            return NoContent();
        }
        /// <summary>
        /// Execute a due recurring transaction
        /// </summary>
        [HttpPut("{id:guid}/execute")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ValidationErrorDto), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> ExecuteRecurringTransaction([FromRoute] Guid id)
        {
            var userId = GetUserId();
            var command = new ExecuteCommand(userId,id);
            await _mediator.Send(command);
            return NoContent();
        }
        /// <summary>
        /// Pause recurring transaction
        /// </summary>
        [HttpPut("{id:guid}/pause")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ValidationErrorDto), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> PauseRecurringTransaction([FromRoute] Guid id)
        {
            var userId = GetUserId();
            var command = new PauseCommand(userId,id);
            await _mediator.Send(command);
            return NoContent();
        }
        /// <summary>
        /// Cancel recurring transaction
        /// </summary>
        [HttpPut("{id:guid}/cancel")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ValidationErrorDto), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> CancelRecurringTransaction([FromRoute] Guid id)
        {
            var userId = GetUserId();
            var command = new CancelCommand(userId,id);
            await _mediator.Send(command);
            return NoContent();
        }
        /// <summary>
        /// Resume recurring transaction
        /// </summary>
        [HttpPut("{id:guid}/resume")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ValidationErrorDto), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> ResumeRecurringTransaction([FromRoute] Guid id)
        {
            var userId = GetUserId();
            var command = new ResumeCommand(userId,id);
            await _mediator.Send(command);
            return NoContent();
        }
        /// <summary>
        /// Delete a recurring transaction
        /// </summary>
        [HttpDelete("{Id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ValidationErrorDto), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> DeleteRecurringTransaction(Guid Id)
        {
            var userId = GetUserId();
            var command = new DeleteRecurringCommand(userId, Id);
            await _mediator.Send(command);
            return NoContent();
        }
        /// <summary>
        /// Get recurring transaction by ID
        /// </summary>
        [HttpGet("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(RecurringTransactionDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ValidationErrorDto), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetRecurringById(Guid id)
        {
            var userId = GetUserId();
            var query = new GetRecurringByIdQuery(userId, id); 
            var recurring = await _mediator.Send(query);
            return Ok(recurring);
        }
        /// <summary>
        /// Get recurring transaction With Filters
        /// </summary>
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(typeof(IEnumerable<RecurringTransactionDto>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ValidationErrorDto), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetRecurringTransactions([FromQuery] GetRecurringFilteredRequest request)
        {
            var userId = GetUserId();
            var query = new GetRecurringQuery(userId,request.AccountId,request.CategoryId,request.Status,request.Period,request.Start,request.End,request.Page , request.PageSize);
            var recurring = await _mediator.Send(query);
            return Ok(recurring);
        }

    }
}
