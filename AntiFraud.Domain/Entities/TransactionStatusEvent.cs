using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transaction.Domain.Enum;

namespace AntiFraud.Domain.Entities
{
    public class TransactionStatusEvent
    {
        public Guid TransactionId { get; set; }
        public StatusPayment Status { get; set; } = StatusPayment.Approved;
    }
}
