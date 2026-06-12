using FinanceCore.Application.Abstractions;
using FinanceCore.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Application.Features.Categories.Queries.GetCategoriesByUserOptions
{
    public class GetCategoriesByUserOptionsHandler : IRequestHandler<GetCategoriesByUserOptionsQuery,IEnumerable<CategoryOptionDto>>
    {
        private readonly ICategoryRepository _categoryRepository;
        public GetCategoriesByUserOptionsHandler(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }
        public async Task<IEnumerable<CategoryOptionDto>> Handle(GetCategoriesByUserOptionsQuery query,CancellationToken token)
        {
            return await _categoryRepository.GetCategoriesByUserOptionsAsync(query.UserId,query.Page,query.PageSize, token);
        }
    }
}
