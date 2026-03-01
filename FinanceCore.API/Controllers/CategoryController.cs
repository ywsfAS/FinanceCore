using FinanceCore.Application.DTOs;
using FinanceCore.Application.DTOs.Auth;
using FinanceCore.Application.Features.Categories.Commands.Create;
using FinanceCore.Application.Features.Categories.Commands.Delete;
using FinanceCore.Application.Features.Categories.Commands.Update;
using FinanceCore.Application.Features.Categories.Queries;
using FinanceCore.Application.Features.Categories.Queries.GetCategoriesByUserId;
using FinanceCore.Application.Features.Categories.Queries.GetCategoryById;
using FinanceCore.Application.Features.Categories.Queries.GetFiltredCategories;
using FinanceCore.Domain.Enums;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace FinanceCore.API.Controllers
{
    [ApiController]
    [Route("api/categories")]
    public class CategoriesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CategoriesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Create a new category
        /// </summary>
        [HttpPost()]
        [Produces("application/json")]
        [ProducesResponseType(typeof(CategoryDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ValidationErrorDto), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateCategory([FromBody] CreateCategoryCommand command)
        {
            var response = await _mediator.Send(command);
            return CreatedAtAction(nameof(GetCategoryById), new { id = response.Id}, response);
        }

        /// <summary>
        /// Get category by ID
        /// </summary>
        [HttpGet("{id}")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(CategoryDto), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ValidationErrorDto), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetCategoryById(Guid id)
        {
            var query = new GetCategoryByIdQuery(id);
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
        public async Task<IActionResult> UpdateCategory(Guid id, [FromBody] UpdateCategoryCommand command)
        {
            if (id != command.Id)
                return BadRequest("ID mismatch");

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
        public async Task<IActionResult> DeleteCategory(Guid id)
        {
            var command = new DeleteCategoryCommand(id);
            await _mediator.Send(command);
            return NoContent();
        }
        /// <summary>
        /// Get all categories for a user
        /// </summary>
        [HttpGet("categories")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(CategoryDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ValidationErrorDto), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetCategoriesByUserId(Guid? userId,string? Name , CategoryType? Type ,DateTime? Date, int Page = 1, int PageSize = 10)
        {
            var query = new GetFiltredCategoriesQuery(userId,Name,Type,Date ,Page, PageSize);
            var categories = await _mediator.Send(query);
            return Ok(categories);
        }
    }
}