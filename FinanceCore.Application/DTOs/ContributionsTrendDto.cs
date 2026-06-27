using FinanceCore.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Application.DTOs
{

    public record ContributionsTrendDataDto(string Month , decimal SavedAmount  ,decimal TargetAmount , decimal SavedPercentage );
    public class ContributionsTrendDto
    {
        public EnCurrency Currency { get; set; }
        public IEnumerable<ContributionsTrendDataDto> History { get; set; }

    }
}
