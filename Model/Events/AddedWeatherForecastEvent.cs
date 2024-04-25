using EFInterceptor.Model.Abstractions;

namespace EFInterceptor.Model.Events
{
    public record AddedWeatherForecastEvent(DateOnly Date, int TemperatureC, int TemperatureF, string Summary) : IDomainEvent;
    //{
    //    public DateOnly Date { get; init; }
    //    public int TemperatureC { get; init; }
    //    public int TemperatureF { get; init; }
    //    public string Summary { get; init; }
    //}
}
