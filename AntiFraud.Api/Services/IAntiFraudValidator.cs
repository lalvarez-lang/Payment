using AntiFraud.Domain.Entities;

namespace AntiFraud.Api.Services
{
    public interface IAntiFraudValidator
    {
        Task<AntiFraudResult> ValidateAsync(TransactionCreatedEvent evt);
    }
}
