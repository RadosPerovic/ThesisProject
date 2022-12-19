using FluentValidation;
using MediatR;
using ThesisProject.Application.Exceptions;

namespace ThesisProject.Application.Behavior;
public class PipelineValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public PipelineValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        var context = new ValidationContext<TRequest>(request);
        var results = await Task.WhenAll(_validators.Select(e => e.ValidateAsync(context, cancellationToken)));
        var failures = results
            .SelectMany(vr => vr.Errors)
            .Where(error => error != null)
            .ToList();

        if (failures.Any())
        {
            var messages = failures
                .Select(e => e.ErrorMessage)
                .Aggregate((first, second) => first + ";" + second);

            throw new ApplicationError(messages);
        }

        return await next();
    }
}
