using System.Text.Json.Serialization;

namespace TextTheWeather.Core.Entities.WeatherGovApi;

public class WeatherGovHourlyData
{
    [JsonPropertyName("startTime")]
    public string StartTime { get; set; }
    [JsonPropertyName("endTime")]
    public string EndTime { get; set; }
    [JsonPropertyName("temperature")]
    public int Temperature { get; set; }
    [JsonPropertyName("probabilityOfPrecipitation")]
    public ProbabilityOfPrecipitation ProbabilityOfPrecipitation { get; set; }
    [JsonPropertyName("windSpeed")]
    public string WindSpeed { get; set; }
    [JsonPropertyName("shortForecast")]
    public string ShortForecast { get; set; }

    public override string ToString()
    {
        return $"Start: {StartTime}, End: {EndTime}, Temp: {Temperature}°F, " +
               $"Precipitation: {ProbabilityOfPrecipitation.Value}%, Wind: {WindSpeed}, " +
               $"Forecast: {ShortForecast}";
    }
}

public class ProbabilityOfPrecipitation
{
    [JsonPropertyName("value")]
    public int Value { get; set; }
}