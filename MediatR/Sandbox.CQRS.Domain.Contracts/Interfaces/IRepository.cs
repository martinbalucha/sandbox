using Sandbox.CQRS.Domain.Contracts.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandbox.CQRS.Domain.Contracts.Interfaces;
public interface IRepository<T> where T : IEntity
{
    Task<T> FindByIdAsync(Guid id);
}
