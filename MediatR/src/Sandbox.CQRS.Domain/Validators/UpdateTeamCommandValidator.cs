using FluentValidation;
using Sandbox.CQRS.Domain.Commands;

namespace Sandbox.CQRS.Domain.Validators;

public class UpdateTeamCommandValidator : AbstractValidator<UpdateTeamCommand>
{
	public UpdateTeamCommandValidator()
	{
		RuleFor(c => c.Name)
			.NotEmpty()
			.NotEmpty();
	}
}
