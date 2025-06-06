using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AppCleanProject.Application.Commons.Interfaces;
using AppCleanProject.Domain.Entities;
using AppCleanProject.Infraestructure.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace AppCleanProject.Infraestructure.UServices;

public class AuthManage:IAuthManage
{
    private JwtSettings JwtSettings { get; set; }
    public AuthManage(IOptions<JwtSettings> jwtSettings)
    {
        this.JwtSettings = jwtSettings.Value;
    }
    
    public bool VerifyPassword(string password, string passwordDb)
    {
        return BCrypt.Net.BCrypt.Verify(password, passwordDb);
    }

    public string HashUPassword(string password)
    {
        var hashPassword = BCrypt.Net.BCrypt.HashPassword(password, workFactor:10);
        return hashPassword;
    }

    public string CreateToken(Users user, List<string>? roles = null)
    {
        var claims = new List<Claim>()
        {
            new Claim(JwtRegisteredClaimNames.NameId,user.Email!),
            new Claim("email", user.Email!),
        };
        if (roles is not null)
        {
            foreach (var rol in roles!)
            {
                var claim = new Claim(ClaimTypes.Role, rol);
                claims.Add(claim);
            }
        }


        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtSettings.Key!));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
        var tokenDescription = new SecurityTokenDescriptor()
        {
            Subject = new ClaimsIdentity(claims),
            Expires = DateTime.UtcNow.Add(JwtSettings.ExpireTime),
            SigningCredentials = credentials
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescription);
        return tokenHandler.WriteToken(token);
    }
}