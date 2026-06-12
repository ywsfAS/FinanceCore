using FinanceCore.API.Requests.Category;
using FinanceCore.Application.DTOs;
using FinanceCore.Application.Features.Categories.Commands.Create;
using FinanceCore.Application.Features.Categories.Commands.Delete;
using FinanceCore.Application.Features.Categories.Commands.Update;
using FinanceCore.Application.Features.Categories.Queries.GetCategoriesByUserOptions;
using FinanceCore.Application.Features.Categories.Queries.GetCategoryById;
using FinanceCore.Application.Features.Categories.Queries.GetFiltredCategories;
using FinanceCore.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace FinanceCore.API.Controllers
{
    [ApiController]
    [Route("api/v1/categories")]
    [Authorize]

    public class CategoriesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CategoriesController(IMediator mediator)
        {
            _mediator = mediator;
        }
        private Guid GetUserId()
        {
            return Guid.Parse(User.FindFirst(ClaimTypes.NameIdentifier)!.Value);
        }

        /// <summary>
        /// Create a new category
        /// </summary>
        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(typeof(CategoryDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ValidationErrorDto), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryRequest request)
        {
            var UserId = GetUserId();
            var command = new CreateCategoryCommand(UserId, request.Name, request.Type, request.Description);
            var response = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetCategoryById), new { id = response.Id }, response);
        }

        /// <summary>
        /// Get category by ID
        /// </summary>
        [HttpGet("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(CategoryDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationErrorDto), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetCategoryById([FromRoute] Guid id)
        {
            var UserId = GetUserId();
            var query = new GetCategoryByIdQuery(UserId, id);
            var category = await _mediator.Send(query);
            return Ok(category);
        }

        /// <summary>
        /// Update an existing category
        /// </summary>
        [HttpPut("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ValidationErrorDto), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> UpdateCategory([FromRoute]Guid id, [FromBody] UpdateCategoryRequest request)
        {
            var UserId = GetUserId();
            var command = new UpdateCategoryCommand(UserId, id, request.Name, request.Descritption);
            await _mediator.Send(command);
            return NoContent();
        }

        /// <summary>
        /// Delete a category
        /// </summary>
        [HttpDelete("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(ValidationErrorDto), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> DeleteCategory([FromRoute]Guid id)
        {
            var UserId = GetUserId();
            var command = new DeleteCategoryCommand(UserId, id);
            await _mediator.Send(command);
            return NoContent();
        }
        /// <summary>
        /// Get all categories for a user
        /// </summary>
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(typeof(CategoryDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationErrorDto), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetCategoriesByUserId(string? Name, CategoryType? Type, DateTime? Date, int Page = 1, int PageSize = 10)
        {
            var userId = GetUserId();
            var query = new GetFiltredCategoriesQuery(userId, Name, Type, Date, Page, PageSize);
            var categories = await _mediator.Send(query);
            return Ok(categories);
        }

        /// <summary>
        /// Get all categories name for a user
        /// </summary>
        [HttpGet("categories/options")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(IEnumerable<CategoryOptionDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationErrorDto), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public async Task<IActionResult> GetCategoriesByUserIdNames([FromRoute] int page = 1 , [FromRoute] int pageSize = 10)
        {
            var userId = GetUserId();
            var query = new GetCategoriesByUserOptionsQuery(userId,page,pageSize);
            var categories = await _mediator.Send(query);
            return Ok(categories);
        }





    }
}