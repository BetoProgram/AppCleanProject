using AppCleanProject.Application.Commons.Interfaces;
using AppCleanProject.Domain.Entities;
using Cortex.Mediator.Commands;
using FluentValidation;
using Mapster;

namespace AppCleanProject.Application.Features.FVeterinarian.Commands
{
    public record RegisterVeterinarianCommand(int? SpecialtyId, string LicenseNumber,
    string? Bio, string? PhotoUrl):ICommand;

    public class RegisterVeterinarianCommandValidator : AbstractValidator<RegisterVeterinarianCommand>
    {
        public RegisterVeterinarianCommandValidator()
        {
            RuleFor(x => x.SpecialtyId).NotEmpty();
            RuleFor(x => x.LicenseNumber).NotEmpty();
            RuleFor(x => x.Bio).NotEmpty();
        }
    }

    public class RegisterVeterinarianCommandHandler(IRepositoryAsync<Veterinarians> repositoryAsync)
    : ICommandHandler<RegisterVeterinarianCommand>
    {
        public async Task Handle(RegisterVeterinarianCommand command, CancellationToken cancellationToken)
        {
            var vtn = command.Adapt<Veterinarians>();

            await repositoryAsync.AddAsync(vtn);
        }
    }
}