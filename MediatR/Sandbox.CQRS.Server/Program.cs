using Mapster;
using Sandbox.CQRS.Contracts.Interfaces;
using Sandbox.CQRS.Domain.Contracts.Entities;
using Sandbox.CQRS.Domain.Handlers;
using Sandbox.CQRS.Persistence.LocalStorage;
using Sandbox.CQRS.Server.Mapping;
using System.IO.Abstractions;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Configuration.AddJsonFile("LocalStoragePersistenceConfiguration.json");


builder.Services.AddMediatR(c =>
{
    c.Lifetime = ServiceLifetime.Singleton;
    c.RegisterServicesFromAssembly(typeof(CreateTeamHandler).Assembly);
});

// Handlers
builder.Services.AddSingleton<GetTeamHandler>();

// Persistence
var persistenceConfiguration = builder.Configuration.GetSection(nameof(LocalStoragePersistenceConfiguration)).Get<LocalStoragePersistenceConfiguration>();

builder.Services.AddSingleton(typeof(IFile), new FileWrapper(new FileSystem()));
builder.Services.AddSingleton(persistenceConfiguration);
builder.Services.AddScoped(typeof(IRepository<Team>), typeof(JsonRepository));

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
