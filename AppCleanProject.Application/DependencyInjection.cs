

using AppCleanProject.Application.Commons.Behaviours;
using AppCleanProject.Application.Features.FServices.Commands;
using Cortex.Mediator.DependencyInjection;
using FluentValidation;
using Microsoft.Extensions.Configuration;
using System.Reflection;

namespace Microsoft.Extensions.DependencyInjection;
public static class DependencyInjection
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration, Type handlerAssemblyMarkerTypes)
    {
        //services.AddHttpContextAccessor();
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
        services.AddCortexMediator(
            configuration: configuration,
            handlerAssemblyMarkerTypes: new[] { typeof(InsertServiceHandler) },
            configure: opt =>
            {
                opt.AddOpenCommandPipelineBehavior(typeof(ValidationBehaviour<>));
            }
        );

        return services;
    }
}
