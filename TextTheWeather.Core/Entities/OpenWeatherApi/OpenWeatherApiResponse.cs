using System.Text.Json.Serialization;

namespace TextTheWeather.Core.Entities.OpenWeatherApi;

public class OpenWeatherApiResponse
{
    [JsonPropertyName("lat")]
    public string Lat { get; set; }
    [JsonPropertyName("lon")]
    public string Lon { get; set; }
    [JsonPropertyName("timezone")]
    public string Timezone { get; set; }
    [JsonPropertyName("timezone_offset")]
    public double TimezoneOffset { get; set; }
    [JsonPropertyName("current")]
    public OpenWeatherCurrentData Current { get; set; }
    [JsonPropertyName("hourly")]
    public List<OpenWeatherHourlyData> Hourly { get; set; }
}