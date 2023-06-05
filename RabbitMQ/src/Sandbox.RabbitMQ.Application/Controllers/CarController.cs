using Microsoft.AspNetCore.Mvc;
using Sandbox.RabbitMQ.Application.Dtos;
using Sandbox.RabbitMQ.Contracts;
using Sandbox.RabbitMQ.Contracts.Events;

namespace Sandbox.RabbitMQ.Application.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CarController : ControllerBase
{
    private readonly IMessageBus messageBus;

    public CarController(IMessageBus messageBus)
    {
        this.messageBus = messageBus;
    }

    [HttpPost]
    public async Task<IActionResult> Start([FromBody] CarStartDto carStart)
    {
        var carStartedEvent = new CarStartedEvent(carStart.Vin, DateTimeOffset.Now);

        await messageBus.PublishAsync(carStartedEvent);

        return Ok();
    }
}
