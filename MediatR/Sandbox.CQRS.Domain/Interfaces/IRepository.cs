using Sandbox.CQRS.Domain.Contracts.Entities;

namespace Sandbox.CQRS.Domain.Interfaces;

public interface IRepository<T> where T : IEntity
{
    Task<T?> FindByIdAsync(Guid id);
}
