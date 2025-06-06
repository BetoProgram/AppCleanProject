using AppCleanProject.Domain.Entities;

namespace AppCleanProject.Application.Commons.Interfaces;

public interface IAuthManage
{
    bool VerifyPassword(string password, string passwordDb);
    string HashUPassword(string password);
    string CreateToken(Users user, List<string>? roles = null);
}