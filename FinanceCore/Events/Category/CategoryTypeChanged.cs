using FinanceCore.Domain.Common;
using FinanceCore.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Domain.Events.Category
{
       public record CategoryTypeChangedEvent(Guid id, CategoryType Old , CategoryType New) : DomainEvent;
}
