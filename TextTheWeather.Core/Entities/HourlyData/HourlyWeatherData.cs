namespace TextTheWeather.Core.Entities.HourlyData;

public class HourlyWeatherData
{
    public DateTime DateTime { get; set; }
    public int Temperature { get; set; }
    public int WindSpeed { get; set; }
    public int ProbabilityOfPrecipitation { get; set; }
    public HourlyWeatherCondition Condition { get; set; }

    public override string ToString()
    {
        return $"HourlyWeatherData: {{ DateTime: {DateTime}, Temperature: {Temperature}°C, WindSpeed: {WindSpeed} km/h, ProbabilityOfPrecipitation: {ProbabilityOfPrecipitation}%, Condition: {Condition} }}";
    }
}