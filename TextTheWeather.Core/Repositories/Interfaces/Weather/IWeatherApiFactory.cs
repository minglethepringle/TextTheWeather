namespace TextTheWeather.Core.Repositories.Interfaces.Weather;

public interface IWeatherApiFactory
{
    IWeatherApi Create();
}