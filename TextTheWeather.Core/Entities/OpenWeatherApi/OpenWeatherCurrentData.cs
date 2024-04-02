using System.Text.Json.Serialization;

namespace TextTheWeather.Core.Entities.OpenWeatherApi;

public class OpenWeatherCurrentData
{
    [JsonPropertyName("sunrise")]
    public long Sunrise { get; set; }
    [JsonPropertyName("sunset")]
    public long Sunset { get; set; }
}