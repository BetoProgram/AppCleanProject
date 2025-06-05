namespace AppCleanProject.Domain.Entities;
public partial class Users
{
    public long Id { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public string? PhoneNumber { get; set; }

    public bool IsActive { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<Appointments> Appointments { get; set; } = new List<Appointments>();

    public virtual ICollection<Pets> Pets { get; set; } = new List<Pets>();

    public virtual Veterinarians? Veterinarians { get; set; }

    public virtual ICollection<Roles> Role { get; set; } = new List<Roles>();
}
