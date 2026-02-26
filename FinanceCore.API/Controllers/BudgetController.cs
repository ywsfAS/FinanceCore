using FinanceCore.Application.Features.Budgets.Commands.Create;
using FinanceCore.Application.Features.Budgets.Commands.Delete;
using FinanceCore.Application.Features.Budgets.Commands.Update;
using FinanceCore.Application.Features.Budgets.Queries.GetBudgetById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FinanceCore.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BudgetsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public BudgetsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Create a new budget
        /// </summary>
        [HttpPost("Create")]
        public async Task<IActionResult> CreateBudget([FromBody] CreateBudgetCommand command)
        {
            var budgetId = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetBudgetById), new { id = budgetId }, budgetId);
        }

        /// <summary>
        /// Get budget by ID
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetBudgetById(Guid id)
        {
            var query = new GetBudgetByIdQuery(id);
            var budget = await _mediator.Send(query);
            return Ok(budget);
        }

        /// <summary>
        /// Get all budgets for a user
        /// </summary>
        [HttpGet("user/{userId}")]
        public async Task<IActionResult> GetBudgetsByUserId(Guid userId)
        {
            var query = new GetBudgetByIdQuery(userId);
            var budgets = await _mediator.Send(query);
            return Ok(budgets);
        }

        /// <summary>
        /// Update an existing budget
        /// </summary>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateBudget(Guid id, [FromBody] UpdateBudgetCommand command)
        {
            if (id != command.Id)
                return BadRequest("ID mismatch");

            await _mediator.Send(command);
            return NoContent();
        }

        /// <summary>
        /// Delete a budget
        /// </summary>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBudget(Guid id)
        {
            var command = new DeleteBudgetCommand(id);
            await _mediator.Send(command);
            return NoContent();
        }
    }
}