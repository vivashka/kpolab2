using KpoApi.Application.Contracts;
using KpoApi.Application.Services;
using Microsoft.Extensions.DependencyInjection;

namespace KpoApi.Application.Extensions;

public static class ApplicationExtensions
{
    public static IServiceCollection ConfigureApplicationLayer(this IServiceCollection services)
    {
        services.AddScoped<ICardiogramService, CardiogramService>();
        return services;
    }
}