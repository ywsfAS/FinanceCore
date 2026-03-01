using FinanceCore.Application.DTOs;
using FinanceCore.Application.DTOs.Transaction;
using FinanceCore.Application.Features.Accounts.Commands.Create;
using FinanceCore.Application.Features.Accounts.Commands.Delete;
using FinanceCore.Application.Features.Accounts.Commands.Update;
using FinanceCore.Application.Features.Accounts.Queries.GetAccountById;
using FinanceCore.Application.Features.Accounts.Queries.GetBalanceById;
using FinanceCore.Application.Features.Transactions.Queries.GetTansactionsByAccountId;
using FinanceCore.Application.Features.Transactions.Queries.GetTransactionById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FinanceCore.API.Controllers
{
    [ApiController]
    [Route("api/accounts")]
    public class AccountsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AccountsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Create a new account
        /// </summary>
        [HttpPost()]
        [Produces("application/json")]
        [ProducesResponseType(typeof(AccountDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ValidationErrorDto), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateAccount([FromBody] CreateAccountCommand command)
        {
            var account = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetAccountById), new { id = account.Id }, account);
        }

        /// <summary>
        /// Get account by ID
        /// </summary>
        [HttpGet("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(AccountDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ValidationErrorDto), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAccountById(Guid id)
        {
            var query = new GetAccountByIdQuery(id);
            var account = await _mediator.Send(query);
            return Ok(account);
        }

        /// <summary>
        /// Get all accounts for a user
        /// </summary>
        [HttpGet("users/{userId}/accounts")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(AccountDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ValidationErrorDto), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAccountsByUserId(Guid userId)
        {
            var query = new GetAccountByIdQuery(userId);
            var accounts = await _mediator.Send(query);
            return Ok(accounts);
        }
        /// <summary>
        /// Get account's Balance
        /// </summary>
        [HttpGet("{Id}/balance")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(AccountBalanceDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationErrorDto), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAccountsBalanceById(Guid userId)
        {
            var query = new GetBalanceByIdQuery(userId);
            var account = await _mediator.Send(query);
            return Ok(account);
        }
        /// <summary>
        /// Update an existing account
        /// </summary>
        [HttpPut("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ValidationErrorDto), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateAccount(Guid id, [FromBody] UpdateAccountCommand command)
        {
            if (id != command.Id)
                return BadRequest("ID mismatch");

            await _mediator.Send(command);
            return NoContent();
        }

        /// <summary>
        /// Delete an account
        /// </summary>
        [HttpDelete("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ValidationErrorDto), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeleteAccount(Guid id)
        {
            var command = new DeleteAccountCommand(id);
            await _mediator.Send(command);
            return NoContent();
        }
        /// <summary>
        /// Get all transactions for an account
        /// </summary>
        [HttpGet("{accountId}/transactions")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(IEnumerable<TransactionDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationErrorDto), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetTransactionsByAccountId(Guid accountId , int Page , int PageSize)
        {
            var query = new GetTransactionsByAccountIdQuery(accountId,Page,PageSize);
            var transactions = await _mediator.Send(query);
            return Ok(transactions);
        }
    }
}