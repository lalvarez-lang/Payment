using Microsoft.EntityFrameworkCore;
namespace FinancialTransactionApi.Data
{
    public class TransactionDbContext : DbContext
    {
        public TransactionDbContext(DbContextOptions<TransactionDbContext> options) : base(options) { }

        public DbSet<Transaction.Domain.Entities.Transaction> Transactions { get; set; }
    }
}