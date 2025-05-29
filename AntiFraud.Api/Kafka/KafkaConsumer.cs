using AntiFraud.Api.Kafka;
using AntiFraud.Api.Services;
using AntiFraud.Domain.Entities;
using Confluent.Kafka;
using System.Text.Json;

namespace AntiFraud.Api.Kafka
{
   
    public class KafkaConsumer : BackgroundService
    {
        private readonly string _bootstrapServers;
        private readonly string _topic = "transaction-created";
        private readonly IAntiFraudService _antiFraudService;
        private readonly IKafkaProducer _kafkaProducer;

        public KafkaConsumer(IConfiguration config, IAntiFraudService antiFraudService, IKafkaProducer kafkaProducer)
        {
            _bootstrapServers = config["Kafka:BootstrapServers"] ?? "localhost:9092";
            _antiFraudService = antiFraudService;
            _kafkaProducer = kafkaProducer;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            var config = new ConsumerConfig
            {
                BootstrapServers = _bootstrapServers,
                GroupId = "antifraud-group",
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            using var consumer = new ConsumerBuilder<Ignore, string>(config).Build();
            consumer.Subscribe(_topic);

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    var cr = consumer.Consume(stoppingToken);
                    var evt = JsonSerializer.Deserialize<TransactionCreatedEvent>(cr.Message.Value);
                    if (evt != null)
                    {
                        var status = await _antiFraudService.ValidateTransactionAsync(evt);
                        await _kafkaProducer.PublishStatusAsync(evt.Id, status.ToString());
                    }
                }
                catch (OperationCanceledException) { }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error: {ex.Message}");
                }
            }
        }
    }
}
    
