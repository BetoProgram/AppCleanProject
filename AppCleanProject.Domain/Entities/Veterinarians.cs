namespace AppCleanProject.Domain.Entities;

public partial class Veterinarians
{
    public long Id { get; set; }

    public int? SpecialtyId { get; set; }

    public string? LicenseNumber { get; set; }

    public string? Bio { get; set; }

    public string? PhotoUrl { get; set; }
    public bool Activate { get; set; }

    public virtual ICollection<Appointments> Appointments { get; set; } = new List<Appointments>();

    public virtual Users IdNavigation { get; set; } = null!;

    public virtual Specialties? Specialty { get; set; }

    public virtual ICollection<Veterinaryschedules> Veterinaryschedules { get; set; } = new List<Veterinaryschedules>();
}
