namespace AppCleanProject.Domain.Entities;
public partial class Veterinaryschedules
{
    public long Id { get; set; }

    public long VeterinarianId { get; set; }

    public string DayOfWeek { get; set; } = null!;

    public TimeOnly StartTime { get; set; }

    public TimeOnly EndTime { get; set; }

    public bool IsAvailable { get; set; }

    public string? Notes { get; set; }

    public virtual Veterinarians Veterinarian { get; set; } = null!;
}
