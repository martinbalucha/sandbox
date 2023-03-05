using Sandbox.CQRS.Contracts.Interfaces;
using Sandbox.CQRS.Domain.Contracts.Entities;
using System.IO.Abstractions;
using System.Text.Json;

namespace Sandbox.CQRS.Persistence.LocalStorage;

public class JsonRepository : IRepository<Team>
{
    private readonly LocalStoragePersistenceConfiguration configuration;
    private readonly IFile file;

    public JsonRepository(LocalStoragePersistenceConfiguration configuration, IFile file)
    {
        this.configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        this.file = file ?? throw new ArgumentNullException(nameof(file));
    }

    public async Task CreateAsync(Team entity)
    {
        string jsonString = await file.ReadAllTextAsync(configuration.FilePath!);

        var teams = JsonSerializer.Deserialize<List<Team>>(jsonString)
            ?? throw new Exception("An error occurred during the file deserialization.");

        teams.Add(entity);

        jsonString = JsonSerializer.Serialize(teams, new JsonSerializerOptions { WriteIndented = true });
        await file.WriteAllTextAsync(configuration.FilePath!, jsonString);
    }

    public async Task<Team?> FindByIdAsync(Guid id)
    {
        string jsonString = await file.ReadAllTextAsync(configuration.FilePath!);

        var teams = JsonSerializer.Deserialize<IEnumerable<Team>>(jsonString) 
            ?? throw new Exception("An error occurred during the file deserialization.");

        return teams.FirstOrDefault(t => t.Id == id);
    }
}
