using System.Threading.Tasks;
using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

public class TransactionServiceTests
{
    [Fact]
    public async Task CreateTransactionAsync_ShouldSaveAndPublish()
    {
        var repo = new InMemoryTransactionRepository();
        var kafkaMock = new Mock<IKafkaProducer>();
        var service = new TransactionService(repo, kafkaMock.Object);

        var tx = new Transaction.Domain.Entities.Transaction
        {
            SourceAccountId = Guid.NewGuid(),
            TargetAccountId = Guid.NewGuid(),
            TransferTypeId = 1,
            Value = 100
        };

        var id = await service.CreateTransactionAsync(tx);
        var saved = await repo.GetByIdAsync(id);

        Assert.NotNull(saved);
        kafkaMock.Verify(k => k.PublishTransactionCreatedAsync(It.IsAny<TransactionService.Domain.Entities.Transaction>()), Times.Once);
    }
}
