using Microsoft.AspNetCore.Mvc;
using AntiFraud.Domain.Entities;
using AntiFraud.Api.UseCases;

[ApiController]
[Route("api/[controller]")]
public class AntiFraudController : ControllerBase
{
    private readonly ValidateTransaction _validateTransaction;

    public AntiFraudController(ValidateTransaction validateTransaction)
    {
        _validateTransaction = validateTransaction;
    }

    [HttpPost("validate")]
    public async Task<ActionResult<AntiFraudResult>> Validate([FromBody] TransactionCreatedEvent request)
    {
        var result = await _validateTransaction.ExecuteAsync(request);
        return Ok(result);
    }
}

public class ValidateRequest
{
    public Guid TransactionId { get; set; }
    public decimal Amount { get; set; }
    public string? Creditor { get; set; }
}