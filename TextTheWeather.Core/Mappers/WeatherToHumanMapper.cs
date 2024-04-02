using System.Text;
using TextTheWeather.Core.Entities.HourlyData;
using TextTheWeather.Core.Entities.OpenWeatherApi;
using TextTheWeather.Core.Entities.Sun;
using TextTheWeather.Core.Entities.User;
using TextTheWeather.Core.Mappers.Interfaces;

namespace TextTheWeather.Core.Mappers;

public class WeatherToHumanMapper(User user) : IMapper<OpenWeatherApiResponse, string>
{
    private User _user = user;
    private SunData _sunData;
    private List<HourlyWeatherData> _todaysWeather;

    public string Map(OpenWeatherApiResponse weather)
    {
        StringBuilder sb = new StringBuilder();

        _sunData = ParseSunriseAndSunset(weather);
        _todaysWeather = GetWeatherForToday(new OpenWeatherHourlyToHourlyWeatherMapper(_sunData).Map(weather.Hourly, weather.TimezoneOffset));
        
        string generalDaytimeWeatherCondition = GetGeneralDaytimeWeatherCondition();
        int maxTemp = GetMaxTemp();
        int minTemp = GetMinTemp();
        bool willRain = WillRainDuringDaytime();
        
        // Append to string builder
        string today = DateTime.Now.ToString("MMMM dd");
        sb.Append($"{today}: Overall, {generalDaytimeWeatherCondition}. High {maxTemp}°F. Low {minTemp}°F.\n");
        // If will rain, append to string builder
        if (willRain)
            sb.Append("It will rain today - bring an umbrella and wear a rain jacket!\n");
        
        sb.Append("\n");
        
        foreach (HourlyWeatherData data in _todaysWeather)
        {
            string hour = data.DateTime.ToString("htt");
            string conditionEmoji = new ConditionToEmojiMapper().Map(data.Condition);
            int actualTemp = data.Temperature;
            int windSpeed = data.WindSpeed;
            
            sb.Append($"{hour}: {conditionEmoji} {actualTemp}°F, wind {windSpeed}mph.\n");
        }

        sb.Append("\nIf you would like to stop receiving these emails, please contact weather@texttheweather.com.\n");
        
        return sb.ToString();
    }

    private SunData ParseSunriseAndSunset(OpenWeatherApiResponse weather)
    {
        DateTime sunrise = DateTimeOffset.FromUnixTimeSeconds(weather.Current.Sunrise).UtcDateTime.AddSeconds(weather.TimezoneOffset);
        DateTime sunset = DateTimeOffset.FromUnixTimeSeconds(weather.Current.Sunset).UtcDateTime.AddSeconds(weather.TimezoneOffset);

        return new SunData()
        {
            Sunrise = TimeOnly.FromDateTime(sunrise),
            Sunset = TimeOnly.FromDateTime(sunset)
        };
    }

    /**
     * Filters the hourly weather data to only include today's weather, meaning from user's WeatherFrom to WeatherTo
     */
    private List<HourlyWeatherData> GetWeatherForToday(List<HourlyWeatherData> data)
    {
        DateTime from = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, _user.WeatherFrom.Hour, _user.WeatherFrom.Minute, 0);
        DateTime to = new DateTime(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day, _user.WeatherTo.Hour, _user.WeatherTo.Minute, 0);
         
        return data
            .FindAll(x => x.DateTime >= from && x.DateTime <= to);
    }
    
    /**
     * Filters the hourly weather data to only include daytime weather, meaning between sunrise and sunset.
     */
    private List<HourlyWeatherData> GetDaytimeWeather(List<HourlyWeatherData> data)
    {
        return data.FindAll(x => TimeOnly.FromDateTime(x.DateTime) > _sunData.Sunrise && TimeOnly.FromDateTime(x.DateTime) < _sunData.Sunset);
    }

    /**
     * Get daytime weather data, meaning between sunrise and sunset.
     */
    private string GetGeneralDaytimeWeatherCondition()
    {
        List<HourlyWeatherData> daytimeData = GetDaytimeWeather(_todaysWeather);

        // If no daytime data, it's nighttime
        if (daytimeData.Count == 0)
            return "night skies";
        
        // Get most common condition category
        HourlyWeatherCondition condition = daytimeData.GroupBy(x => x.Condition)
            .OrderByDescending(x => x.Count())
            .First()
            .Key;

        switch (condition)
        {
            case HourlyWeatherCondition.Sunny:
                return "sunny";
            case HourlyWeatherCondition.VeryCloudy:
                return "very cloudy";
            case HourlyWeatherCondition.PartlyCloudy:
                return "partly cloudy";
            case HourlyWeatherCondition.BarelyCloudy:
                return "barely cloudy";
            case HourlyWeatherCondition.Rainy:
                return "rainy";
            default:
                throw new Exception($"Unknown weather condition! Condition: {condition}");
        }
    }
    
    private bool WillRainDuringDaytime()
    {
        List<HourlyWeatherData> daytimeData = GetDaytimeWeather(_todaysWeather);
        
        return daytimeData.Exists(x => x.Condition == HourlyWeatherCondition.Rainy && x.ProbabilityOfPrecipitation >= 50);
    }
    
    private int GetMaxTemp()
    {
        return _todaysWeather.Max(x => x.Temperature);
    }
    
    private int GetMinTemp()
    {
        return _todaysWeather.Min(x => x.Temperature);
    }
}