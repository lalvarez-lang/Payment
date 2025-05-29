using AntiFraud.Api.Persistence;
using AntiFraud.Api.Services;
using AntiFraud.Domain.Entities;

namespace FraudServices.Test
{
    public class AntiFraudServiceTests
    {
            [Fact]
            public async Task ValidateTransactionAsync_ShouldRejectIfValueExceeds()
            {
                var repo = new InMemoryDailyLimitRepository();
                var service = new AntiFraudService(repo);

                var evt = new TransactionCreatedEvent
                {
                    Id = Guid.NewGuid(),
                    SourceAccountId = Guid.NewGuid(),
                    Value = 3000,
                    CreatedAt = DateTime.UtcNow
                };

                var result = await service.ValidateTransactionAsync(evt);
                Assert.Equal("rejected", result);
            }

            [Fact]
            public async Task ValidateTransactionAsync_ShouldRejectIfDailyLimitExceeded()
            {
                var repo = new InMemoryDailyLimitRepository();
                var service = new AntiFraudService(repo);

                var accountId = Guid.NewGuid();
                await repo.AddTransactionAsync(accountId, DateTime.UtcNow, 20000);

                var evt = new TransactionCreatedEvent
                {
                    Id = Guid.NewGuid(),
                    SourceAccountId = accountId,
                    Value = 100,
                    CreatedAt = DateTime.UtcNow
                };

                var result = await service.ValidateTransactionAsync(evt);
                Assert.Equal("rejected", result);
            }

            [Fact]
            public async Task ValidateTransactionAsync_ShouldApproveIfValid()
            {
                var repo = new InMemoryDailyLimitRepository();
                var service = new AntiFraudService(repo);

                var evt = new TransactionCreatedEvent
                {
                    Id = Guid.NewGuid(),
                    SourceAccountId = Guid.NewGuid(),
                    Value = 100,
                    CreatedAt = DateTime.UtcNow
                };

                var result = await service.ValidateTransactionAsync(evt);
                Assert.Equal("approved", result);
            }
        }
}