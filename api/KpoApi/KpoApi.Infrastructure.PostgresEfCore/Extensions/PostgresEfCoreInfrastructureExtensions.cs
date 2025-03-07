using KpoApi.Application.Contracts.External;
using KpoApi.Infrastructure.PostgresEfCore.Contracts.Repositories;
using KpoApi.Infrastructure.PostgresEfCore.Models;
using KpoApi.Infrastructure.PostgresEfCore.Repositories;
using KpoApi.Infrastructure.PostgresEfCore.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;

namespace KpoApi.Infrastructure.PostgresEfCore.Extensions;

public static class PostgresEfCoreInfrastructureExtensions
{
    private const string DbConnectionString = "POSTGRES_CONNECTION_STRING";

    public static void ConfigurePostgresInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        string connectionStrings = GetConnectionString();

        var dataSourceBuilder = new NpgsqlDataSourceBuilder(connectionStrings);

        var dataSource = dataSourceBuilder.Build();
        services.AddDbContext<AppDbContext>(options => options.UseNpgsql(dataSource));
        services.ConfigureRepositories();
        services.AddScoped<IPostgresEfCoreProvider, PostgresEfCoreProvider>();
    }
    
    private static IServiceCollection ConfigureRepositories(this IServiceCollection services)
    {
        
        services.AddScoped<IUsersRepository, UserRepository>();
        
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
}