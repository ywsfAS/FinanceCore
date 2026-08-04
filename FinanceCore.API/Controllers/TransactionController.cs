using Asp.Versioning;
using FinanceCore.API.Requests.Transaction;
using FinanceCore.Application.DTOs;
using FinanceCore.Application.DTOs.Transaction;
using FinanceCore.Application.Features.Transactions.Commands.Delete;
using FinanceCore.Application.Features.Transactions.Commands.TransactionImports;
using FinanceCore.Application.Features.Transactions.Commands.Transactions;
using FinanceCore.Application.Features.Transactions.Export;
using FinanceCore.Application.Features.Transactions.Queries.GetFiltredTransactions;
using FinanceCore.Application.Features.Transactions.Queries.GetTransactionById;
using FinanceCore.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using System.Security.Claims;

namespace FinanceCore.API.Controllers
{
    [EnableRateLimiting("Default")]
    [ApiController]
    [Route("api/v{version:apiVersion}/transactions")]
    [ApiVersion("1.0")]
    [Authorize]
    public class TransactionsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public TransactionsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        private Guid GetUserId()
        {
            return Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        }

        /// <summary>
        /// Create a new transaction
        /// </summary>
        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(typeof(TransactionDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ValidationErrorDto), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateTransaction([FromBody] CreateTransactionRequest request)
        {
            var UserId = GetUserId();
            var command = new TransactionCommand(UserId, request.AccountId,request.ToAccountId, request.CategoryId, request.Type,request.Amount, request.Description, request.TransactionDate);
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
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetTransactionById(Guid id)
        {
            var UserId = GetUserId();
            var query = new GetTransactionByIdQuery(UserId , id);
            var transaction = await _mediator.Send(query);
            return Ok(transaction);
        }


        /// <summary>
        /// Get all transactions for an account
        /// </summary>
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(typeof(IEnumerable<TransactionDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationErrorDto), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetTransactionsByFilters(Guid? accountId , Guid ? toAccountId, Guid? CategoryId , DateTime? Start , DateTime? End , EnTransactionType? Type , int Page = 1 ,int PageSize = 10 )
        {
            var userId = GetUserId();
            var query = new GetFiltredTransactionsQuery(userId,accountId,toAccountId,CategoryId, Start, End, Type, Page, PageSize);
            var transactions = await _mediator.Send(query);
            return Ok(transactions);
        }

        /// <summary>
        /// export transactions with filters
        /// </summary>
        [HttpGet("export")]
        [ProducesResponseType(typeof(File), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationErrorDto), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> ExportTransactionsByFilters(Guid? accountId , Guid ? toAccountId, Guid? CategoryId , DateTime? Start , DateTime? End , EnTransactionType? Type , int Page = 1 ,int PageSize = 10 )
        {
            var userId = GetUserId();
            var query = new ExportCSVQuery(userId,accountId,toAccountId,CategoryId, Start, End, Type, Page, PageSize);
            var result = await _mediator.Send(query);
            return File(result.content,result.contentType , result.fileName);
        }
        /// <summary>
        /// import transactions
        /// </summary>
        [HttpPost("import/{type}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationErrorDto), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> ImportTransactions(
            [FromForm] ImportTransactionRequest req,
            [FromRoute] EnFileType type)
        {
            var userId = GetUserId();

            await using var stream = req.File.OpenReadStream();

            var command = new ImportTransactionCommand(
                userId,
                req.AccountId,
                stream,
                type,
                req.File.FileName);

            await _mediator.Send(command);

            return Ok("Transactions imported successfully");
        }
        /// <summary>
        /// Delete a transaction
        /// </summary>
        [HttpDelete("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ValidationErrorDto), StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> DeleteTransaction(Guid id)
        {
            var UserId = GetUserId();
            var command = new DeleteTransactionCommand(UserId,id);
            await _mediator.Send(command);
            return NoContent();
        }
    }
}