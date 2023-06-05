using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Sandbox.RabbitMQ.Consumer;
using Sandbox.RabbitMQ.Consumer.MessagingConfiguration;

IConfiguration config = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json")
    .AddEnvironmentVariables()
    .Build();

var host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.Configure<MessageBrokerConfiguration>(
            config.GetSection(nameof(MessageBrokerConfiguration)));

        services.AddMassTransit(busRegistrationConfigurator =>
        {
            busRegistrationConfigurator.AddConsumer<CarStartedConsumer>();

            busRegistrationConfigurator.SetKebabCaseEndpointNameFormatter();

            busRegistrationConfigurator.UsingRabbitMq((context, configurator) =>
            {
                var brokerConfiguration = context.GetRequiredService<MessageBrokerConfiguration>();

                configurator.Host(new Uri(brokerConfiguration.Host), h =>
                {
                    h.Username(brokerConfiguration.Username);
                    h.Password(brokerConfiguration.Password);
                });

            });

        });
    }).Build();

host.Run();


