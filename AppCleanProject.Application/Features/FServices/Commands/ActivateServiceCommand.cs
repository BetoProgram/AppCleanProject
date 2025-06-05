using AppCleanProject.Application.Commons.Exceptions;
using AppCleanProject.Application.Commons.Interfaces;
using AppCleanProject.Domain.Entities;
using Cortex.Mediator.Commands;
using FluentValidation;
using System.Net;

namespace AppCleanProject.Application.Features.FServices.Commands
{
    public record ActivateServiceCommand(int Id, bool IsActive): ICommand;

    public class ActivateServiceCommandValidator: AbstractValidator<ActivateServiceCommand>
    {
        public ActivateServiceCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.IsActive).NotEmpty();
        }
    }

    public class ActivateServiceCommandHandler(IRepositoryAsync<Services> repositoryAsync) : ICommandHandler<ActivateServiceCommand>
    {
        private readonly IRepositoryAsync<Services> _repositoryAsync = repositoryAsync;

        public async Task Handle(ActivateServiceCommand command, CancellationToken cancellationToken)
        {
            var serviceFoundDb =  await _repositoryAsync.GetByIdAsync(command.Id, cancellationToken);

            if (serviceFoundDb is null)
            {
                throw new CustomException(HttpStatusCode.NotFound, new { message = "Not found service" });
            }
            serviceFoundDb.IsActive = command.IsActive;
            await _repositoryAsync.UpdateAsync(serviceFoundDb);
        }
    }
}
