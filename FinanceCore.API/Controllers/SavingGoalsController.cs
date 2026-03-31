using FinanceCore.API.Requests.ReccuringTransations;
using FinanceCore.API.Requests.Savings;
using FinanceCore.Application.DTOs;
using FinanceCore.Application.DTOs.Goal;
using FinanceCore.Application.DTOs.RecurringTransaction;
using FinanceCore.Application.Features.Goals.Commands.Create;
using FinanceCore.Application.Features.Goals.Commands.Delete;
using FinanceCore.Application.Features.Goals.Commands.Update;
using FinanceCore.Application.Features.RecurringTransaction.Commands.Delete;
using FinanceCore.Application.Features.RecurringTransaction.Commands.Update;
using FinanceCore.Application.Features.RecurringTransactions.commands.Create;
using FinanceCore.Application.Features.RecurringTransactions.queries.GetRecurringById;
using FinanceCore.Application.Features.SavingGoals.Queries.GetSavingGoalById;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FinanceCore.API.Controllers
{
    [ApiController]
    [Route("api/v1/savings")]
    [Authorize]
    public class SavingGoalsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public SavingGoalsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        private Guid GetUserId()
        {
            return Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        }
        /// <summary>
        /// Create a new saving Goal
        /// </summary>
        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(typeof(SavingsGoalDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ValidationErrorDto), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> CreateSavingGoal([FromBody] CreateSavingsGoalRequest request)
        {
            var userId = GetUserId();
            var command = new CreateSavingsGoalCommand(userId,request.Name,request.TargetAmount,request.TargetDate , request.Description);
            var saving = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetRecurringByIdQuery), new { id = saving.Id }, saving);
        }


        /// <summary>
        /// Update an existing saving Goal
        /// </summary>
        [HttpPut]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ValidationErrorDto), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> UpdateSavingGoal([FromBody] UpdateSavingsGoalRequest request)
        {
            var userId = GetUserId();
            var command = new UpdateSavingsGoalCommand(userId, request.Id, request.Name, request.TargetAmount, request.TargetDate, request.Description, request.Status);
            await _mediator.Send(command);
            return NoContent();
        }
        /// <summary>
        /// Delete a saving Goal
        /// </summary>
        [HttpDelete("{Id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ValidationErrorDto), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> DeleteSavingGoal(Guid Id)
        {
            var userId = GetUserId();
            var command = new DeleteSavingsGoalCommand(userId, Id);
            await _mediator.Send(command);
            return NoContent();
        }
        /// <summary>
        /// Get saving Goal
        /// </summary>
        [HttpGet("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(SavingsGoalDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ValidationErrorDto), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetSavingGoalById(Guid id)
        {
            var userId = GetUserId();
            var query = new GetSavingGoalQuery(userId, id);
            var saving = await _mediator.Send(query);
            return Ok(saving);
        }
    }
}
