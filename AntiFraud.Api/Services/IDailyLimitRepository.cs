namespace AntiFraud.Api.Services
{
    public interface IDailyLimitRepository
    {
        Task<decimal> GetDailyTotalAsync(Guid accountId, DateTime date);
        Task AddTransactionAsync(Guid accountId, DateTime date, decimal value);
    }
}
