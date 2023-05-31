using MassTransit;
using Sandbox.RabbitMQ.MessagingConfiguration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.Configure<MessageBrokerConfiguration>(
    builder.Configuration.GetSection(nameof(MessageBrokerConfiguration)));

builder.Services.AddMassTransit(busRegistrationConfigurator =>
{
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

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
