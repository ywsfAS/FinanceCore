using FinanceCore.API.Requests.Account;
using FinanceCore.API.Requests.ReccuringTransations;
using FinanceCore.Application.DTOs;
using FinanceCore.Application.DTOs.RecurringTransaction;
using FinanceCore.Application.Features.Accounts.Commands.Create;
using FinanceCore.Application.Features.Accounts.Commands.Delete;
using FinanceCore.Application.Features.Accounts.Commands.Update;
using FinanceCore.Application.Features.RecurringTransaction.Commands.Delete;
using FinanceCore.Application.Features.RecurringTransaction.Commands.Update;
using FinanceCore.Application.Features.RecurringTransactions.commands.Create;
using FinanceCore.Application.Features.RecurringTransactions.queries.GetRecurringById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FinanceCore.API.Controllers
{
    [ApiController]
    [Route("api/v1/recurring-transactions")]
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
        [ProducesResponseType(typeof(CreateRecurringTransactionDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ValidationErrorDto), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> CreateRecurringTransaction([FromBody] CreateRecurringTransactionRequest request)
        {
            var userId = GetUserId();
            var command = new CreateRecurringCommand(userId, request.accountId, request.categoryId, request.amount, request.type , request.period , request.interval , request.isActive , request.description , request.startDate , request.endDate );
            var reccuring = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetRecurringByIdQuery), new { id = reccuring.id}, reccuring);
        }

        /// <summary>
        /// Update an existing recurring transaction
        /// </summary>
        [HttpPut]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ValidationErrorDto), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> UpdateRecurringTransaction([FromBody] UpdateRecurringTransactionRequest request)
        {
            var userId = GetUserId();
            var command = new UpdateRecurringCommand(userId , request.Id , request.AccountId , request.CategoryId , request.Amount , request.Type , request.Period , request.Interval , request.Description , request.StartDate , request.EndDate , request.IsActive);
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
        [ProducesResponseType(typeof(CreateRecurringTransactionDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ValidationErrorDto), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetRecurringById(Guid id)
        {
            var userId = GetUserId();
            var query = new GetRecurringByIdQuery(userId, id); 
            var recurring = await _mediator.Send(query);
            return Ok(recurring);
        }

    }
}
