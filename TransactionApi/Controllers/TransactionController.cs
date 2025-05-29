using Confluent.Kafka;
using Microsoft.AspNetCore.Mvc;
using Transaction.Api.Services;
using TransactionService.Infrastructure.Kafka;

namespace Transaction.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TransactionsController : ControllerBase
    {
        private readonly ITransactionService _transactionService;
        private readonly IKafkaProducer _kafkaProducer;

        public TransactionsController(ITransactionService transactionService, IKafkaProducer kafkaProducer )
        {
            _transactionService = transactionService;
            _kafkaProducer = kafkaProducer;
        }

        [HttpPost]
        public async Task<IActionResult> CreateTransaction([FromBody] decimal amount)
        {
            try
            {
                var transaction = await _transactionService.CreateTransactionAsync(amount);
                return CreatedAtAction(nameof(CreateTransaction), new { id = transaction.Id }, transaction);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
