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

        JsonSerializerSettings settings = new()
        {
            DateParseHandling = DateParseHandling.None
        };

        // Sunset-Sunrise API
        dynamic sunInfo = JsonConvert.DeserializeObject<dynamic>(
            await HttpClient.GetStringAsync($"https://api.sunrisesunset.io/json?lat={latitude}&lng={longitude}"), settings);

        // Weather.gov API first request
        dynamic response = JsonConvert.DeserializeObject<dynamic>(
            await HttpClient.GetStringAsync($"https://api.weather.gov/points/{latitude},{longitude}"), settings);

        // Weather.gov API second request
        string forecastHourlyUrl = response.properties.forecastHourly.ToString();
        Console.WriteLine($"Forecast Hourly URL: {forecastHourlyUrl}");
        dynamic weatherInfo = JsonConvert.DeserializeObject<dynamic>(
            await HttpClient.GetStringAsync(forecastHourlyUrl), settings);

        // Get properties.periods 2 4 entries
        List<WeatherGovHourlyData> hourlyWeather = ((JArray)weatherInfo.properties.periods)
            .Select(p => JsonConvert.DeserializeObject<WeatherGovHourlyData>(p.ToString(), settings))
            // Grab the first 24 entries
            .Take(24)
            .ToList();

        SunData sunData = new()
        {
            Sunrise = TimeOnly.FromDateTime(DateTime.Parse(sunInfo.results.sunrise.ToString())),
            Sunset = TimeOnly.FromDateTime(DateTime.Parse(sunInfo.results.sunset.ToString()))
        };

        Console.WriteLine($"Sunrise: {sunData.Sunrise}, Sunset: {sunData.Sunset}");

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