using System.Net;
using AppCleanProject.Application.Commons.Exceptions;
using AppCleanProject.Application.Commons.Interfaces;
using AppCleanProject.Domain.Entities;
using Cortex.Mediator.Commands;
using FluentValidation;
using Mapster;

namespace AppCleanProject.Application.Features.FVeterinarian.Commands
{
    public record UpdateVeterinarianCommand(int Id, int? SpecialtyId, string LicenseNumber,
    string? Bio, string? PhotoUrl) : ICommand;

    public class UpdateVeterinarianCommandValidator : AbstractValidator<UpdateVeterinarianCommand>
    {
        public UpdateVeterinarianCommandValidator()
        {
            RuleFor(x => x.Id).NotNull();
            RuleFor(x => x.SpecialtyId).NotEmpty();
            RuleFor(x => x.LicenseNumber).NotEmpty();
            RuleFor(x => x.Bio).NotEmpty();
        }
    }

    public class UpdateVeterinarianCommandHanlder : ICommandHandler<UpdateVeterinarianCommand>
    {
        private readonly IRepositoryAsync<Veterinarians> _repositoryAsync;

        public UpdateVeterinarianCommandHanlder(IRepositoryAsync<Veterinarians> repositoryAsync)
        {
            _repositoryAsync = repositoryAsync;
        }

        public async Task Handle(UpdateVeterinarianCommand command, CancellationToken cancellationToken)
        {
            var veterinarianDb = await _repositoryAsync.GetByIdAsync(command.Id, cancellationToken);

            if (veterinarianDb is null)
            {
                throw new CustomException(HttpStatusCode.NotFound, new { message = "Not found item" });
            }

            var Veterinarian = command.Adapt(veterinarianDb);

            await _repositoryAsync.UpdateAsync(Veterinarian, cancellationToken);
        }
    }
}