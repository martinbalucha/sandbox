using Sandbox.CQRS.Domain.Contracts.Entities;

namespace Sandbox.CQRS.Contracts.Interfaces;

public interface IRepository<T> where T : IEntity
{
    Task CreateAsync(T entity);

    Task UpdateAsync(T entity);

    Task<T?> FindByIdAsync(Guid id);

    Task<IEnumerable<T>> ListAsync();
}
