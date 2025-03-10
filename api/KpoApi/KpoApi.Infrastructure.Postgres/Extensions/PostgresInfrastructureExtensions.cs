using KpoApi.Application.Contracts.External;
using KpoApi.Infrastructure.PostgresEfCore.Contracts.Mappers;
using KpoApi.Infrastructure.PostgresEfCore.Contracts.Repositories;
using KpoApi.Infrastructure.PostgresEfCore.Mappers;
using KpoApi.Infrastructure.PostgresEfCore.Repositories;
using KpoApi.Infrastructure.PostgresEfCore.Services;
using KpoApi.Infrastructure.PostgresMigrator.Extensions;
using KpoApi.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace KpoApi.Infrastructure.PostgresEfCore.Extensions;

public static class PostgresInfrastructureExtensions
{
    private const string DbConnectionString = "POSTGRES_CONNECTION_STRING";
    
    public static void ConfigurePostgresInfrastructure(this IServiceCollection services)
    {
        string connectionStrings = GetConnectionString();
        
        services.ConfigureRepositories();
        services.ConfigureMappers();
        
        services.AddMigration(connectionStrings);
        
        services.AddScoped<IPostgresProvider, PostgresProvider>();
    }
    
    private static IServiceCollection ConfigureRepositories(this IServiceCollection services)
    {
        
        services.AddScoped<ICardiogramsRepository, CardiogramsRepository>();
        services.AddScoped<IEntireCardiogramMapper, EntireCardiogramMapper>();
        
        return services;
    }
    
    private static string GetConnectionString()
    {
        string? connectionString = Environment.GetEnvironmentVariable(DbConnectionString);

        if (string.IsNullOrWhiteSpace(connectionString))
        {
            throw new InvalidOperationException(
                $"Отсутствует переменная окружения {DbConnectionString}. Заполните ее и перезапустите приложение");
        }

        return connectionString;
    }
    
    private static IServiceCollection ConfigureMappers(this IServiceCollection services)
    {
        services.AddScoped<ICardiogramsMapper, CardiogramsMapper>();
        return services;
    }
    
}