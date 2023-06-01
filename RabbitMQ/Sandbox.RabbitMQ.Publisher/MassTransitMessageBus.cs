using MassTransit;
using Sandbox.RabbitMQ.Contracts;

namespace Sandbox.RabbitMQ.Publisher;

public class MassTransitMessageBus : IMessageBus
{
    private readonly IPublishEndpoint publishEndpoint;

    public MassTransitMessageBus(IPublishEndpoint publishEndpoint)
    {
        this.publishEndpoint = publishEndpoint ?? throw new ArgumentNullException(nameof(publishEndpoint));
    }

    public Task PublishAsync<T>(T message, CancellationToken cancellationToken = default)
    {
        return publishEndpoint.Publish(message, cancellationToken);
    }
}
