using AntiFraud.Domain.Entities;

namespace AntiFraud.Api.Services
{
    public class AntiFraudService : IAntiFraudService
    {
        private readonly IDailyLimitRepository _repository;
        private const decimal MaxValue = 2000;
        private const decimal MaxDaily = 20000;

        public AntiFraudService(IDailyLimitRepository repository)
        {
            _repository = repository;
        }

        public async Task<string> ValidateTransactionAsync(TransactionCreatedEvent evt)
        {
            if (evt.Value > MaxValue)
                return "rejected";

            var dailyTotal = await _repository.GetDailyTotalAsync(evt.SourceAccountId, evt.CreatedAt);
            if (dailyTotal + evt.Value > MaxDaily)
                return "rejected";

            await _repository.AddTransactionAsync(evt.SourceAccountId, evt.CreatedAt, evt.Value);
            return "approved";
        }
    }
}
