using FinanceCore.Domain.Enums;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Application.Features.Categories.Commands.Create
{
    public record CreateCategoryCommand(
        Guid UserId,
        string Name,
        CategoryType Type,
        string? Description = null) : IRequest<Guid>;
}
