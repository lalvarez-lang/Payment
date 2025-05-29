using Transaction.Domain.Enum;

namespace Transaction.Api.Repositories
{
    public interface ITransactionRepository
    {
        Task AddAsync(Transaction.Domain.Entities.Transaction transaction);

        Task<Transaction.Domain.Entities.Transaction?> GetByIdAsync(Guid id);

        Task UpdateStatusAsync(Guid id, StatusPayment status);

    }
}
