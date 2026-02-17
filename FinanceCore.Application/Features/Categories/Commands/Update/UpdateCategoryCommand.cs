using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Application.Features.Categories.Commands.Update
{
    public record UpdateCategoryCommand(
        Guid Id,
        string Name,
        string? Description = null) : IRequest;
}
