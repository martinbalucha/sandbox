using MediatR;
using Sandbox.CQRS.Contracts.Interfaces;
using Sandbox.CQRS.Domain.Contracts.Entities;
using Sandbox.CQRS.Domain.Queries;

namespace Sandbox.CQRS.Domain.Handlers;

public class ListTeamsHandler : IRequestHandler<ListTeamsQuery, IEnumerable<Team>>
{
    private readonly IRepository<Team> repository;

    public ListTeamsHandler(IRepository<Team> repository)
    {
        this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task<IEnumerable<Team>> Handle(ListTeamsQuery request, CancellationToken cancellationToken)
    {
        return await repository.ListAsync();
    }
}
