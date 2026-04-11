using FinanceCore.Application.DTOs;
using FinanceCore.Application.Features.Report.GetMonthlySummary;
using FinanceCore.Application.Features.Report.GetMonthlySummaryPerAccount;
using FinanceCore.Application.Features.Report.GetMonthlySummaryPerUser;
using FinanceCore.Application.Features.Report.GetNetWorth;
using FinanceCore.Application.Features.Report.GetSpendingByCategory;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Routing.Constraints;
using System.Security.Claims;

namespace FinanceCore.API.Controllers
{
    [ApiController]
    [Route("api/v1/reports")]
    [Authorize]
    public class ReportController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ReportController(IMediator mediator) {
            _mediator = mediator;
        }
        private Guid GetUserId()
        {
            return Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        }

        [HttpGet("monthly-summary")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(MonthlySummaryDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationErrorDto), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetMonthlySummary(Guid id, int year, int month)
        {
            var UserId = GetUserId();
            var query = new GetMonthlySummaryQuery(UserId, id, year, month);
            var response = await _mediator.Send(query);
            return Ok(response);

        }
        [HttpGet("monthly-summary-per-user")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(IEnumerable<MonthlySummaryDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationErrorDto), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetMonthlySummaryPerUser(int year, int month)
        {
            var UserId = GetUserId();
            var query = new GetMonthlySummaryQueryUser(UserId,year, month);
            var response = await _mediator.Send(query);
            return Ok(response);

        }
        [HttpGet("spending-by-category")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(IEnumerable<SpendingByCategoryDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationErrorDto), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetSpendingByCategory(Guid? AccountId, int year, int month)
        {
            var UserId = GetUserId();
            var query = new GetSpendingByCategoryQuery(UserId, AccountId, year, month);
            var response = await _mediator.Send(query);
            return Ok(response);
        }
        [HttpGet("net-worth")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(NetWorthDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationErrorDto), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetNetWorth()
        {
            var UserId = GetUserId();
            var query = new GetNetWorthQuery(UserId);
            var response = await _mediator.Send(query);
            return Ok(response);

        }



    }
}

