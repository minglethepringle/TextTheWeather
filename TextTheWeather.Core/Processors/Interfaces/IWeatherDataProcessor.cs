using TextTheWeather.Core.Entities.WeatherApi;

namespace TextTheWeather.Core.Processors.Interfaces;

public interface IWeatherDataProcessor
{
	string GetWeatherDescription();
}