using KpoApi.Application.Extensions;
using KpoApi.Presentation.Extensions;
using KpoApi.Infrastructure.PostgresEfCore.Extensions;
using Microsoft.AspNetCore.Builder;
using Serilog;

namespace KpoApi.WebHost;

public class Startup 
{
    private readonly IConfiguration _configuration;

    public Startup(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    public void ConfigureServices(IServiceCollection service)
    {
        service.AddCors();
        service.AddSerilog();
        service.AddRouting();
 
        service.ConfigurePostgresInfrastructure();
        service.ConfigurePostgresInfrastructure(_configuration);
        service.ConfigureApplicationLayer();
        service.ConfigurePresentationLayer();
        
    }
    
    public void Configure(IApplicationBuilder applicationBuilder)
    {
        applicationBuilder.UseRouting();
        
        applicationBuilder.UseSwagger();
        applicationBuilder.UseSwaggerUI();
        applicationBuilder.UseCors(builder => builder.AllowAnyOrigin().AllowAnyHeader());
        applicationBuilder.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });

        
        applicationBuilder.UseAuthorization();
    }
}