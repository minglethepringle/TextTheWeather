using TextTheWeather.Core.Entities;
using TextTheWeather.Core.Entities.OpenWeatherApi;

namespace TextTheWeather.Core.Repositories.Interfaces.WeatherRepository;

public interface IWeatherApi
{
    Task<OpenWeatherApiResponse> GetWeather(string latitude, string longitude);
}