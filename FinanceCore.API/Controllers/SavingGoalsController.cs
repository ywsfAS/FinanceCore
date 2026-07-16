using FinanceCore.API.Requests.Savings;
using FinanceCore.Application.DTOs;
using FinanceCore.Application.DTOs.Goal;
using FinanceCore.Application.Features.Goals.Commands.Create;
using FinanceCore.Application.Features.Goals.Commands.Delete;
using FinanceCore.Application.Features.Goals.Commands.Update;
using FinanceCore.Application.Features.SavingGoals.Commands.AddContribution;
using FinanceCore.Application.Features.SavingGoals.Commands.Cancel;
using FinanceCore.Application.Features.SavingGoals.Commands.Pause;
using FinanceCore.Application.Features.SavingGoals.Commands.Resume;
using FinanceCore.Application.Features.SavingGoals.Queries.GetSavingGoalById;
using FinanceCore.Application.Features.SavingGoals.Queries.GetSavingGoalsFiltered;
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
        public async Task<IActionResult> CreateSavingGoal([FromBody] CreateSavingsGoalRequest request, CancellationToken cancellationToken)
        {
            var userId = GetUserId();
            var command = new CreateSavingsGoalCommand(userId, request.Name, new Money(request.TargetAmount, request.Currency), request.TargetDate, request.Description);

            var saving = await _mediator.Send(command, cancellationToken);

            return CreatedAtAction(nameof(GetSavingGoalById), new { id = saving.Id }, saving);
        }

        /// <summary>
        /// Update an existing saving Goal
        /// </summary>
        [HttpPut("{id:guid}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ValidationErrorDto), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> UpdateSavingGoal([FromRoute] Guid id,[FromBody] UpdateSavingsGoalRequest request, CancellationToken cancellationToken)
        {
            var userId = GetUserId();
            var command = new UpdateSavingsGoalCommand(userId,id,request.Name, new Money(request.TargetAmount, request.Currency), request.TargetDate, request.Description, request.Status);

            await _mediator.Send(command, cancellationToken);

            return NoContent();
        }

        /// <summary>
        /// Delete a saving Goal
        /// </summary>
        [HttpDelete("{id:guid}")] // Enforces Route Guid constraint
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ValidationErrorDto), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> DeleteSavingGoal([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var userId = GetUserId();
            var command = new DeleteSavingsGoalCommand(userId, id);

            await _mediator.Send(command, cancellationToken);

            return NoContent();
        }

        /// <summary>
        /// Get saving Goal
        /// </summary>
        [HttpGet("{id:guid}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(SavingsGoalDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationErrorDto), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetSavingGoalById([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var userId = GetUserId();
            var query = new GetSavingGoalQuery(userId, id);

            var saving = await _mediator.Send(query, cancellationToken);

            return Ok(saving);
        }

        /// <summary>
        /// Get saving Goals using pagination and filters
        /// </summary>
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(typeof(IEnumerable<SavingsGoalDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationErrorDto), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetSavingGoals([FromQuery] GetSavingGoalsFilteredRequest request, CancellationToken cancellationToken)
        {
            var userId = GetUserId();
            var query = new GetSavingsGoalFilteredQuery(userId,request.Name,request.Currency,request.Status ,request.Page, request.PageSize);

            var saving = await _mediator.Send(query, cancellationToken);

            return Ok(saving);
        }

        /// <summary>
        /// Contribute to a saving goal
        /// </summary>
        [HttpPost("{id:guid}/contribute")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ValidationErrorDto), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> AddContribute([FromRoute] Guid id,[FromBody]AddGoalContributionRequest request, CancellationToken cancellationToken)
        {
            var userId = GetUserId();
            var command = new AddContributionCommand(userId,id,request.AccountId,request.Amount,request.Currency,request.ContributionDate,request.Description);

            await _mediator.Send(command, cancellationToken);

            return Ok(new { Message = "Contribution Added Succesfully" });
        }
        /// <summary>
        /// Pause a saving goal
        /// </summary>
        [HttpPost("{id:guid}/pause")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationErrorDto), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> PauseSavingGoal([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var userId = GetUserId();
            var command = new PauseSavingGoalCommand(id, userId);

            await _mediator.Send(command, cancellationToken);

            return Ok(new { Message = "Paused successfully" });
        }

        /// <summary>
        /// Resume a saving goal
        /// </summary>
        [HttpPost("{id:guid}/resume")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationErrorDto), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> ResumeSavingGoal([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var userId = GetUserId();
            var command = new ResumeSavingGoalCommand(id, userId);

            await _mediator.Send(command, cancellationToken);

            return Ok(new { Message = "Resumed successfully" });
        }

        /// <summary>
        /// Cancel a saving goal
        /// </summary>
        [HttpPost("{id:guid}/cancel")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationErrorDto), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> CancelSavingGoal([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var userId = GetUserId();
            var command = new CancelSavingGoalCommand(id, userId);

            await _mediator.Send(command, cancellationToken);

            return Ok(new { Message = "Cancelled successfully" });
        }
    }
}