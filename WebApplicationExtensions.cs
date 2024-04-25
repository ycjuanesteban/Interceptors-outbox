using EFInterceptor.Infrastructure.Persistence;
using EFInterceptor.Infrastructure.Persistence.Interceptors;
using EFInterceptor.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

namespace EFInterceptor;

public static class WebApplicationExtensions
{
    public static IServiceCollection AddEFInterceptorServices(
        this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddScoped<IWeatherForcastsRepository, WeatherForcastsRepository>();
        services.AddSingleton<InsertOutboxInterceptor>();


        var connectionString = configuration.GetConnectionString("WeatherForecastDbContext");

        services.AddDbContext<WeatherForecastDbContext>((sp, options) =>
        {
            options
                .UseNpgsql(connectionString)
                .AddInterceptors(sp.GetService<InsertOutboxInterceptor>()!);
        });

        return services;
    }
}