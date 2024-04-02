namespace TextTheWeather.Core.Entities.HourlyData;

public class HourlyWeatherData
{
    public DateTime DateTime { get; set; }
    public int Temperature { get; set; }
    public int FeelsLike { get; set; }
    public int HumidityPercentage { get; set; }
    public int WindSpeed { get; set; }
    public int CloudsPercentage { get; set; }
    public int ProbabilityOfPrecipitation { get; set; }
    public HourlyWeatherCondition Condition { get; set; }
    public string ConditionDescription { get; set; }
}