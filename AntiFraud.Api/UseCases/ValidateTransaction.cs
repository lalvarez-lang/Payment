using AntiFraud.Api.Services;
using AntiFraud.Domain.Entities;

namespace AntiFraud.Api.UseCases
{
    public class ValidateTransaction
    {
        private readonly IAntiFraudValidator _validator;

        public ValidateTransaction(IAntiFraudValidator validator)
        {
            _validator = validator;
        }

        public async Task<AntiFraudResult> ExecuteAsync(TransactionCreatedEvent ev)
        {
            return await _validator.ValidateAsync(ev);
        }
    }
}
