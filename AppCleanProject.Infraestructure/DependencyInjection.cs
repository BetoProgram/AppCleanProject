using System.Text;
using AppCleanProject.Application.Commons.Interfaces;
using AppCleanProject.Infraestructure.Data;
using AppCleanProject.Infraestructure.Models;
using AppCleanProject.Infraestructure.Persistence;
using AppCleanProject.Infraestructure.UServices;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using WatchDog;

namespace Microsoft.Extensions.DependencyInjection;

public static class DependencyInjection
{
    public static IServiceCollection AddInfraestructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        string connectionString = configuration.GetConnectionString("DefaultConnection")!;
        services.AddDbContext<AppVetenaryContext>(c =>
        c.UseNpgsql(connectionString));

        services.AddHttpContextAccessor();

        #region CORS
        services.AddCors(opt =>
        {
            opt.AddPolicy("corspolicy", builder =>
                builder.WithOrigins("*")
                .AllowAnyMethod()
                .AllowAnyOrigin()
                .AllowAnyHeader());
        });
        #endregion

        #region WatchDog Logs
        services.AddWatchDogServices(opt =>
        {
            opt.IsAutoClear = false;
            opt.SetExternalDbConnString = connectionString;
            opt.DbDriverOption = WatchDog.src.Enums.WatchDogDbDriverEnum.PostgreSql;
        });
        #endregion

        #region Authentication
        services.Configure<JwtSettings>(configuration.GetSection("JwtSettings"));
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JwtSettings:Key"]!));

        services.AddAuthentication(opt =>
            {
                opt.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    IssuerSigningKey = key,
                    ClockSkew = TimeSpan.Zero
                };
            });
        #endregion

        #region Injections
        services.AddScoped(typeof(IRepositoryAsync<>), typeof(RepositoryImplAsync<>));
        services.AddTransient(typeof(IAuthManage), typeof(AuthManage));
        #endregion

        return services;
    }
}

