using FinanceCore.API.Requests.Account;
using FinanceCore.Application.DTOs;
using FinanceCore.Application.DTOs.Transaction;
using FinanceCore.Application.Features.Accounts.Commands.Create;
using FinanceCore.Application.Features.Accounts.Commands.Delete;
using FinanceCore.Application.Features.Accounts.Commands.Update;
using FinanceCore.Application.Features.Accounts.Queries.GetAccountById;
using FinanceCore.Application.Features.Accounts.Queries.GetAccountByUserOptions;
using FinanceCore.Application.Features.Accounts.Queries.GetAccountFiltered;
using FinanceCore.Application.Features.Report.GetSpendingByCategory;
using FinanceCore.Domain.Common;
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
            var command = new CreateAccountCommand(userId,request.Name,request.Type,new Money(request.InitialBalance,request.Currency));
            var account = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetAccountById), new { id = account.Id }, account);
        }

        /// <summary>
        /// get user accounts info filtered
        /// </summary>
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(typeof(IEnumerable<AccountInfoDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationErrorDto), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetAccounts([FromQuery] AccountFilteredRequest request)
        {
            var userId = GetUserId();
            var command = new GetAccountFilteredQuery(userId,request.Name,request.Type,request.Currency,request.page,request.pageSize); 
            var accounts = await _mediator.Send(command);
            return Ok(accounts);
        }

        /// <summary>
        /// Get account by Id
        /// </summary>
        [HttpGet("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(AccountDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationErrorDto), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetAccountById([FromRoute]Guid id)
        {
            var userId = GetUserId();
            var query = new GetAccountByIdQuery(userId,id); // Should return only Accounts of UserId
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
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> UpdateAccount([FromRoute] Guid id,[FromBody] UpdateAccountRequest request)
        {
            var userId = GetUserId();
            var command = new UpdateAccountCommand(userId,id, request.Name,request.Type);
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
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> DeleteAccount([FromRoute]Guid id)
        {
            var userId = GetUserId();
            var command = new DeleteAccountCommand(userId,id);
            await _mediator.Send(command);
            return NoContent();
        }
        /// <summary>
        /// Get accounts options format
        /// </summary>
        [HttpGet("options")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(IEnumerable<AccountOptionsDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationErrorDto), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetAccountsOptions([FromQuery] int page , [FromQuery] int pageSize)
        {
            var userId = GetUserId();
            var query = new GetAccountsOptionsQuery(userId); 
            var accounts = await _mediator.Send(query);
            return Ok(accounts);
        }

        [HttpGet("{accountId}/spending-by-category")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(IEnumerable<SpendingByCategoryDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationErrorDto), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetSpendingByCategory([FromRoute]Guid accountId, [FromQuery] int year,[FromQuery] int month, [FromQuery] int page = 1 , [FromQuery] int pageSize = 10)
        {
            var UserId = GetUserId();
            var query = new GetSpendingByCategoryQuery(UserId, accountId, year, month,page,pageSize);
            var response = await _mediator.Send(query);
            return Ok(response);
        }


    }
}