using AppCleanProject.Application.Commons.Exceptions;
using AppCleanProject.Application.Commons.Interfaces;
using AppCleanProject.Domain.Entities;
using Cortex.Mediator.Commands;
using FluentValidation;
using Mapster;
using Microsoft.AspNetCore.Http;
using System.Net;
using System.Security.Claims;

namespace AppCleanProject.Application.Features.Pet.Commands
{
    public record UpdatePetCommand(long Id, string Name, string Species,
        string? Breed, DateOnly? DateOfBirth, string? Gender, string? Characteristics, string? PhotoUrl): ICommand;

    public class UpdatePetCommandValidator:AbstractValidator<UpdatePetCommand>
    {
        public UpdatePetCommandValidator()
        {
            RuleFor(x => x.Name).NotNull().NotEmpty();
            RuleFor(x => x.Species).NotNull().NotEmpty();
            RuleFor(x => x.Breed).NotEmpty();
            RuleFor(x => x.Gender).NotEmpty();
        }
    }

    public class UpdatePetCommandHandler(IRepositoryAsync<Pets> repositoryAsync, 
        IHttpContextAccessor httpContextAccessor) : ICommandHandler<UpdatePetCommand>
    {
        public async Task Handle(UpdatePetCommand command, CancellationToken cancellationToken)
        {
            string userId = httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value!;

            if (string.IsNullOrEmpty(userId))
            {
                throw new CustomException(HttpStatusCode.Unauthorized,
                    new { message = "User not Authenticated" });
            }

            var petFoundDb = await repositoryAsync.GetByIdAsync(command.Id, cancellationToken);

            if (petFoundDb == null)
            {
                throw new CustomException(HttpStatusCode.NotFound, 
                    new { message = "Not found Pet" });
            }

            Pets pet = command.Adapt(petFoundDb);

            await repositoryAsync.UpdateAsync(pet, cancellationToken);
        }
    }
}
