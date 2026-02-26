using FinanceCore.Application.DTOs;
using FinanceCore.Application.Features.Transactions.Commands.Create;
using FinanceCore.Application.Features.Transactions.Commands.Delete;
using FinanceCore.Application.Features.Transactions.Commands.Update;
using FinanceCore.Application.Features.Transactions.Queries.GetTransactionById;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.ExceptionServices;

namespace FinanceCore.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TransactionsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Create a new transaction
        /// </summary>
        [HttpPost("Create")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(TransactionDto),StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ValidationErrorDto), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateTransaction([FromBody] CreateTransactionCommand command)
        {
            var response = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetTransactionById), new { id = response.Id }, response);
        }

        /// <summary>
        /// Get transaction by ID
        /// </summary>
        [HttpGet("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(TransactionDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationErrorDto), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetTransactionById(Guid id)
        {
            var query = new GetTransactionByIdQuery(id);
            var transaction = await _mediator.Send(query);
            return Ok(transaction);
        }

        /// <summary>
        /// Get all transactions for an account
        /// </summary>
        [HttpGet("account/{accountId}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(TransactionDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationErrorDto), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetTransactionsByAccountId(Guid accountId)
        {
            var query = new GetTransactionByIdQuery(accountId);
            var transactions = await _mediator.Send(query);
            return Ok(transactions);
        }

        /// <summary>
        /// Update an existing transaction
        /// </summary>
        [HttpPut("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ValidationErrorDto), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> UpdateTransaction(Guid id, [FromBody] UpdateTransactionCommand command)
        {
            if (id != command.Id)
                return BadRequest("ID mismatch");

            await _mediator.Send(command);
            return NoContent();
        }

        /// <summary>
        /// Delete a transaction
        /// </summary>
        [HttpDelete("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ValidationErrorDto), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteTransaction(Guid id)
        {
            var command = new DeleteTransactionCommand(id);
            await _mediator.Send(command);
            return NoContent();
        }
    }
}