using EFInterceptor.Infrastructure.Repositories;
using EFInterceptor.Model;
using Microsoft.AspNetCore.Mvc;

namespace EFInterceptor.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries =
        [
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        ];

        private readonly IWeatherForcastsRepository _weatherForcastsRepository;

        public WeatherForecastController(IWeatherForcastsRepository repository)
        {
            _weatherForcastsRepository = repository;
        }

        [HttpPost(Name = "GetWeatherForecast")]
        public async Task Post(CancellationToken cancellationToken = default)
        {
            var forecast = Enumerable.Range(1, 5)
                .Select(index =>
                    WeatherForecast.Create(Random.Shared.Next(-20, 55),
                    Summaries[Random.Shared.Next(Summaries.Length)]))
            .ToArray();

            foreach (var item in forecast)
            {
                await _weatherForcastsRepository.SaveAsync(item, cancellationToken);
            }
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public async Task<IEnumerable<WeatherForecast>> Get(CancellationToken cancellationToken = default)
        {
            return await _weatherForcastsRepository.GetAllAsync(cancellationToken);
        }
    }
}
