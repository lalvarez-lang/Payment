using AntiFraud.Domain.Entities;

namespace AntiFraud.Api.Services
{

    public interface IAntiFraudService
    {
        Task<string> ValidateTransactionAsync(TransactionCreatedEvent evt);
    }
}
