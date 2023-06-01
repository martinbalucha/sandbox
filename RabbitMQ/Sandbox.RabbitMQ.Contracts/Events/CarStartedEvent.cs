namespace Sandbox.RabbitMQ.Contracts.Events;

public record CarStartedEvent(string Vin, DateTimeOffset Time);
