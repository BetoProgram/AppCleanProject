using AppCleanProject.Application.Commons.Exceptions;
using AppCleanProject.Application.Commons.Interfaces;
using AppCleanProject.Application.Features.Pet.Dtos;
using AppCleanProject.Application.Features.Pet.Specifications;
using AppCleanProject.Domain.Entities;
using Cortex.Mediator.Queries;
using Mapster;
using Microsoft.AspNetCore.Http;
using System.Net;
using System.Security.Claims;

namespace AppCleanProject.Application.Features.Pet.Queries
{
    public record GetPetsCustomerQuery:IQuery<List<PetsResponseDto>>;

    public class GetPetsCustomerQueryHandler(IRepositoryAsync<Pets> repositoryAsync, IHttpContextAccessor httpContextAccessor) : IQueryHandler<GetPetsCustomerQuery, List<PetsResponseDto>>
    {
        public async Task<List<PetsResponseDto>> Handle(GetPetsCustomerQuery query, CancellationToken cancellationToken)
        {
            string userId = httpContextAccessor.HttpContext?.User.FindFirst(ClaimTypes.NameIdentifier)?.Value!;

            if (string.IsNullOrEmpty(userId))
            {
                throw new CustomException(HttpStatusCode.Unauthorized,
                    new { message = "User not Authenticated" });
            }

            var petSpec = new PetsByIdUserSpec(Convert.ToInt64(userId));

            var pets = await repositoryAsync.ListAsync(petSpec, cancellationToken);

            return pets.Adapt<List<PetsResponseDto>>();
        }
    }
}
