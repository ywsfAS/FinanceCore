using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Domain.Enums
{
    public enum EnMessageSubject : byte
    {
        AccountAndBilling = 1,
        TechnicalSupport = 2,
        FeatureRequest = 3,
        Partnership = 4,
        Security = 5,
        Other = 6,
    }
}
