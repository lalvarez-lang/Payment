using FinancialTransactionApi.Data;
using Transaction.Domain.Enum;

namespace Transaction.Api.Repositories
{
    public class TransactionRepository: ITransactionRepository 
    {
        private readonly TransactionDbContext _context;

        public TransactionRepository(TransactionDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Transaction.Domain.Entities.Transaction transaction)
        {
            await _context.Transactions.AddAsync(transaction);
            await _context.SaveChangesAsync();
        }

        public async Task<Domain.Entities.Transaction?> GetByIdAsync(Guid id)
        {
            return await _context.Transactions.FindAsync(id);
        }

        public async Task UpdateStatusAsync(Guid id, StatusPayment status)
        {
            var tx = await _context.Transactions.FindAsync(id);
            if (tx != null)
            {
                tx.Status = status;
                await _context.SaveChangesAsync();
            }
        }
    }
}
