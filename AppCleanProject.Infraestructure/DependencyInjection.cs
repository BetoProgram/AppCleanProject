using AppCleanProject.Application.Commons.Interfaces;
using AppCleanProject.Infraestructure.Data;
using AppCleanProject.Infraestructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using WatchDog;

namespace Microsoft.Extensions.DependencyInjection;
public static class DependencyInjection
{
    public static IServiceCollection AddInfraestructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        string connectionString = configuration.GetConnectionString("DefaultConnection")!;
        services.AddDbContext<AppVetenaryContext>(c => 
        c.UseNpgsql(connectionString));

        #region WatchDog Logs
        services.AddWatchDogServices(opt =>
        {
            opt.IsAutoClear = false;
            opt.SetExternalDbConnString = connectionString;
            opt.DbDriverOption = WatchDog.src.Enums.WatchDogDbDriverEnum.PostgreSql;
        });
        #endregion

        #region Repositories
        services.AddScoped(typeof(IRepositoryAsync<>), typeof(RepositoryImplAsync<>));
        #endregion

        return services;
    }
}

