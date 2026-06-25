using FinanceCore.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Application.DTOs
{
    public class SubscriptionGrowthDto
    {
        public decimal CurrentPeriodTotal { get; set; }
        public decimal PreviousPeriodTotal { get; set; }
        public decimal? ChangePercent { get; set; }
        public EnCurrency Currency { get; set; }
    }
}
