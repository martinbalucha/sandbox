using Mapster;
using Sandbox.CQRS.Server.Mapping;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Configuration.AddJsonFile("LocalStoragePersistenceConfiguration.json");

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

var configurationServiceAssembly = Assembly.GetAssembly(typeof(MappingConfiguration));
if (configurationServiceAssembly != null)
{
    TypeAdapterConfig.GlobalSettings.Scan(configurationServiceAssembly);
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
