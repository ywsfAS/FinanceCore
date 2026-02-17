using FinanceCore.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Application.DTOs
{
    public record CategoryDto(
        Guid Id,
        Guid UserId,
        string Name,
        CategoryType Type,
        string? Description);

}
