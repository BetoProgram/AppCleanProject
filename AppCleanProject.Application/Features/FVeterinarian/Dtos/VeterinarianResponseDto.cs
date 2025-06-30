using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AppCleanProject.Application.Features.FVeterinarian.Dtos
{
    public class VeterinarianResponseDto
    {
        public long Id { get; set; }

        public int? SpecialtyId { get; set; }

        public string? LicenseNumber { get; set; }

        public string? Bio { get; set; }

        public string? PhotoUrl { get; set; }
    }
}