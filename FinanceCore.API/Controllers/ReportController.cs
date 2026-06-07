using FinanceCore.Application.DTOs;
using FinanceCore.Application.Features.Report.GetMonthlySummary;
using FinanceCore.Application.Features.Report.GetMonthlySummaryPerAccount;
using FinanceCore.Application.Features.Report.GetMonthlySummaryPerUser;
using FinanceCore.Application.Features.Report.GetMonthlyTrend;
using FinanceCore.Application.Features.Report.GetNetWorth;
using FinanceCore.Application.Features.Report.GetSpendingByCategory;
using FinanceCore.Application.Features.Report.GetSpendingByCategoryPerUser;
using FinanceCore.Application.Features.Report.GetSummaryPerUser;
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

        [HttpGet("monthly/account")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(MonthlyAccountSummaryDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationErrorDto), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetMonthlySummary(Guid id, int year, int month)
        {
            var UserId = GetUserId();
            var query = new GetMonthlySummaryQuery(UserId, id, year, month);
            var response = await _mediator.Send(query);
            return Ok(response);

        }
        [HttpGet("summary/user")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(MonthlyAccountSummaryDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationErrorDto), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetSummaryPerUser()
        {
            var UserId = GetUserId();
            var query = new GetSummaryPerUserQuery(UserId);
            var response = await _mediator.Send(query);
            return Ok(response);

        }

        [HttpGet("monthly/user")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(MonthlySummaryPerUserDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationErrorDto), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetMonthlySummaryPerUser(int year , int month)
        {
            var UserId = GetUserId();
            var query = new GetMonthlySummaryPerUserQuery(UserId, year, month);
            var response = await _mediator.Send(query);
            return Ok(response);

        }


        [HttpGet("monthly/trend")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(IEnumerable<MonthlyTrendDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationErrorDto), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetTrend([FromQuery]int month)
        {
            var UserId = GetUserId();
            var query = new MonthlyTrendQuery(UserId,month);
            var response = await _mediator.Send(query);
            return Ok(response);

        }


        [HttpGet("spending/by-category/user")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(IEnumerable<SpendingByCategoryDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationErrorDto), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetSpendingCategoryPerUser(int year, int month)
        {
            var UserId = GetUserId();
            var query = new GetSpendingByCategoryPerUserQuery(UserId,year, month);
            var response = await _mediator.Send(query);
            return Ok(response);

        }

        [HttpGet("spending/by-category/account")]
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

