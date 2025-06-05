namespace AppCleanProject.Domain.Entities;

public partial class Pets
{
    public long Id { get; set; }

    public long OwnerId { get; set; }

    public string Name { get; set; } = null!;

    public string Species { get; set; } = null!;

    public string? Breed { get; set; }

    public DateOnly? DateOfBirth { get; set; }

    public string? Gender { get; set; }

    public string? Characteristics { get; set; }

    public string? PhotoUrl { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual ICollection<Appointments> Appointments { get; set; } = new List<Appointments>();

    public virtual Users Owner { get; set; } = null!;
}
