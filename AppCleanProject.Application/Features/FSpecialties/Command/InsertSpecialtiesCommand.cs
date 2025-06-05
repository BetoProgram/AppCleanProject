using AppCleanProject.Application.Commons.Interfaces;
using AppCleanProject.Domain.Entities;
using Cortex.Mediator.Commands;
using FluentValidation;
using Mapster;

namespace AppCleanProject.Application.Features.FSpecialties.Command;
public record InsertSpecialtiesCommand(string Name, string Description):ICommand;

public class InsertSpecialtiesCommandValidator:AbstractValidator<InsertSpecialtiesCommand>
{
    public InsertSpecialtiesCommandValidator()
    {
        RuleFor(x => x.Name).NotNull().NotEmpty();
        RuleFor(x => x.Description).NotNull().NotEmpty();
    }
}

public class InsertSpecialtiesCommandHandler(IRepositoryAsync<Specialties> repo)
    :ICommandHandler<InsertSpecialtiesCommand>
{
    public async Task Handle(InsertSpecialtiesCommand command, CancellationToken cancellationToken)
    {
        var spec = command.Adapt<Specialties>();
        await repo.AddAsync(spec, cancellationToken);
    }
}