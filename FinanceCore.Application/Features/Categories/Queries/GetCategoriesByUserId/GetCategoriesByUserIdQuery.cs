using FinanceCore.Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Application.Features.Categories.Queries.GetCategoriesByUserId
{
    public record GetCategoriesByUserIdQuery(Guid UserId , int Page ,int PageSize) : IRequest<IEnumerable<CategoryDto>?>;
}
