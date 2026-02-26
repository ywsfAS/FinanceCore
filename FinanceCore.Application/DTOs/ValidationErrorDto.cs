using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Application.DTOs
{
    public class ValidationErrorDto
    {
        public Dictionary<string, string[]> Errors { get; set; } = new();
    }
}
