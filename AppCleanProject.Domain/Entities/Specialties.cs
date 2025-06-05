namespace AppCleanProject.Domain.Entities;

public partial class Specialties
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public virtual ICollection<Veterinarians> Veterinarians { get; set; } = new List<Veterinarians>();
}
