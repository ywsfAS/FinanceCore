using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Application.DTOs.Auth
{
    public record RegisterDto(
      Guid Id,
      string Name,
      string Email
    );
}
