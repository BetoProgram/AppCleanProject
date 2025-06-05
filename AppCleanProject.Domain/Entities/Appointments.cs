namespace AppCleanProject.Domain.Entities;

public partial class Appointments
{
    public long Id { get; set; }

    public long ClientId { get; set; }

    public long PetId { get; set; }

    public long VeterinarianId { get; set; }

    public int ServiceId { get; set; }

    public int StatusId { get; set; }

    public DateTime AppointmentDatetime { get; set; }

    public string? ReasonForAppointment { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual Users Client { get; set; } = null!;

    public virtual Pets Pet { get; set; } = null!;

    public virtual Services Service { get; set; } = null!;

    public virtual Appointmentstatuses Status { get; set; } = null!;

    public virtual Veterinarians Veterinarian { get; set; } = null!;
}
