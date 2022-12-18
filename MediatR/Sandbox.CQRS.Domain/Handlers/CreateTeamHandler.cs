using MediatR;
using Sandbox.CQRS.Domain.Commands;
using Sandbox.CQRS.Domain.Contracts.Entities;
using Sandbox.CQRS.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandbox.CQRS.Domain.Handlers;

public class CreateTeamHandler : IRequestHandler<CreateTeamCommand, Team>
{
    private readonly IRepository<Team> repository;

    public CreateTeamHandler(IRepository<Team> repository)
    {
        this.repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }

    public Task<Team> Handle(CreateTeamCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
