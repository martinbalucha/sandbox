using MediatR;
using Sandbox.CQRS.Domain.Contracts.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandbox.CQRS.Domain.Events;

public record TeamCreatedNotification(Team Team) : INotification;
