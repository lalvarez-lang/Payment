using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Transaction.Domain.Enum;

namespace AntiFraud.Domain.Entities
{
    public class AntiFraudResult
    {
        public StatusPayment Status { get; set; } = StatusPayment.Approved;
        public string? Reason { get; set; }
    }
}
