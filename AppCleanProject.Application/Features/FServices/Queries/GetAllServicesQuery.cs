using AppCleanProject.Application.Commons.Interfaces;
using AppCleanProject.Application.Features.FServices.Dtos;
using AppCleanProject.Domain.Entities;
using Cortex.Mediator.Queries;
using Mapster;

namespace AppCleanProject.Application.Features.FServices.Queries
{
    public class GetAllServicesQuery:IQuery<List<ServiceResponseDto>>
    {
    }

    public class GetAllServicesQueryHandler(IRepositoryAsync<Services> _readRepositoryAsync) : IQueryHandler<GetAllServicesQuery, List<ServiceResponseDto>>
    {
        public async Task<List<ServiceResponseDto>> Handle(GetAllServicesQuery query, CancellationToken cancellationToken)
        {
            var services =  await _readRepositoryAsync.ListAsync(cancellationToken);

            return services.Adapt<List<ServiceResponseDto>>();
        }
    }
}
