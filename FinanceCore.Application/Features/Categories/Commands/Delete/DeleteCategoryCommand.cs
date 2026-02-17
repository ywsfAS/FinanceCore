using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Application.Features.Categories.Commands.Delete
{
    public record DeleteCategoryCommand(Guid Id) : IRequest;
}
