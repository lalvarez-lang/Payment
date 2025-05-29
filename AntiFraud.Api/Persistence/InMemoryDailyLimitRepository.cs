using AntiFraud.Api.Services;

namespace AntiFraud.Api.Persistence
{
    public class InMemoryDailyLimitRepository : IDailyLimitRepository
    {
        private readonly Dictionary<(Guid, DateTime), decimal> _dailyTotals = new();

        public Task<decimal> GetDailyTotalAsync(Guid accountId, DateTime date)
        {
            _dailyTotals.TryGetValue((accountId, date.Date), out var total);
            return Task.FromResult(total);
        }

        public Task AddTransactionAsync(Guid accountId, DateTime date, decimal value)
        {
            var key = (accountId, date.Date);
            if (_dailyTotals.ContainsKey(key))
                _dailyTotals[key] += value;
            else
                _dailyTotals[key] = value;
            return Task.CompletedTask;
        }
    }
}
