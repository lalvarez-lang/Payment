using FinancialTransactionApi.Data;
using Microsoft.EntityFrameworkCore;
using Moq;
using Transaction.Api.Repositories;
using TransactionService.Infrastructure.Kafka;

public class TransactionServiceTests
{
    [Fact]
    public async Task CreateTransactionAsync_ShouldGenerateErrorMessage()
    {
        // Arrange

        var options = new DbContextOptionsBuilder<TransactionDbContext>()
          .UseInMemoryDatabase(databaseName: "FinancialDb")
          .Options;

        using var mockDataContext = new TransactionDbContext(options);
        var kafkaMock = new Mock<IKafkaProducer>();
        var repo = new TransactionRepository(mockDataContext);
        var service = new Transaction.Api.Services.TransactionService(repo, kafkaMock.Object);

        var tx = new Transaction.Domain.Entities.Transaction
        {
            SourceAccountId = Guid.NewGuid(),
            TargetAccountId = Guid.NewGuid(),
            TransferTypeId = 1,
            Value = 100
        };
  
        // Assert
        var exception = await Assert.ThrowsAsync<ArgumentException>(() => service.CreateTransactionAsync(tx.Value));
        Assert.Equal("The transaction amount must be greater than 2000.", exception.Message);
    }

    [Fact]
    public async Task CreateTransactionAsync_ShouldGenerateTransaction()
    {
        // Arrange

        var options = new DbContextOptionsBuilder<TransactionDbContext>()
          .UseInMemoryDatabase(databaseName: "FinancialDb") 
          .Options;

        using var mockDataContext = new TransactionDbContext(options);
        var kafkaMock = new Mock<IKafkaProducer>();
        var repo = new TransactionRepository(mockDataContext);
        var service = new Transaction.Api.Services.TransactionService(repo, kafkaMock.Object);

        var tx = new Transaction.Domain.Entities.Transaction 
        {
            SourceAccountId = Guid.NewGuid(),
            TargetAccountId = Guid.NewGuid(),
            TransferTypeId = 1,
            Value = 3000
        };

        // Act
        var id = await service.CreateTransactionAsync(tx.Value);

        // Assert
        Assert.NotNull(id);
        kafkaMock.Verify(k => k.PublishTransactionCreatedAsync(It.IsAny<string>()), Times.Once);
    }
}