namespace Sandbox.RabbitMQ.Contracts.Events;

public record CarStartedEvent(string Vin, string Brand, DateTimeOffset Time);
