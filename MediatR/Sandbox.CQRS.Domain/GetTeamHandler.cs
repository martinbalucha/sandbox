using MediatR;
using Sandbox.CQRS.Domain.Contracts.Entities;
using Sandbox.CQRS.Domain.Interfaces;
using Sandbox.CQRS.Domain.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandbox.CQRS.Domain;

public class GetTeamHandler : IRequestHandler<GetTeamQuery, Team>
{
    private readonly IRepository<Team> repository;

    public GetTeamHandler(IRepository<Team> repository)
    {
        this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public async Task<Team> Handle(GetTeamQuery request, CancellationToken cancellationToken)
    {
        return await repository.FindByIdAsync(request.TeamId);
    }
}
