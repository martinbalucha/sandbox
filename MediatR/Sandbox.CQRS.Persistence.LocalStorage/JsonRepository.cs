using Sandbox.CQRS.Domain.Contracts.Entities;
using Sandbox.CQRS.Domain.Interfaces;
using System;
using System.Collections.Generic;
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
        this.file = file?? throw new ArgumentNullException(nameof(file));
    }

    public async Task<Team?> FindByIdAsync(Guid id)
    {
        string jsonString = await file.ReadAllTextAsync(configuration.FilePath!);

        var teams = JsonSerializer.Deserialize<IEnumerable<Team>>(jsonString) 
            ?? throw new Exception("An error occurred during the reading");

        return teams.FirstOrDefault(t => t.Id == id);
    }
}
