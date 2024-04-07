using TextTheWeather.Core.Entities.HourlyData;
using TextTheWeather.Core.Entities.User;
using TextTheWeather.Core.Entities.WeatherApi;
using TextTheWeather.Core.Helpers;
using TextTheWeather.Core.Processors.Interfaces;

namespace TextTheWeather.Core.Processors;

public class WeatherDataProcessor(WeatherApiResponse weatherApiResponse, User user) : IWeatherDataProcessor
{
	private List<HourlyWeatherData> WeatherForToday;
	private List<HourlyWeatherData> DaytimeWeather;

	public string GetWeatherDescription()
	{
		WeatherForToday = GetWeatherForToday(weatherApiResponse.HourlyWeatherData);
		DaytimeWeather = GetDaytimeWeather(WeatherForToday);

		return new WeatherDescriptionBuilder()
			.WithLocalNow(LocalDateTime.LocalNow(weatherApiResponse.TimezoneOffset))
			.WithDaytimeWeatherCondition(GetGeneralDaytimeWeatherCondition())
			.WithMaxTemperature(GetMaxTemp())
			.WithMinTemperature(GetMinTemp())
			.WithAverageDaytimeWindSpeed(GetAverageDaytimeWindSpeed())
			.WithRainDuringDaytime(WillRainDuringDaytime())
			.WithWeatherForToday(WeatherForToday)
			.Build();
	}

	/**
	 * Filters the hourly weather data to only include today's weather, meaning from user's WeatherFrom to WeatherTo for today
	 */
	private List<HourlyWeatherData> GetWeatherForToday(List<HourlyWeatherData> data)
	{
		DateTime localNow = LocalDateTime.LocalNow(weatherApiResponse.TimezoneOffset);
		DateTime from = new DateTime(localNow.Year, localNow.Month, localNow.Day, user.WeatherFrom.Hour, user.WeatherFrom.Minute, 0);
		DateTime to = new DateTime(localNow.Year, localNow.Month, localNow.Day, user.WeatherTo.Hour, user.WeatherTo.Minute, 0);

		return data
			.FindAll(x => x.DateTime >= from && x.DateTime <= to);
	}

	/**
	 * Filters the hourly weather data to only include daytime weather, meaning between sunrise and sunset.
	 */
	private List<HourlyWeatherData> GetDaytimeWeather(List<HourlyWeatherData> data)
	{
		if (data.Count == 0)
			return [];

		return data.FindAll(x => TimeOnly.FromDateTime(x.DateTime) > weatherApiResponse.SunData.Sunrise && TimeOnly.FromDateTime(x.DateTime) < weatherApiResponse.SunData.Sunset);
	}

	private HourlyWeatherCondition GetGeneralDaytimeWeatherCondition()
	{
		// If no daytime data, it's nighttime
		if (DaytimeWeather.Count == 0)
			return HourlyWeatherCondition.Night;

		// Get most common condition category
		return DaytimeWeather.GroupBy(x => x.Condition)
			.OrderByDescending(x => x.Count())
			.First()
			.Key;
	}

	private int GetMaxTemp()
	{
		if (WeatherForToday.Count == 0)
			return 0;

		return WeatherForToday.Max(x => x.Temperature);
	}

	private int GetMinTemp()
	{
		if (WeatherForToday.Count == 0)
			return 0;

		return WeatherForToday.Min(x => x.Temperature);
	}

	private int GetAverageDaytimeWindSpeed()
	{
		if (DaytimeWeather.Count == 0)
			return 0;

		return (int) DaytimeWeather.Average(x => x.WindSpeed);
	}

	private bool WillRainDuringDaytime()
	{
		if (DaytimeWeather.Count == 0)
			return false;

		return DaytimeWeather.Any(x => x.Condition == HourlyWeatherCondition.Rainy && x.ProbabilityOfPrecipitation >= 50);
	}
}