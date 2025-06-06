using AppCleanProject.Application.Features.UserAuth.Commands;
using AppCleanProject.Application.Features.UserAuth.Dtos;
using AppCleanProject.Application.Features.UserAuth.Queries;
using Microsoft.AspNetCore.Mvc;

namespace AppCleanProject.WebApi.Controllers;

public class AuthController:ApiBaseController
{
    [HttpPost("[action]")]
    public async Task<ActionResult> Register([FromBody] CreateUserCommand request)
    {
        await Mediator.SendAsync(request);
        return Ok(new { message = "Register success!!!" });
    }
    
    [HttpPost("[action]")]
    public async Task<ActionResult> Login([FromBody] GetAccessUserQuery request)
    {
        var user = await Mediator
            .SendAsync<GetAccessUserQuery, UserAuthResponseDto>(new GetAccessUserQuery
            {
                Email = request.Email, 
                Password = request.Password
            });
        return Ok(user);
    }
}