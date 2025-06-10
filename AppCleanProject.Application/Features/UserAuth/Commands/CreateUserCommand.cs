using System.Net;
using AppCleanProject.Application.Commons.Exceptions;
using AppCleanProject.Application.Commons.Interfaces;
using AppCleanProject.Application.Features.UserAuth.Specifications;
using AppCleanProject.Domain.Entities;
using Cortex.Mediator.Commands;
using FluentValidation;
using Mapster;

namespace AppCleanProject.Application.Features.UserAuth.Commands;

public record CreateUserCommand(string FirstName, string LastName, string Email,
    string Password, string? PhoneNumber, int TipoRol):ICommand;

public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
{
    public CreateUserCommandValidator()
    {
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
        RuleFor(x => x.Password).NotEmpty();
        RuleFor(x => x.FirstName).NotEmpty();
        RuleFor(x => x.LastName).NotEmpty();
        RuleFor(x => x.PhoneNumber).NotEmpty().MaximumLength(20);
        RuleFor(x => x.TipoRol).NotEmpty().InclusiveBetween(2,3);
    }
}

public class CreateUserCommandHandler(
    IRepositoryAsync<Users> repositoryAsync,
    IRepositoryAsync<Roles> repositoryRoleAsync,
    IAuthManage authManage)
    :ICommandHandler<CreateUserCommand>
{
    public async Task Handle(CreateUserCommand command, CancellationToken cancellationToken)
    {
        var spec = new UserByEmailSpec(command.Email);
        var userFoundDb = await repositoryAsync.FirstOrDefaultAsync(spec, cancellationToken);

        if (userFoundDb != null)
        {
            throw new CustomException(HttpStatusCode.BadRequest,
                new { message = "User is already registered" });
        }

        var role = await repositoryRoleAsync.GetByIdAsync(command.TipoRol, cancellationToken);

        var hashPassword = authManage.HashUPassword(command.Password);
        var user = command.Adapt<Users>();
        user.PasswordHash = hashPassword;
        user.IsActive = true;
        user.CreatedAt = DateTime.UtcNow;
        user.Role.Add(role!);

        await repositoryAsync.AddAsync(user, cancellationToken);
       
    }
} 