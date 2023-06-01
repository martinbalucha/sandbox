namespace Sandbox.RabbitMQ.Contracts;

public interface IMessageBus
{
    Task PublishAsync<T>(T message, CancellationToken cancellationToken = default);
}
