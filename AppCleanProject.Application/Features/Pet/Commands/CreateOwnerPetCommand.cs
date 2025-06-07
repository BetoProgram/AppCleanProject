using AppCleanProject.Application.Commons.Exceptions;
using AppCleanProject.Application.Commons.Interfaces;
using AppCleanProject.Domain.Entities;
using Cortex.Mediator.Commands;
using FluentValidation;
using Mapster;
using Microsoft.AspNetCore.Http;
using System.Net;

namespace AppCleanProject.Application.Features.Pet.Commands
{
    public record CreateOwnerPetCommand(string Name, string Species, 
        string? Breed, DateOnly? DateOfBirth, string? Gender, string? Characteristics, string? PhotoUrl) :ICommand;

    public class CreateOwnerPetCommandValidator:AbstractValidator<CreateOwnerPetCommand>
    {
        public CreateOwnerPetCommandValidator()
        {
            RuleFor(x => x.Name).NotNull().NotEmpty();
            RuleFor(x => x.Species).NotNull().NotEmpty();
            RuleFor(x => x.Breed).NotEmpty();
            RuleFor(x => x.Gender).NotEmpty();
        }
    }

    public class CreateOwnerPetCommandHandler(IRepositoryAsync<Pets> repositoryAsync, 
        IHttpContextAccessor httpContextAccessor) : ICommandHandler<CreateOwnerPetCommand>
    {
        public async Task Handle(CreateOwnerPetCommand command, CancellationToken cancellationToken)
        {
            string userId = httpContextAccessor.HttpContext?.User.FindFirst("nameid")?.Value!;

            if (string.IsNullOrEmpty(userId))
            {
                throw new CustomException(HttpStatusCode.Unauthorized, 
                    new { message = "User not Authenticated" });
            }

            var pet = command.Adapt<Pets>();
            pet.OwnerId = Convert.ToInt64(userId);

            await repositoryAsync.AddAsync(pet);
        }
    }
}
