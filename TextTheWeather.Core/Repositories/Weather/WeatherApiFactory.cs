using TextTheWeather.Core.Repositories.Interfaces.Weather;

namespace TextTheWeather.Core.Repositories.Weather;

public class WeatherApiFactory : IWeatherApiFactory
{
    public IWeatherApi Create()
    {
        return new OpenWeatherApi();
    }
}