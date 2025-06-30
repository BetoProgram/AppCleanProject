using System.Net;
using AppCleanProject.Application.Commons.Exceptions;
using AppCleanProject.Application.Commons.Interfaces;
using AppCleanProject.Domain.Entities;
using Cortex.Mediator.Commands;

namespace AppCleanProject.Application.Features.FVeterinarian.Commands
{
    public record ActivateVeterinarianCommand(int Id, bool Activate):ICommand;

    public class ActivateVeterinarianCommandHandler(IRepositoryAsync<Veterinarians> repositoryAsync)
    : ICommandHandler<ActivateVeterinarianCommand>
    {
        public async Task Handle(ActivateVeterinarianCommand command, CancellationToken cancellationToken)
        {
            var veterinarianDb = await repositoryAsync.GetByIdAsync(command.Id, cancellationToken);

            if (veterinarianDb is null)
            {
                throw new CustomException(HttpStatusCode.NotFound, new { message = "Not found item" });
            }

            veterinarianDb.Activate = command.Activate;
            await repositoryAsync.UpdateAsync(veterinarianDb, cancellationToken);
        }
    }
}