using AppCleanProject.Application.Commons.Interfaces;
using AppCleanProject.Application.Features.FSpecialties.Dtos;
using AppCleanProject.Domain.Entities;
using Cortex.Mediator.Queries;
using Mapster;

namespace AppCleanProject.Application.Features.FSpecialties.Queries;
public class GetAllSpecialtiesQuery:IQuery<List<SpecialtiesResponseDto>>
{
}

public class GetAllSpecialtiesQueryHandler(IRepositoryAsync<Specialties> repositoryAsync) 
    : IQueryHandler<GetAllSpecialtiesQuery, List<SpecialtiesResponseDto>>
{
    public async Task<List<SpecialtiesResponseDto>> Handle(GetAllSpecialtiesQuery query, CancellationToken cancellationToken)
    {
        var specials = await repositoryAsync.ListAsync(cancellationToken);
        return specials.OrderByDescending(s => s.Id).Adapt<List<SpecialtiesResponseDto>>();
    }
}