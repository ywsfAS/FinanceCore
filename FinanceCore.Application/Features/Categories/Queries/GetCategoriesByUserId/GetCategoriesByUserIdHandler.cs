using FinanceCore.Application.Abstractions;
using FinanceCore.Application.DTOs;
using FinanceCore.Domain.Categories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Application.Features.Categories.Queries.GetCategoriesByUserId
{
    public class GetCategoriesByUserIdHandler : IRequestHandler<GetCategoriesByUserIdQuery,IEnumerable<CategoryDto>?>
    {
        private readonly ICategoryRepository _categoryRepository;
        public GetCategoriesByUserIdHandler(ICategoryRepository categoryRepository) { 
            _categoryRepository = categoryRepository;
        }
        public async Task<IEnumerable<CategoryDto>?> Handle(GetCategoriesByUserIdQuery query , CancellationToken token
         )
        {
            return await  _categoryRepository.GetCategoriesByUserIdAsync(query.UserId, query.Page, query.PageSize,token);
        }
    }
}
