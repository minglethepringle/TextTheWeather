using System.Globalization;
using TextTheWeather.Core.Entities.HourlyData;
using TextTheWeather.Core.Entities.OpenWeatherApi;
using TextTheWeather.Core.Entities.Sun;
using TextTheWeather.Core.Mappers.Interfaces;

namespace TextTheWeather.Core.Mappers;

public class OpenWeatherHourlyToHourlyWeatherMapper : IWeatherDataMapper<List<OpenWeatherHourlyData>, List<HourlyWeatherData>>
{
    private SunData _sunData;

    public OpenWeatherHourlyToHourlyWeatherMapper(SunData sunData)
    {
        _sunData = sunData;
    }

    public List<HourlyWeatherData> Map(List<OpenWeatherHourlyData> from, double timezoneOffset)
    {
        List<HourlyWeatherData> hourlyWeatherData = new List<HourlyWeatherData>();
        
        foreach (OpenWeatherHourlyData openWeatherHourlyData in from)
        {
            DateTime dateTime = DateTimeOffset.FromUnixTimeSeconds(openWeatherHourlyData.DateTime).UtcDateTime.AddSeconds(timezoneOffset);
            HourlyWeatherData hourlyWeather = new HourlyWeatherData
            {
                DateTime = dateTime,
                Temperature = (int) openWeatherHourlyData.Temperature,
                FeelsLike = (int) openWeatherHourlyData.FeelsLike,
                HumidityPercentage = (int) openWeatherHourlyData.Humidity,
                WindSpeed = (int) openWeatherHourlyData.WindSpeed,
                CloudsPercentage = (int) openWeatherHourlyData.CloudsPercentage,
                ProbabilityOfPrecipitation = (int) openWeatherHourlyData.ProbabilityOfPrecipitation,
                Condition = GetHourlyWeatherCondition(openWeatherHourlyData, dateTime),
                ConditionDescription = ToTitleCase(openWeatherHourlyData.Weather[0].ConditionDescription)
            };
            
            hourlyWeatherData.Add(hourlyWeather);
        }
        
        return hourlyWeatherData;
    }

    private string ToTitleCase(string input)
    {
        return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(input.ToLower()); 
    }

    private HourlyWeatherCondition GetHourlyWeatherCondition(OpenWeatherHourlyData openWeatherHourlyData, DateTime dateTime)
    {
        // If time is outside of sunrise and sunset, return Night
        if (TimeOnly.FromDateTime(dateTime) < _sunData.Sunrise || TimeOnly.FromDateTime(dateTime) > _sunData.Sunset)
            return HourlyWeatherCondition.Night;
        
        string condition = openWeatherHourlyData.Weather[0].ConditionCategory;
        string description = openWeatherHourlyData.Weather[0].ConditionDescription;
        
        return condition switch
        {
            "Clear" => HourlyWeatherCondition.Sunny,
            "Clouds" => GetCloudyCondition(description, openWeatherHourlyData.CloudsPercentage),
            "Rain" => HourlyWeatherCondition.Rainy,
            _ => throw new Exception($"Unknown weather condition! Condition: {condition}")
        };
    }
    
    private HourlyWeatherCondition GetCloudyCondition(string description, decimal cloudPercentage)
    {
        if (description.Contains("overcast"))
            return HourlyWeatherCondition.VeryCloudy;

        if (cloudPercentage > 50)
            return HourlyWeatherCondition.PartlyCloudy;

        return HourlyWeatherCondition.BarelyCloudy;
    }
}