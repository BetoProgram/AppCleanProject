namespace AppCleanProject.Domain.Entities;

public partial class Services
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public int DurationMinutes { get; set; }

    public decimal Price { get; set; }

    public bool IsActive { get; set; }

    public virtual ICollection<Appointments> Appointments { get; set; } = new List<Appointments>();
}
