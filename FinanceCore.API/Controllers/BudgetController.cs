using FinanceCore.API.Requests;
using FinanceCore.API.Requests.Budget;
using FinanceCore.Application.DTOs;
using FinanceCore.Application.Features.Budgets.Commands.Create;
using FinanceCore.Application.Features.Budgets.Commands.Delete;
using FinanceCore.Application.Features.Budgets.Commands.Update;
using FinanceCore.Application.Features.Budgets.Queries.GetBudgetById;
using FinanceCore.Application.Features.Budgets.Queries.GetBudgetProgress;
using FinanceCore.Application.Features.Budgets.Queries.GetBudgetsFiltered;
using FinanceCore.Domain.Common;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using System.Security.Claims;

namespace FinanceCore.API.Controllers
{
    [EnableRateLimiting("Default")]
    [ApiController]
    [Route("api/v1/budgets")]
    [Authorize]
    public class BudgetsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BudgetsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        private Guid GetUserId()
        {
            return Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        }


        /// <summary>
        /// get budgets with filters
        /// </summary>
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(typeof(IEnumerable<BudgetInfoDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationErrorDto), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetBudgets([FromQuery] GetBudgetsFilteredRequest request)
        {
            var UserId = GetUserId();
            var command = new GetBudgetsFilteredQuery(UserId, request.Name, request.CategoryId, request.Period, request.Page, request.PageSize);
            var budgets = await _mediator.Send(command);
            return Ok(budgets);
                
        }
        /// <summary>
        /// Create a new budget
        /// </summary>
        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(typeof(BudgetDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ValidationErrorDto), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> CreateBudget([FromBody] CreateBudgetRequest request)
        {
            var UserId = GetUserId();
            var command = new CreateBudgetCommand(UserId, request.CategoryId, request.name, new Money(request.Amount, request.Currency), request.Period, request.StartDate);
            var budget = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetBudgetById), new { id = budget.Id }, budget);
        }

        /// <summary>
        /// Get budget by ID
        /// </summary>
        [HttpGet("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(BudgetDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationErrorDto), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetBudgetById([FromRoute]Guid id)
        {
            var UserId = GetUserId();
            var query = new GetBudgetByIdQuery(UserId ,id);
            var budget = await _mediator.Send(query);
            return Ok(budget);
        }
        /// <summary>
        /// Get budget Progress
        /// </summary>
        [HttpGet("{id}/progress")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(BudgetProgressDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ValidationErrorDto), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetBudgetProgressById(Guid id)
        {
            var UserId = GetUserId();
            var query = new GetBudgetProgressQuery(UserId,id);
            var budget = await _mediator.Send(query);
            return Ok(budget);
        }


        /// <summary>
        /// Update an existing budget
        /// </summary>
        [HttpPut("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ValidationErrorDto), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> UpdateBudget(Guid id,[FromBody] UpdateBudgetRequest request)
        {
            var UserId = GetUserId() ;
            var command = new UpdateBudgetCommand(UserId,id,request.Name,new Money(request.Amount,request.Currency),request.Period,request.StartDate);
            await _mediator.Send(command);
            return NoContent();
        }

        /// <summary>
        /// Delete a budget
        /// </summary>
        [HttpDelete("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ValidationErrorDto), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> DeleteBudget(Guid id)
        {
            var UserId = GetUserId();
            var command = new DeleteBudgetCommand(UserId ,id);
            await _mediator.Send(command);
            return NoContent();
        }

    }
}