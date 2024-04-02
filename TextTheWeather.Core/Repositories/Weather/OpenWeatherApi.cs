using System.Text.Json;
using TextTheWeather.Core.Entities.OpenWeatherApi;
using TextTheWeather.Core.Repositories.Interfaces.WeatherRepository;

namespace TextTheWeather.Core.Repositories.Weather;

public class OpenWeatherApi : IWeatherApi
{
    private static readonly HttpClient HttpClient = new HttpClient();
    
    public async Task<OpenWeatherApiResponse> GetWeather(string latitude, string longitude)
    {
        // Parse the response into an OpenWeatherApiResponse object
        string response = await HttpClient.GetStringAsync(
            $"https://api.openweathermap.org/data/3.0/onecall?lat={latitude}&lon={longitude}&exclude=daily,minutely&units=imperial&appid={EnvironmentVariables.OpenWeatherApiKey}");
        
        JsonSerializerOptions options = new()
        {
            PropertyNameCaseInsensitive = true
        };
        OpenWeatherApiResponse? apiResponse = JsonSerializer.Deserialize<OpenWeatherApiResponse>(response, options);
        if (apiResponse == null)
            throw new Exception("Failed to parse OpenWeatherApiResponse");
        
        return apiResponse;
    }
}