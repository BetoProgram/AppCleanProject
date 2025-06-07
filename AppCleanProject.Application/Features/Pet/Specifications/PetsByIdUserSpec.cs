using AppCleanProject.Domain.Entities;
using Ardalis.Specification;

namespace AppCleanProject.Application.Features.Pet.Specifications
{
    public class PetsByIdUserSpec:Specification<Pets>
    {
        public PetsByIdUserSpec(long userId)
        {
            Query.Where(x => x.OwnerId == userId);
            Query.Include(x => x.Owner);
        }
    }
}
