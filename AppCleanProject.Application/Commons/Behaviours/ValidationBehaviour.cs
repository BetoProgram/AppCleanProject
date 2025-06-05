
using Cortex.Mediator.Commands;
using FluentValidation;

using ValidationException = AppCleanProject.Application.Commons.Exceptions.ValidationException;

namespace AppCleanProject.Application.Commons.Behaviours;
public class ValidationBehaviour<TCommand> : ICommandPipelineBehavior<TCommand>
    where TCommand : ICommand
{
    private readonly IEnumerable<IValidator<TCommand>> _validators;

    public ValidationBehaviour(IEnumerable<IValidator<TCommand>> validators)
    {
        _validators = validators;
    }

    public async Task Handle(TCommand command, CommandHandlerDelegate next, CancellationToken cancellationToken)
    {
        if (_validators.Any())
        {
            var context = new ValidationContext<ICommand>(command);

            var validationResult = await Task.WhenAll(
                _validators.Select(v => v.ValidateAsync(context, cancellationToken))
            );

            var failures = validationResult.Where(r => r.Errors.Any())
                .SelectMany(r => r.Errors)
                .ToList();

            if (failures.Any())
            {
                throw new ValidationException(failures);
            }
            
        }
        await next();
    }
}

