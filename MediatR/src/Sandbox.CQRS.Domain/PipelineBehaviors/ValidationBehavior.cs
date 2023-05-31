using FluentValidation;
using MediatR;

namespace Sandbox.CQRS.Domain.PipelineBehaviors;

public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private const char ErrorMessageSeparator = ' ';

    private readonly IValidator<TRequest> validator;

    public ValidationBehavior(IValidator<TRequest> validator)
    {
        this.validator = validator ?? throw new ArgumentNullException(nameof(validator));
    }

    public Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var validationResult = validator.Validate(request);
        if (!validationResult.IsValid)
        {
            throw new ValidationException($"The request does not satisfy the following conditions: " +
                $"{string.Join(ErrorMessageSeparator, validationResult.Errors)}");
        }

        return next();
    }
}
