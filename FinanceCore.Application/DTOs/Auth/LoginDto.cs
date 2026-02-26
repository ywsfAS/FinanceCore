using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Application.DTOs.Auth
{
    public record LoginDto(
        Guid Id,
        string Email,
        string? Token
    );
}
