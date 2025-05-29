using Confluent.Kafka;
using Microsoft.AspNetCore.Mvc;
using Transaction.Api.Repositories;
using TransactionService.Infrastructure.Kafka;

namespace Transaction.Api.Services
{
    public class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _transactionRepository;
        private readonly IKafkaProducer _kafkaProducer;

        public TransactionService(ITransactionRepository transactionRepository, IKafkaProducer kafkaProducer)
        {
            _transactionRepository = transactionRepository;
            _kafkaProducer = kafkaProducer;
        }

        public async Task<Transaction.Domain.Entities.Transaction> CreateTransactionAsync(decimal amount)
        {
            if (amount <= 2000)
            {
                throw new ArgumentException("The transaction amount must be greater than 2000.");
            }

            var transaction = new Transaction.Domain.Entities.Transaction { Value = amount, SourceAccountId=new Guid(), TargetAccountId=new Guid(), CreatedAt= DateTime.UtcNow, TransferTypeId=1, Id=new Guid(), Status=Domain.Enum.StatusPayment.Pending };
            await _transactionRepository.AddAsync(transaction);
            await _kafkaProducer.PublishTransactionCreatedAsync($"Transaction created: {transaction.Id}, Amount: {transaction.Value}");
            return transaction;
        }

    }
}

