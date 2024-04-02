using System.Text.Json.Serialization;

namespace TextTheWeather.Core.Entities.OpenWeatherApi;

public class OpenWeatherHourlyData
{
    [JsonPropertyName("dt")]
    public long DateTime { get; set; }
    [JsonPropertyName("temp")]
    public decimal Temperature { get; set; }
    [JsonPropertyName("feels_like")]
    public decimal FeelsLike { get; set; }
    [JsonPropertyName("pressure")]
    public decimal Pressure { get; set; }
    [JsonPropertyName("humidity")]
    public decimal Humidity { get; set; }
    // [JsonPropertyName("dew_point")]
    // public string DewPoint { get; set; }
    // [JsonPropertyName("uvi")]
    // public string UvIndex { get; set; }
    [JsonPropertyName("clouds")]
    public decimal CloudsPercentage { get; set; }
    // [JsonPropertyName("visibility")]
    // public string Visibility { get; set; }
    [JsonPropertyName("wind_speed")]
    public decimal WindSpeed { get; set; }
    [JsonPropertyName("wind_deg")]
    public decimal WindDirection { get; set; }
    [JsonPropertyName("pop")]
    public decimal ProbabilityOfPrecipitation { get; set; }
    [JsonPropertyName("weather")]
    public List<OpenWeatherConditionData> Weather { get; set; }
    
    // [JsonPropertyName("rain")]
    // public OpenWeatherRainData Rain { get; set; }
    // [JsonPropertyName("snow")]
    // public OpenWeatherSnowData Snow { get; set; }
    // [JsonPropertyName("uvi")]
    // public string Uvi { get; set; }
}