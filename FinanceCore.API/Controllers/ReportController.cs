using Asp.Versioning;
using FinanceCore.API.Requests.Report;
using FinanceCore.Application.DTOs;
using FinanceCore.Application.Features.Report.ContributionsTrend;
using FinanceCore.Application.Features.Report.GetBudgetHealth;
using FinanceCore.Application.Features.Report.GetMonthlySummary;
using FinanceCore.Application.Features.Report.GetMonthlyTrend;
using FinanceCore.Application.Features.Report.GetSubscriptions;
using FinanceCore.Application.Features.Report.GetSubscriptionsGrowth;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using System.Security.Claims;

namespace FinanceCore.API.Controllers
{
    [EnableRateLimiting("Reports")]
    [ApiController]
    [Route("api/v{version:apiVersion}/reports")]
    [ApiVersion("1.0")]
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

        [HttpGet("monthly/accounts")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(MonthlySummaryDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationErrorDto), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetMonthlySummary(Guid? id, int? year, int? month,int page = 1 , int pageSize = 10)
        {
            var userId = GetUserId();
            var today = DateTime.Today;
            var query = new GetAccountsMonthlySummaryQuery(userId, id, year ?? today.Year, month ?? today.Month,page,pageSize);
            var response = await _mediator.Send(query);
            return Ok(response);

        }

        [HttpGet("budgets-health")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(BudgetHealthDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationErrorDto), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetBudgetHealth(int page = 1 , int pageSize = 10)
        {
            var userId = GetUserId();
            var query = new BudgetHealthQuery(userId,page,pageSize);
            var response = await _mediator.Send(query);
            return Ok(response);

        }

        [HttpGet("subscriptions")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(SubscriptionDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationErrorDto), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetSubscriptions([FromQuery] GetSubscriptionsRequest request,[FromQuery]int page = 1 ,[FromQuery] int pageSize = 10)
        {
            var userId = GetUserId();
            var query = new SubscriptionQuery(userId,request.CategoryId,request.AccountId,request.Name,request.Period,request.Type,page,pageSize);
            var response = await _mediator.Send(query);
            return Ok(response);


        }

        [HttpGet("subscriptions/growth")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(SubscriptionGrowthDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationErrorDto), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetSubscriptionsGrowth([FromQuery] GetSubscriptionGrowthRequest request)
        {
            var userId = GetUserId();
            var query = new SubscriptionGrowthQuery(userId,request.AccountId,request.Type,request.Start,request.End);
            var response = await _mediator.Send(query);
            return Ok(response);

        }

        [HttpGet("monthly/trend")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(IEnumerable<MonthlyTrendDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationErrorDto), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetTrend([FromQuery]int lastNMonth = 1)
        {
            var userId = GetUserId();
            var query = new MonthlyTrendQuery(userId,lastNMonth);
            var response = await _mediator.Send(query);
            return Ok(response);

        }

        [HttpGet("monthly/contributions/trend")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(IEnumerable<ContributionsTrendDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationErrorDto), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetContributionsTrend([FromQuery]int lastNMonth = 1)
        {
            var userId = GetUserId();
            var query = new ContributionsTrendQuery(userId,lastNMonth);
            var response = await _mediator.Send(query);
            return Ok(response);

        }



    }
}

