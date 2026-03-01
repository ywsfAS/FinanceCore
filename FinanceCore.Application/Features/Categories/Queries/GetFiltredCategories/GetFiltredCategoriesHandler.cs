using FinanceCore.Application.Abstractions;
using FinanceCore.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Application.Features.Categories.Queries.GetFiltredCategories
{
    public class GetFiltredCategoriesHandler : IRequestHandler<GetFiltredCategoriesQuery , IEnumerable<CategoryDto>?>
    {
        private readonly ICategoryRepository _categoryRepository;
        public GetFiltredCategoriesHandler(ICategoryRepository categoryRepository) { 
            _categoryRepository = categoryRepository;
        }
        public async Task<IEnumerable<CategoryDto>?> Handle(GetFiltredCategoriesQuery query ,CancellationToken token
         )
        {
            return await  _categoryRepository.GetFiltredCategoriesAsync(query.UserId,query.Name,(byte?)query.Type,query.CreatedAt,query.Page , query.PageSize);
        }
    }
}
