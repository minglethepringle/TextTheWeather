using TextTheWeather.Core.Entities.OpenWeatherApi;
using TextTheWeather.Core.Entities.WeatherApi;

namespace TextTheWeather.Core.Repositories.Interfaces.Weather;

public interface IWeatherApi
{
	Task<WeatherApiResponse> GetWeather(string latitude, string longitude);
}