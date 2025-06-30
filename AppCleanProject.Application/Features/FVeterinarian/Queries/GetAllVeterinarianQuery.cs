
using AppCleanProject.Application.Commons.Interfaces;
using AppCleanProject.Application.Features.FVeterinarian.Dtos;
using AppCleanProject.Domain.Entities;
using Cortex.Mediator.Queries;
using Mapster;

namespace AppCleanProject.Application.Features.FVeterinarian.Queries
{
    public record GetAllVeterinarianQuery : IQuery<List<VeterinarianResponseDto>>;

    public class GetAllVeterinarianQueryHandler(IRepositoryAsync<Veterinarians> repositoryAsync)
    : IQueryHandler<GetAllVeterinarianQuery, List<VeterinarianResponseDto>>
    {
        public async Task<List<VeterinarianResponseDto>> Handle(GetAllVeterinarianQuery query, CancellationToken cancellationToken)
        {
            var veterinarians = await repositoryAsync.ListAsync(cancellationToken);

            return veterinarians.Adapt<List<VeterinarianResponseDto>>();
        }
    }
}