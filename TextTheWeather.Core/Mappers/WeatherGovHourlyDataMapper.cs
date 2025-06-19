using TextTheWeather.Core.Entities.HourlyData;
using TextTheWeather.Core.Entities.OpenWeatherApi;
using TextTheWeather.Core.Entities.Sun;
using TextTheWeather.Core.Entities.WeatherGovApi;
using TextTheWeather.Core.Mappers.Interfaces;

namespace TextTheWeather.Core.Mappers;

public class WeatherGovHourlyDataMapper(SunData sunData) : IMapper<List<WeatherGovHourlyData>, List<HourlyWeatherData>>
{
    public List<HourlyWeatherData> Map(List<WeatherGovHourlyData> from)
    {
        return from.Select(data =>
        {
            // Convert 2025-06-19T15:00:00-04:00 to DateTime 2025-06-19 3PM disregarding machine timezone
            DateTime dateTime = DateTimeOffset.Parse(data.StartTime).DateTime;
            return new HourlyWeatherData
            {
                DateTime = dateTime,
                Temperature = data.Temperature,
                WindSpeed = int.Parse(data.WindSpeed.Split(" ")[0]),
                ProbabilityOfPrecipitation = data.ProbabilityOfPrecipitation.Value,
                Condition = IsDay(dateTime) ? GetHourlyWeatherCondition(data) : HourlyWeatherCondition.Night
            };
        }).ToList();
    }

    private bool IsDay(DateTime dateTime)
    {
        return TimeOnly.FromDateTime(dateTime) >= sunData.Sunrise && TimeOnly.FromDateTime(dateTime) <= sunData.Sunset;
    }

    private HourlyWeatherCondition GetHourlyWeatherCondition(WeatherGovHourlyData data)
    {
        string desc = data.ShortForecast;

        if (desc.Contains("Rain") || desc.Contains("Shower"))
            return HourlyWeatherCondition.Rainy;
        if (desc.Equals("Sunny") || desc.Equals("Mostly Sunny") || desc.Contains("Clear"))
            return HourlyWeatherCondition.Sunny;
        if (desc.Equals("Partly Sunny"))
            return HourlyWeatherCondition.BarelyCloudy;
        if (desc.Equals("Partly Cloudy"))
            return HourlyWeatherCondition.PartlyCloudy;
        if (desc.Equals("Mostly Cloudy"))
            return HourlyWeatherCondition.VeryCloudy;

        throw new ArgumentException($"Unknown weather condition: {desc}");
    }
}