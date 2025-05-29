using AntiFraud.Api.Kafka;
using AntiFraud.Api.Persistence;
using AntiFraud.Api.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<IDailyLimitRepository, InMemoryDailyLimitRepository>();

builder.Services.AddSingleton<IAntiFraudService, AntiFraudService>();

builder.Services.AddSingleton<IKafkaProducer, KafkaProducer>();

builder.Services.AddHostedService<KafkaConsumer>();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();

builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configura el middleware de Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();