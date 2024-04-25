using EFInterceptor.Model.Events;

namespace EFInterceptor.Model
{
    public class WeatherForecast : AggregateRoot
    {
        public DateOnly Date { get; init; }
        public int TemperatureC { get; init; }
        public int TemperatureF { get; init; }
        public string Summary { get; init; }

        private WeatherForecast(int temperatureC, string summary)
        {
            Date = DateOnly.FromDateTime(DateTime.Now);
            TemperatureC = temperatureC;
            TemperatureF = 32 + (int)(TemperatureC / 0.5556);
            Summary = summary;
        }

        public static WeatherForecast Create(int temperatureC, string summary)
        {
            var weatherForecast = new WeatherForecast(temperatureC, summary);
            var @event = new AddedWeatherForecastEvent(weatherForecast.Date, weatherForecast.TemperatureC, weatherForecast.TemperatureF, weatherForecast.Summary);
            //{
            //    Date = weatherForecast.Date,
            //    TemperatureC = weatherForecast.TemperatureC,
            //    TemperatureF = weatherForecast.TemperatureF,
            //    Summary = weatherForecast.Summary
            //};
            weatherForecast.AddEvent(@event);

            return weatherForecast;
        }
    }
}
