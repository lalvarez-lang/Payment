using Microsoft.AspNetCore.Mvc;

namespace Transaction.Api.Services
{
    public interface ITransactionService
    {
        Task<Transaction.Domain.Entities.Transaction> CreateTransactionAsync(decimal amount);
    }
}
