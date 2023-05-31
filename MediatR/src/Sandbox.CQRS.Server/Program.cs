using Serilog;
using Mapster;
using Sandbox.CQRS.Contracts.Interfaces;
using Sandbox.CQRS.Domain.Contracts.Entities;
using Sandbox.CQRS.Domain.Handlers;
using Sandbox.CQRS.Persistence.LocalStorage;
using Sandbox.CQRS.Server.Mapping;
using System.IO.Abstractions;
using System.Reflection;
using MediatR;
using Sandbox.CQRS.Domain.PipelineBehaviors;
using FluentValidation;
using Sandbox.CQRS.Domain.Commands;
using Sandbox.CQRS.Domain.Validators;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Configuration.AddJsonFile("LocalStoragePersistenceConfiguration.json");

builder.Logging.ClearProviders();
builder.Host.UseSerilog((context, service, configuration) =>
{
    configuration.WriteTo.Console();
});

builder.Services.AddMediatR(c =>
{
    c.Lifetime = ServiceLifetime.Singleton;
    c.RegisterServicesFromAssembly(typeof(CreateTeamHandler).Assembly);
});

// Persistence
var persistenceConfiguration = builder.Configuration.GetSection(nameof(LocalStoragePersistenceConfiguration)).Get<LocalStoragePersistenceConfiguration>()
                                ?? throw new ArgumentException("Bad configuration");

builder.Services.AddSingleton(typeof(IFile), new FileWrapper(new FileSystem()));
builder.Services.AddSingleton(persistenceConfiguration);
builder.Services.AddScoped(typeof(IRepository<Team>), typeof(JsonRepository));
builder.Services.AddSingleton(typeof(IValidator<UpdateTeamCommand>), typeof(UpdateTeamCommandValidator));
builder.Services.AddTransient(typeof(IPipelineBehavior<UpdateTeamCommand, Unit>), typeof(ValidationBehavior<UpdateTeamCommand, Unit>));

builder.WebHost.UseKestrel(o => o.AllowAlternateSchemes = true);

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

var configurationServiceAssembly = Assembly.GetAssembly(typeof(MappingConfiguration));
if (configurationServiceAssembly != null)
{
    TypeAdapterConfig.GlobalSettings.Scan(configurationServiceAssembly);
}

app.UseAuthorization();

app.MapControllers();

app.Run();
