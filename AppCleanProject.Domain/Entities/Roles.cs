namespace AppCleanProject.Domain.Entities;

public partial class Roles
{
    public int Id { get; set; }

    public string RoleName { get; set; } = null!;

    public string? Description { get; set; }

    public virtual ICollection<Users> User { get; set; } = new List<Users>();
    //public ICollection<UserRoles> UserRoles { get; set; }
}
