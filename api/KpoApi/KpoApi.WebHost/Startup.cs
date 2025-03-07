using Microsoft.AspNetCore.Builder;
using Serilog;

namespace KpoApi.Presentation;

public class Startup 
{
    private readonly IConfiguration _configuration;

    public Startup(IConfiguration configuration)
    {
        _configuration = configuration;
    }
    
    public void ConfigureServices(IServiceCollection service)
    {
        
        service.AddSerilog();
        service.AddRouting();
        // var assembly = Assembly.Load("CardioView.Presentation"); // TODO исправлено?
        //
        // service.AddControllers()
        //     .AddApplicationPart(assembly);
        
        
    }
    
    public void Configure(IApplicationBuilder applicationBuilder)
    {
        applicationBuilder.UseRouting();
        
        applicationBuilder.UseSwagger();
        applicationBuilder.UseSwaggerUI();
        
        applicationBuilder.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}