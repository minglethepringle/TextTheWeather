using System.Text.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using TextTheWeather.Core.Entities.OpenWeatherApi;
using TextTheWeather.Core.Entities.Sun;
using TextTheWeather.Core.Entities.WeatherApi;
using TextTheWeather.Core.Entities.WeatherGovApi;
using TextTheWeather.Core.Mappers;
using TextTheWeather.Core.Mappers.Interfaces;
using TextTheWeather.Core.Repositories.Interfaces.Weather;

namespace TextTheWeather.Core.Repositories.Weather;

public class WeatherGovApi : IWeatherApi
{
    private IMapper<OpenWeatherApiResponse, WeatherApiResponse> OpenWeatherApiMapper =
        new OpenWeatherApiResponseMapper();

    private HttpClient HttpClient = new();

    public async Task<WeatherApiResponse> GetWeather(string latitude, string longitude)
    {
        HttpClient.DefaultRequestHeaders.Add("User-Agent", "texttheweather.com");
        HttpClient.DefaultRequestHeaders.Add("Accept", "application/geo+json");

        // Sunset-Sunrise API
        dynamic sunInfo = JsonConvert.DeserializeObject<dynamic>(
            await HttpClient.GetStringAsync($"https://api.sunrisesunset.io/json?lat={latitude}&lng={longitude}"));

        // Weather.gov API first request
        dynamic response = JsonConvert.DeserializeObject<dynamic>(
            await HttpClient.GetStringAsync($"https://api.weather.gov/points/{latitude},{longitude}"));

        // Weather.gov API second request
        dynamic weatherInfo = JsonConvert.DeserializeObject<dynamic>(
            await HttpClient.GetStringAsync(response.properties.forecastHourly.ToString()));

        // Get properties.periods 24 entries
        List<WeatherGovHourlyData> hourlyWeather = ((JArray)weatherInfo.properties.periods)
            .Select(p => JsonConvert.DeserializeObject<WeatherGovHourlyData>(p.ToString()))
            // Grab the first 24 entries
            .Take(24)
            .ToList();

        SunData sunData = new()
        {
            Sunrise = TimeOnly.FromDateTime(DateTime.Parse(sunInfo.results.sunrise.ToString())),
            Sunset = TimeOnly.FromDateTime(DateTime.Parse(sunInfo.results.sunset.ToString()))
        };

        return new WeatherApiResponse
        {
            Latitude = latitude,
            Longitude = longitude,
            Timezone = sunInfo.results.timezone,
            TimezoneOffset = sunInfo.results.utc_offset * 60,
            SunData = sunData,
            HourlyWeatherData = new WeatherGovHourlyDataMapper(sunData).Map(hourlyWeather)
        };
    }
}