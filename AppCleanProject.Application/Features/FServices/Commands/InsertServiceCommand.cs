using AppCleanProject.Application.Commons.Interfaces;
using AppCleanProject.Domain.Entities;
using Cortex.Mediator.Commands;
using FluentValidation;
using Mapster;

namespace AppCleanProject.Application.Features.FServices.Commands
{
    public record InsertServiceCommand(string Name,string? Description,
    int DurationMinutes,decimal Price) : ICommand;

    public class InsertServiceCommandValidator: AbstractValidator<InsertServiceCommand>
    {
        public InsertServiceCommandValidator()
        {
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Description).NotEmpty();
            RuleFor(x => x.DurationMinutes).NotEmpty();
            RuleFor(x => x.Price).NotEmpty();
        }
    }

    public class InsertServiceHandler(IRepositoryAsync<Services> repositoryAsync) : ICommandHandler<InsertServiceCommand>
    {
        public async Task Handle(InsertServiceCommand command, CancellationToken cancellationToken)
        {
            var service = command.Adapt<Services>();
            service.IsActive = true;
            await repositoryAsync.AddAsync(service, cancellationToken);
        }
    }


}
