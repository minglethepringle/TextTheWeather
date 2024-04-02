using TextTheWeather.Core.Entities.HourlyData;
using TextTheWeather.Core.Mappers.Interfaces;

namespace TextTheWeather.Core.Mappers;

public class ConditionToEmojiMapper : IMapper<HourlyWeatherCondition, string>
{
    public string Map(HourlyWeatherCondition from)
    {
        return from switch
        {
            HourlyWeatherCondition.Sunny => "☀️",
            HourlyWeatherCondition.VeryCloudy => "☁️",
            HourlyWeatherCondition.PartlyCloudy => "🌥️",
            HourlyWeatherCondition.BarelyCloudy => "🌤️",
            HourlyWeatherCondition.Rainy => "🌧️",
            HourlyWeatherCondition.Night => "🌙",
            _ => throw new Exception($"Unknown weather condition! Condition: {from}")
        };
    }
}