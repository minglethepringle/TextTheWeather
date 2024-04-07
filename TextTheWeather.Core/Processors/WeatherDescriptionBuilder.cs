using System.Text;
using TextTheWeather.Core.Entities.HourlyData;
using TextTheWeather.Core.Mappers;

namespace TextTheWeather.Core.Processors;

public class WeatherDescriptionBuilder
{
	private StringBuilder StringBuilder = new StringBuilder();
	private DateTime LocalNow;
	private HourlyWeatherCondition GeneralDaytimeWeatherCondition;
	private int MaxTemp;
	private int MinTemp;
	private int AverageDaytimeWindSpeed;
	private bool WillRainDuringDaytime;
	private List<HourlyWeatherData> WeatherForToday;

	public WeatherDescriptionBuilder WithLocalNow(DateTime localNow)
	{
		LocalNow = localNow;
		return this;
	}

	public WeatherDescriptionBuilder WithDaytimeWeatherCondition(HourlyWeatherCondition condition)
	{
		GeneralDaytimeWeatherCondition = condition;
		return this;
	}

	public WeatherDescriptionBuilder WithMaxTemperature(int temperature)
	{
		MaxTemp = temperature;
		return this;
	}

	public WeatherDescriptionBuilder WithMinTemperature(int temperature)
	{
		MinTemp = temperature;
		return this;
	}

	public WeatherDescriptionBuilder WithAverageDaytimeWindSpeed(int windSpeed)
	{
		AverageDaytimeWindSpeed = windSpeed;
		return this;
	}

	public WeatherDescriptionBuilder WithRainDuringDaytime(bool willRain)
	{
		WillRainDuringDaytime = willRain;
		return this;
	}

	public WeatherDescriptionBuilder WithWeatherForToday(List<HourlyWeatherData> weather)
	{
		WeatherForToday = weather;
		return this;
	}

	private string GetTodaysDateFormatted()
	{
		return LocalNow.ToString("MMMM dd");
	}

	private string GetGeneralDaytimeWeatherCondition()
	{
		var weatherCondition = GeneralDaytimeWeatherCondition switch
		{
			HourlyWeatherCondition.Sunny => "sunny",
			HourlyWeatherCondition.PartlyCloudy => "partly cloudy",
			HourlyWeatherCondition.VeryCloudy => "very cloudy",
			HourlyWeatherCondition.BarelyCloudy => "barely cloudy",
			HourlyWeatherCondition.Rainy => "rainy",
			HourlyWeatherCondition.Night => "night skies",
			_ => throw new Exception($"Unknown weather condition! Condition: {GeneralDaytimeWeatherCondition}")
		};

		if (AverageDaytimeWindSpeed > 12)
			weatherCondition += " and windy";
		else if (AverageDaytimeWindSpeed > 6)
			weatherCondition += " and breezy";

		return weatherCondition;
	}

	private string GetWeatherDescriptionForHour(HourlyWeatherData hourWeather)
	{
		var hour = hourWeather.DateTime.ToString("htt");
		var conditionEmoji = new ConditionToEmojiMapper().Map(hourWeather.Condition);
		var actualTemp = hourWeather.Temperature;
		var windSpeed = hourWeather.WindSpeed;

		return $"{hour}: {conditionEmoji} {actualTemp}°F, wind {windSpeed}mph.";
	}

	public string Build()
	{
		StringBuilder.AppendLine($"{GetTodaysDateFormatted()}: Overall, {GetGeneralDaytimeWeatherCondition()}. High {MaxTemp}°F. Low {MinTemp}°F.");

		if (WillRainDuringDaytime)
			StringBuilder.AppendLine("It will rain today - bring an umbrella and wear a rain jacket!");

		StringBuilder.AppendLine();

		foreach (HourlyWeatherData hourWeather in WeatherForToday) StringBuilder.AppendLine(GetWeatherDescriptionForHour(hourWeather));

		StringBuilder.AppendLine();

		StringBuilder.Append("Reply STOP to stop receiving weather updates from TextTheWeather.");

		return StringBuilder.ToString();
	}
}