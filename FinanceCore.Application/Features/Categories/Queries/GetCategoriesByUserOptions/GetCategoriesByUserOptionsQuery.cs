using FinanceCore.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Application.Features.Categories.Queries.GetCategoriesByUserOptions
{
    public record GetCategoriesByUserOptionsQuery(Guid userId) : IRequest<IEnumerable<CategoryOptionDto>?>;
}
