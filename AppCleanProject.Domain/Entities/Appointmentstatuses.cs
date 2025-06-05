
namespace AppCleanProject.Domain.Entities;

public partial class Appointmentstatuses
{
    public int Id { get; set; }

    public string StatusName { get; set; } = null!;

    public string? Description { get; set; }

    public virtual ICollection<Appointments> Appointments { get; set; } = new List<Appointments>();
}
