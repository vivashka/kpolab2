using KpoApi.Presentation.Contracts;
using KpoApi.Presentation.Controllers;
using KpoApi.Presentation.Mappers;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace KpoApi.Presentation.Extensions;

public static class PresentationLayerExtensions
{
    public static IServiceCollection ConfigurePresentationLayer(this IServiceCollection services)
    {
        
        services.AddControllers()
            .AddApplicationPart(typeof(CardiogramController).Assembly);
        
        services.AddControllers()
            .AddApplicationPart(typeof(UserController).Assembly);
        
        services.AddControllers()
            .AddApplicationPart(typeof(SeparateDataController).Assembly);
        
        services.AddEndpointsApiExplorer();

        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "API CardioViewer.Backend", Version = "v1" });
        });
        
        services.ConfigureMappers();
        
        return services;
    }

    private static IServiceCollection ConfigureMappers(this IServiceCollection services)
    {
        services.AddScoped<ICardiogramMapper, CardiogramsMapper>();
        return services;
    }
}