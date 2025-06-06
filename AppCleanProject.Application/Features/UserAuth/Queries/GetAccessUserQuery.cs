using System.Net;
using AppCleanProject.Application.Commons.Exceptions;
using AppCleanProject.Application.Commons.Interfaces;
using AppCleanProject.Application.Features.UserAuth.Dtos;
using AppCleanProject.Application.Features.UserAuth.Specifications;
using AppCleanProject.Domain.Entities;
using Cortex.Mediator.Queries;
using FluentValidation;

namespace AppCleanProject.Application.Features.UserAuth.Queries;

public class GetAccessUserQuery:IQuery<UserAuthResponseDto>
{
    public string Email { get; set; } = null!;
    public string Password { get; set; } = null!;
}

public class GetAccessUserQueryValidator:AbstractValidator<GetAccessUserQuery>
{
    public GetAccessUserQueryValidator()
    {
        RuleFor(x => x.Email).NotEmpty().EmailAddress();
        RuleFor(x => x.Password).NotEmpty();
    }
}

public class GetAccessUserQueryHandler(
    IRepositoryAsync<Users> repositoryAsync,
    IAuthManage authManage) 
    : IQueryHandler<GetAccessUserQuery, UserAuthResponseDto>
{
    public async Task<UserAuthResponseDto> Handle(GetAccessUserQuery query, CancellationToken cancellationToken)
    {
        var spec = new UserByEmailSpec(query.Email);
        var userFoundDb = await repositoryAsync.FirstOrDefaultAsync(spec, cancellationToken);

        if (userFoundDb == null)
        {
            throw new CustomException(HttpStatusCode.NotFound, 
                new { message = "User not found" });
        }

        var verifyPassword = authManage.VerifyPassword(query.Password, userFoundDb.PasswordHash);

        if (!verifyPassword)
        {
            throw new CustomException(HttpStatusCode.Unauthorized,
                new { message = "Invalid password" });
        }
        List<string> roles = [];
        roles.AddRange(userFoundDb.Role.Select(x => x.RoleName));

        return new UserAuthResponseDto
        {
            Email = userFoundDb.Email,
            Token = authManage.CreateToken(userFoundDb, roles)
        };
    }
}