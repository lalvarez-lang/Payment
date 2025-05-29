using Transaction.Domain.Enum;

namespace Transaction.Domain.Entities
{
    public class Transaction
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid SourceAccountId { get; set; }
        public Guid TargetAccountId { get; set; }
        public int TransferTypeId { get; set; }
        public decimal Value { get; set; }
        public StatusPayment Status { get; set; } = StatusPayment.Pending;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
