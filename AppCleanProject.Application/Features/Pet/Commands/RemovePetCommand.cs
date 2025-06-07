using AppCleanProject.Application.Commons.Exceptions;
using AppCleanProject.Application.Commons.Interfaces;
using AppCleanProject.Domain.Entities;
using Cortex.Mediator.Commands;
using System.Net;


namespace AppCleanProject.Application.Features.Pet.Commands
{
    public class RemovePetCommand:ICommand
    {
        public long Id { get; set; }
    }

    public class RemovePetCommandHandler(IRepositoryAsync<Pets> repositoryAsync) : ICommandHandler<RemovePetCommand>
    {
        public async Task Handle(RemovePetCommand command, CancellationToken cancellationToken)
        {
            var petFoundDb = await repositoryAsync.GetByIdAsync(command.Id, cancellationToken);

            if(petFoundDb is null)
            {
                throw new CustomException(HttpStatusCode.NotFound,
                    new { message = "Not found Pet" });
            }

            int appointments = petFoundDb.Appointments.Count;

            if(appointments > 0)
            {
                throw new CustomException(HttpStatusCode.BadRequest,
                    new {  message = "Pet has associated appointments" });
            }

            await repositoryAsync.DeleteAsync(petFoundDb, cancellationToken);
        }
    }
}
