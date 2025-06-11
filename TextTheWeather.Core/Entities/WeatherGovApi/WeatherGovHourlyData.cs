using System.Text.Json.Serialization;

namespace TextTheWeather.Core.Entities.WeatherGovApi;

public class WeatherGovHourlyData
{
    [JsonPropertyName("startTime")]
    public DateTime StartTime { get; set; }
    [JsonPropertyName("endTime")]
    public DateTime EndTime { get; set; }
    [JsonPropertyName("temperature")]
    public int Temperature { get; set; }
    [JsonPropertyName("probabilityOfPrecipitation")]
    public ProbabilityOfPrecipitation ProbabilityOfPrecipitation { get; set; }
    [JsonPropertyName("windSpeed")]
    public string WindSpeed { get; set; }
    [JsonPropertyName("shortForecast")]
    public string ShortForecast { get; set; }
}

public class ProbabilityOfPrecipitation
{
    [JsonPropertyName("value")]
    public int Value { get; set; }
}