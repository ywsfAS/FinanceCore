using FinanceCore.Application.DTOs;
using FinanceCore.Application.Features.Report.GetMonthlySummary;
using FinanceCore.Application.Features.Report.GetMonthlyTrend;
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

        [HttpGet("monthly/accounts")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(MonthlySummaryDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationErrorDto), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetMonthlySummary(Guid? id, int year, int month,int page = 1 , int pageSize = 10)
        {
            var UserId = GetUserId();
            var query = new GetAccountsMonthlySummaryQuery(UserId, id, year, month,page,pageSize);
            var response = await _mediator.Send(query);
            return Ok(response);

        }


        [HttpGet("monthly/trend")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(IEnumerable<MonthlyTrendDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationErrorDto), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetTrend([FromQuery]int lastNMonth)
        {
            var UserId = GetUserId();
            var query = new MonthlyTrendQuery(UserId,lastNMonth);
            var response = await _mediator.Send(query);
            return Ok(response);

        }




    }
}

