using FinanceCore.API.Requests.Account;
using FinanceCore.Application.DTOs;
using FinanceCore.Application.DTOs.Transaction;
using FinanceCore.Application.Features.Accounts.Commands.Create;
using FinanceCore.Application.Features.Accounts.Commands.Delete;
using FinanceCore.Application.Features.Accounts.Commands.Update;
using FinanceCore.Application.Features.Accounts.Queries.GetAccountById;
using FinanceCore.Application.Features.Accounts.Queries.GetBalanceById;
using FinanceCore.Application.Features.Transactions.Queries.GetTansactionsByAccountId;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FinanceCore.API.Controllers
{
    [ApiController]
    [Route("api/v1/accounts")]
    [Authorize]
    public class AccountsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AccountsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        private Guid GetUserId()
        {
            return Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        }

        /// <summary>
        /// Create a new account
        /// </summary>
        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(typeof(AccountDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ValidationErrorDto), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> CreateAccount([FromBody] CreateAccountRequest request)
        {
            var userId = GetUserId();
            var command = new CreateAccountCommand(userId,request.Name,request.Type,request.Currency,request.InitialBalance);
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
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetAccountById(Guid id)
        {
            var userId = GetUserId();
            var query = new GetAccountByIdQuery(userId,id); // Should return only Accounts of UserId
            var account = await _mediator.Send(query);
            return Ok(account);
        }

        /// <summary>
        /// Get account's Balance
        /// </summary>
        [HttpGet("balance")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(AccountBalanceDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationErrorDto), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetAccountsBalanceById(Guid AccountId)
        {
            var userId = GetUserId();
            var query = new GetBalanceByIdQuery(userId,AccountId);
            var account = await _mediator.Send(query);
            return Ok(account);
        }
        /// <summary>
        /// Update an existing account
        /// </summary>
        [HttpPut]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ValidationErrorDto), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> UpdateAccount([FromBody] UpdateAccountRequest request)
        {
            var userId = GetUserId();
            var command = new UpdateAccountCommand(userId, request.Name);
            await _mediator.Send(command);
            return NoContent();
        }

        /// <summary>
        /// Delete an account
        /// </summary>
        [HttpDelete("{Id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ValidationErrorDto), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> DeleteAccount(Guid Id)
        {
            var userId = GetUserId();
            var command = new DeleteAccountCommand(userId,Id);
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
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetTransactionsByAccountId(Guid accountId , int Page , int PageSize)
        {
            var UserId = GetUserId();
            var query = new GetTransactionsByAccountIdQuery(UserId,accountId,Page,PageSize); // should return account  under the userId 
            var transactions = await _mediator.Send(query);
            return Ok(transactions);
        }
    }
}