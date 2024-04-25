using EFInterceptor.Infrastructure.Persistence;
using EFInterceptor.Model;
using Microsoft.EntityFrameworkCore;

namespace EFInterceptor.Infrastructure.Repositories;

public interface IWeatherForcastsRepository
{
    Task SaveAsync(WeatherForecast entity, CancellationToken ct);

    Task<IList<WeatherForecast>> GetAllAsync(CancellationToken ct);
}

public class WeatherForcastsRepository : IWeatherForcastsRepository
{
    private readonly DbContext _dbContext;

    public WeatherForcastsRepository(WeatherForecastDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IList<WeatherForecast>> GetAllAsync(CancellationToken ct)
    {
        return await _dbContext.Set<WeatherForecast>().ToListAsync(ct);
    }

    public async Task SaveAsync(WeatherForecast entity, CancellationToken ct)
    {
        await _dbContext.Set<WeatherForecast>().AddAsync(entity, ct);
        await _dbContext.SaveChangesAsync(ct);
    }
}