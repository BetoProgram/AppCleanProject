namespace AppCleanProject.Application.Features.Pet.Dtos
{
    public class PetsResponseDto
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
    }
}
