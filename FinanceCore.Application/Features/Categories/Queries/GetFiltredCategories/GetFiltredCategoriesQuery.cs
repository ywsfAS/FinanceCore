using FinanceCore.Application.DTOs;
using FinanceCore.Domain.Enums;
using MediatR;
using System;
using System.ComponentModel;


namespace FinanceCore.Application.Features.Categories.Queries.GetFiltredCategories
{
    public record GetFiltredCategoriesQuery(Guid? UserId ,string? Name, CategoryType? Type, DateTime? CreatedAt, int Page , int PageSize) : IRequest<IEnumerable<CategoryDto>?>;
}
