using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppCleanProject.Application.Features.FServices.Dtos
{
    public class ServiceResponseDto
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        public int DurationMinutes { get; set; }

        public decimal Price { get; set; }

        public bool IsActive { get; set; }
    }
}
