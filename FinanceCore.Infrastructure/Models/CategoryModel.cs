using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Infrastructure.Models
{
    namespace FinanceCore.Infrastructure.Models
    {
        public class CategoryModel
        {
            public Guid Id { get; set; }
            public Guid UserId { get; set; }
            public string Name { get; set; } = string.Empty;
            public bool IsActive { get; set; }
            public byte Type { get; set; }
            public string? Description { get; set; }
            public bool IsDefault { get; set; }
            public DateTime CreatedAt { get; set; }
            public DateTime? UpdatedAt { get; set; }
        }
    }

}
