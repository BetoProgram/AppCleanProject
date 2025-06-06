namespace AppCleanProject.Application.Features.UserAuth.Dtos;

public class UserAuthResponseDto
{
    public string Email { get; set; } = null!;
    public string Token { get; set; } = null!;
}