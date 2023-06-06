using MassTransit;
using Microsoft.Extensions.Options;
using Sandbox.RabbitMQ.Application.MessagingConfiguration;
using Sandbox.RabbitMQ.Contracts;
using Sandbox.RabbitMQ.Contracts.Events;
using Sandbox.RabbitMQ.Publisher;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// RabbitMQ
builder.Services.Configure<MessageBrokerConfiguration>(

    builder.Configuration.GetSection(nameof(MessageBrokerConfiguration)));

builder.Services.AddMassTransit(busRegistrationConfigurator =>
{
    busRegistrationConfigurator.SetKebabCaseEndpointNameFormatter();

    busRegistrationConfigurator.UsingRabbitMq((context, configurator) =>
    {
        var brokerConfiguration = context.GetRequiredService<IOptions<MessageBrokerConfiguration>>();

        configurator.Host(new Uri(brokerConfiguration.Value.Host), h =>
        {
            h.Username(brokerConfiguration.Value.Username);
            h.Password(brokerConfiguration.Value.Password);
        });

        configurator.Message<CarStartedEvent>(x =>
        {
            x.SetEntityName(nameof(CarStartedEvent));
        });
        configurator.Publish<CarStartedEvent>(x =>
        {
            x.Durable = true;
            x.ExchangeType = "direct";
        });

        configurator.Send<CarStartedEvent>(x =>
        {
            x.UseRoutingKeyFormatter(ctx => ctx.Message.Brand);
        });
    });
});

builder.Services.AddTransient<IMessageBus, MassTransitMessageBus>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
