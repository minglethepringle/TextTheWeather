using System.Text.Json.Serialization;

namespace TextTheWeather.Core.Entities.OpenWeatherApi;

public class OpenWeatherConditionData
{
    [JsonPropertyName("id")]
    public int WeatherConditionId { get; set; }
    [JsonPropertyName("main")]
    public string ConditionCategory { get; set; }
    [JsonPropertyName("description")]
    public string ConditionDescription { get; set; }
    [JsonPropertyName("icon")]
    public string ConditionIcon { get; set; }
}