using AppCleanProject.Application.Commons.Interfaces;
using AppCleanProject.Infraestructure.Data;
using Ardalis.Specification.EntityFrameworkCore;

namespace AppCleanProject.Infraestructure.Persistence
{
    public class RepositoryImplAsync<T>:RepositoryBase<T>, IRepositoryAsync<T> where T : class
    {
        private readonly AppVetenaryContext _context;

        public RepositoryImplAsync(AppVetenaryContext context):base(context)
        {
            _context = context;
        }
    }
}
