using AntiFraud.Domain.Entities;
using Confluent.Kafka;
using System.Text.Json;

namespace AntiFraud.Api.Kafka
{
    public interface IKafkaProducer
    {
        Task PublishStatusAsync(Guid transactionId, string status);
    }
    public class KafkaProducer : IKafkaProducer
    {
        private readonly string _bootstrapServers;
        private readonly string _topic = "transaction-status-updated";

        public KafkaProducer(IConfiguration config)
        {
            _bootstrapServers = config["BootstrapServers"] ?? "localhost:9092";
        }

        public async Task PublishStatusAsync(Guid transactionId, string status)
        {
            var config = new ProducerConfig { BootstrapServers = _bootstrapServers };
            using var producer = new ProducerBuilder<Null, string>(config).Build();
            var message = JsonSerializer.Serialize(new TransactionStatusEvent
            {
                TransactionId = transactionId,
                Status = Transaction.Domain.Enum.StatusPayment.Approved 
            });
            await producer.ProduceAsync(_topic, new Message<Null, string> { Value = message });
        }
    }
}
