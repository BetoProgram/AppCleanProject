using AppCleanProject.Application.Commons.Exceptions;
using AppCleanProject.Application.Commons.Interfaces;
using AppCleanProject.Domain.Entities;
using Cortex.Mediator.Commands;
using FluentValidation;
using Mapster;
using System.Net;

namespace AppCleanProject.Application.Features.FServices.Commands
{
    public record UpdateServiceCommand(int Id, string Name, string? Description,
    int DurationMinutes, decimal Price) : ICommand;

    public class UpdateServiceCommandValidator: AbstractValidator<UpdateServiceCommand>
    {
        public UpdateServiceCommandValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Description).NotEmpty();
            RuleFor(x => x.DurationMinutes).NotEmpty();
            RuleFor(x => x.Price).NotEmpty();
        }
    }

    public class UpdateServiceCommandHandler(IRepositoryAsync<Services> repositoryAsync) : ICommandHandler<UpdateServiceCommand>
    {
        private readonly IRepositoryAsync<Services> _repositoryAsync = repositoryAsync;

        public async Task Handle(UpdateServiceCommand command, CancellationToken cancellationToken)
        {
            var serviceFoundDb =  await _repositoryAsync.GetByIdAsync(command.Id);

            if (serviceFoundDb is null)
            {
                throw new CustomException(HttpStatusCode.NotFound, new {  message = "Not found service" });
            }

            var service = command.Adapt<Services>();

            await _repositoryAsync.UpdateAsync(service);
        }
    }
}
