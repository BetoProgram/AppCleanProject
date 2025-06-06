namespace AppCleanProject.Infraestructure.Models;

public class JwtSettings
{
    public string? Key { get; set; }
    public string? Issuer { get; set; }
    public string? Audience { get; set; }
    public double DurationInMinutes { get; set; }
    public TimeSpan ExpireTime { get; set; }
}