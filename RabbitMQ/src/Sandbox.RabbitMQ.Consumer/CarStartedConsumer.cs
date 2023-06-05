using MassTransit;
using Sandbox.RabbitMQ.Contracts.Events;
using Serilog;

namespace Sandbox.RabbitMQ.Consumer;

public class CarStartedConsumer : IConsumer<CarStartedEvent>
{
    private readonly ILogger logger;

    public CarStartedConsumer(ILogger logger)
    {
        this.logger = logger;
    }

    public Task Consume(ConsumeContext<CarStartedEvent> context)
    {
        logger.Information("A car with VIN '{@Vin}' has been started at {@StartTime}", context.Message.Vin, 
            context.Message.Time.ToString("dd:MM:yyyy hh:mm:ss"));

        return Task.CompletedTask;
    }
}
