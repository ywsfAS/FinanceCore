using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FinanceCore.Infrastructure.Models
{
    public class TransferModel
    {
        public Guid DebitTransactionId { get; set; }
        public Guid CreditTransactionId { get; set; }
        public decimal SourceBalance { get; set; }
        public decimal DestinationBalance { get; set; }
        public DateTime TransferDate { get; set; }
    }
}
