using Confluent.Kafka;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace TransactionService.Infrastructure.Kafka;

public interface IKafkaProducer
{
    Task PublishTransactionCreatedAsync(string message);
}

public class KafkaProducer : IKafkaProducer
{
    public readonly string _bootstrapServers;
    public readonly string _topic;


    public KafkaProducer(IConfiguration config)
    {
        _bootstrapServers = config["Kafka:BootstrapServers"] ?? "localhost:9092";
        _topic = config["Kafka:Topic"]?? "financial-transactions";
    }
    public async Task PublishTransactionCreatedAsync(string message)
    {
        var config = new ProducerConfig { BootstrapServers = _bootstrapServers,
            Acks = Acks.All, 
            EnableIdempotence = true };
        using var producer = new ProducerBuilder<Null, string>(config).Build();
        await producer.ProduceAsync(_topic, new Message<Null, string> { Value = message });
    }
}
