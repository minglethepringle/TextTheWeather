using System.Text.Json;
using TextTheWeather.Core.Entities.OpenWeatherApi;
using TextTheWeather.Core.Entities.WeatherApi;
using TextTheWeather.Core.Mappers;
using TextTheWeather.Core.Mappers.Interfaces;
using TextTheWeather.Core.Repositories.Interfaces.Weather;

namespace TextTheWeather.Core.Repositories.Weather;

public class OpenWeatherApi : IWeatherApi
{
	private IMapper<OpenWeatherApiResponse, WeatherApiResponse> OpenWeatherApiMapper = new OpenWeatherApiResponseMapper();
	private HttpClient HttpClient = new HttpClient();

	public async Task<WeatherApiResponse> GetWeather(string latitude, string longitude)
	{
		// Parse the response into an OpenWeatherApiResponse object
		var response = await HttpClient.GetStringAsync(
			$"https://api.openweathermap.org/data/3.0/onecall?lat={latitude}&lon={longitude}&exclude=daily,minutely&units=imperial&appid={EnvironmentVariables.OpenWeatherApiKey}");

		JsonSerializerOptions options = new JsonSerializerOptions
		{
			PropertyNameCaseInsensitive = true
		};
		OpenWeatherApiResponse apiResponse = JsonSerializer.Deserialize<OpenWeatherApiResponse>(response, options);

		return OpenWeatherApiMapper.Map(apiResponse);
	}
}