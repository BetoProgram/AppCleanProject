using AppCleanProject.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace AppCleanProject.Infraestructure.Data;

public partial class AppVetenaryContext : DbContext
{
    public AppVetenaryContext()
    {
    }

    public AppVetenaryContext(DbContextOptions<AppVetenaryContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Appointments> Appointments { get; set; }

    public virtual DbSet<Appointmentstatuses> Appointmentstatuses { get; set; }

    public virtual DbSet<Pets> Pets { get; set; }

    public virtual DbSet<Roles> Roles { get; set; }

    public virtual DbSet<Services> Services { get; set; }

    public virtual DbSet<Specialties> Specialties { get; set; }

    public virtual DbSet<Users> Users { get; set; }

    public virtual DbSet<Veterinarians> Veterinarians { get; set; }

    public virtual DbSet<Veterinaryschedules> Veterinaryschedules { get; set; }

//    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
//        => optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=storagepost;Username=admin;Password=admin123");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Appointments>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("appointments_pkey");

            entity.ToTable("appointments");

            entity.HasIndex(e => new { e.VeterinarianId, e.AppointmentDatetime }, "appointments_veterinarian_id_appointment_datetime_key").IsUnique();

            entity.HasIndex(e => e.ClientId, "idx_appointments_client_id");

            entity.HasIndex(e => e.AppointmentDatetime, "idx_appointments_datetime");

            entity.HasIndex(e => e.VeterinarianId, "idx_appointments_veterinarian_id");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AppointmentDatetime).HasColumnName("appointment_datetime");
            entity.Property(e => e.ClientId).HasColumnName("client_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("created_at");
            entity.Property(e => e.PetId).HasColumnName("pet_id");
            entity.Property(e => e.ReasonForAppointment).HasColumnName("reason_for_appointment");
            entity.Property(e => e.ServiceId).HasColumnName("service_id");
            entity.Property(e => e.StatusId).HasColumnName("status_id");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("updated_at");
            entity.Property(e => e.VeterinarianId).HasColumnName("veterinarian_id");

            entity.HasOne(d => d.Client).WithMany(p => p.Appointments)
                .HasForeignKey(d => d.ClientId)
                .HasConstraintName("appointments_client_id_fkey");

            entity.HasOne(d => d.Pet).WithMany(p => p.Appointments)
                .HasForeignKey(d => d.PetId)
                .HasConstraintName("appointments_pet_id_fkey");

            entity.HasOne(d => d.Service).WithMany(p => p.Appointments)
                .HasForeignKey(d => d.ServiceId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("appointments_service_id_fkey");

            entity.HasOne(d => d.Status).WithMany(p => p.Appointments)
                .HasForeignKey(d => d.StatusId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("appointments_status_id_fkey");

            entity.HasOne(d => d.Veterinarian).WithMany(p => p.Appointments)
                .HasForeignKey(d => d.VeterinarianId)
                .OnDelete(DeleteBehavior.Restrict)
                .HasConstraintName("appointments_veterinarian_id_fkey");
        });

        modelBuilder.Entity<Appointmentstatuses>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("appointmentstatuses_pkey");

            entity.ToTable("appointmentstatuses");

            entity.HasIndex(e => e.StatusName, "appointmentstatuses_status_name_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.StatusName)
                .HasMaxLength(50)
                .HasColumnName("status_name");
        });

        modelBuilder.Entity<Pets>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("pets_pkey");

            entity.ToTable("pets");

            entity.HasIndex(e => e.OwnerId, "idx_pets_owner_id");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Breed)
                .HasMaxLength(100)
                .HasColumnName("breed");
            entity.Property(e => e.Characteristics).HasColumnName("characteristics");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("created_at");
            entity.Property(e => e.DateOfBirth).HasColumnName("date_of_birth");
            entity.Property(e => e.Gender)
                .HasMaxLength(20)
                .HasColumnName("gender");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
            entity.Property(e => e.OwnerId).HasColumnName("owner_id");
            entity.Property(e => e.PhotoUrl)
                .HasMaxLength(255)
                .HasColumnName("photo_url");
            entity.Property(e => e.Species)
                .HasMaxLength(100)
                .HasColumnName("species");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("updated_at");

            entity.HasOne(d => d.Owner).WithMany(p => p.Pets)
                .HasForeignKey(d => d.OwnerId)
                .HasConstraintName("pets_owner_id_fkey");
        });

        modelBuilder.Entity<Roles>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("roles_pkey");

            entity.ToTable("roles");

            entity.HasIndex(e => e.RoleName, "roles_role_name_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.RoleName)
                .HasMaxLength(50)
                .HasColumnName("role_name");
        });

        modelBuilder.Entity<Services>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("services_pkey");

            entity.ToTable("services");

            entity.HasIndex(e => e.Name, "services_name_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.DurationMinutes).HasColumnName("duration_minutes");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("is_active");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .HasColumnName("name");
            entity.Property(e => e.Price)
                .HasPrecision(10, 2)
                .HasColumnName("price");
        });

        modelBuilder.Entity<Specialties>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("specialties_pkey");

            entity.ToTable("specialties");

            entity.HasIndex(e => e.Name, "specialties_name_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Description).HasColumnName("description");
            entity.Property(e => e.Name)
                .HasMaxLength(100)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Users>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("users_pkey");

            entity.ToTable("users");

            entity.HasIndex(e => e.Email, "idx_users_email");

            entity.HasIndex(e => e.Email, "users_email_key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("created_at");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.FirstName)
                .HasMaxLength(100)
                .HasColumnName("first_name");
            entity.Property(e => e.IsActive)
                .HasDefaultValue(true)
                .HasColumnName("is_active");
            entity.Property(e => e.LastName)
                .HasMaxLength(100)
                .HasColumnName("last_name");
            entity.Property(e => e.PasswordHash)
                .HasMaxLength(255)
                .HasColumnName("password_hash");
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(20)
                .HasColumnName("phone_number");
            entity.Property(e => e.UpdatedAt)
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .HasColumnName("updated_at");

            entity.HasMany(d => d.Role).WithMany(p => p.User)
                .UsingEntity<Dictionary<string, object>>(
                    "Userroles",
                    r => r.HasOne<Roles>().WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .HasConstraintName("userroles_role_id_fkey"),
                    l => l.HasOne<Users>().WithMany()
                        .HasForeignKey("UserId")
                        .HasConstraintName("userroles_user_id_fkey"),
                    j =>
                    {
                        j.HasKey("UserId", "RoleId").HasName("userroles_pkey");
                        j.ToTable("userroles");
                        j.IndexerProperty<long>("UserId").HasColumnName("user_id");
                        j.IndexerProperty<int>("RoleId").HasColumnName("role_id");
                    });
        });

        modelBuilder.Entity<Veterinarians>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("veterinarians_pkey");

            entity.ToTable("veterinarians");

            entity.HasIndex(e => e.LicenseNumber, "idx_veterinarians_license");

            entity.HasIndex(e => e.LicenseNumber, "veterinarians_license_number_key").IsUnique();

            entity.Property(e => e.Id)
                .ValueGeneratedNever()
                .HasColumnName("id");
            entity.Property(e => e.Bio).HasColumnName("bio");
            entity.Property(e => e.Activate).HasColumnName("activate");
            entity.Property(e => e.LicenseNumber)
                .HasMaxLength(50)
                .HasColumnName("license_number");
            entity.Property(e => e.PhotoUrl)
                .HasMaxLength(255)
                .HasColumnName("photo_url");
            entity.Property(e => e.SpecialtyId).HasColumnName("specialty_id");

            entity.HasOne(d => d.IdNavigation).WithOne(p => p.Veterinarians)
                .HasForeignKey<Veterinarians>(d => d.Id)
                .HasConstraintName("veterinarians_id_fkey");

            entity.HasOne(d => d.Specialty).WithMany(p => p.Veterinarians)
                .HasForeignKey(d => d.SpecialtyId)
                .OnDelete(DeleteBehavior.SetNull)
                .HasConstraintName("veterinarians_specialty_id_fkey");
        });

        modelBuilder.Entity<Veterinaryschedules>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("veterinaryschedules_pkey");

            entity.ToTable("veterinaryschedules");

            entity.HasIndex(e => e.VeterinarianId, "idx_veterinaryschedules_veterinarian_id");

            entity.HasIndex(e => new { e.VeterinarianId, e.DayOfWeek, e.StartTime, e.EndTime }, "veterinaryschedules_veterinarian_id_day_of_week_start_time__key").IsUnique();

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.DayOfWeek)
                .HasMaxLength(15)
                .HasColumnName("day_of_week");
            entity.Property(e => e.EndTime).HasColumnName("end_time");
            entity.Property(e => e.IsAvailable)
                .HasDefaultValue(true)
                .HasColumnName("is_available");
            entity.Property(e => e.Notes).HasColumnName("notes");
            entity.Property(e => e.StartTime).HasColumnName("start_time");
            entity.Property(e => e.VeterinarianId).HasColumnName("veterinarian_id");

            entity.HasOne(d => d.Veterinarian).WithMany(p => p.Veterinaryschedules)
                .HasForeignKey(d => d.VeterinarianId)
                .HasConstraintName("veterinaryschedules_veterinarian_id_fkey");
        });

       /*  modelBuilder.Entity<UserRoles>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.RoleId });
            entity.HasOne(ur => ur.User)
              .WithMany(u => u.UserRoles)
              .HasForeignKey(ur => ur.UserId);

        // Define la relación de muchos a uno con Role
        entity.HasOne(ur => ur.Role)
              .WithMany(r => r.UserRoles)
              .HasForeignKey(ur => ur.RoleId);
        }); */

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
