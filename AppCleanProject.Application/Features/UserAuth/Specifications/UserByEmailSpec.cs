using AppCleanProject.Domain.Entities;
using Ardalis.Specification;

namespace AppCleanProject.Application.Features.UserAuth.Specifications;

public class UserByEmailSpec:Specification<Users>
{
    public UserByEmailSpec(string email)
    {
        if (!string.IsNullOrEmpty(email))
        {
            Query.Where(x => x.Email == email);
            Query.Include(x => x.Role);
        }
    }
}