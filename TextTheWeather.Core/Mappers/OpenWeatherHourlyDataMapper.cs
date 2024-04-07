using TextTheWeather.Core.Entities.HourlyData;
using TextTheWeather.Core.Entities.OpenWeatherApi;
using TextTheWeather.Core.Entities.Sun;
using TextTheWeather.Core.Helpers;
using TextTheWeather.Core.Mappers.Interfaces;

namespace TextTheWeather.Core.Mappers;

public class OpenWeatherHourlyDataMapper(SunData sunData) : IWeatherDataMapper<List<OpenWeatherHourlyData>, List<HourlyWeatherData>>
{
	public List<HourlyWeatherData> Map(List<OpenWeatherHourlyData> from, double timezoneOffset)
	{
		return from.Select(openWeatherHourlyData =>
		{
			DateTime dateTime = LocalDateTime.FromUnixTime(openWeatherHourlyData.DateTime, timezoneOffset);
			return new HourlyWeatherData
			{
				DateTime = dateTime,
				Temperature = (int) openWeatherHourlyData.Temperature,
				FeelsLike = (int) openWeatherHourlyData.FeelsLike,
				HumidityPercentage = (int) openWeatherHourlyData.Humidity,
				WindSpeed = (int) openWeatherHourlyData.WindSpeed,
				CloudsPercentage = (int) openWeatherHourlyData.CloudsPercentage,
				ProbabilityOfPrecipitation = (int) openWeatherHourlyData.ProbabilityOfPrecipitation,
				Condition = IsDay(dateTime) ? GetHourlyWeatherCondition(openWeatherHourlyData, dateTime) : HourlyWeatherCondition.Night,
				ConditionDescription = openWeatherHourlyData.Weather[0].ConditionDescription.ToTitleCase()
			};
		}).ToList();
	}

	private bool IsDay(DateTime dateTime)
	{
		return TimeOnly.FromDateTime(dateTime) >= sunData.Sunrise && TimeOnly.FromDateTime(dateTime) <= sunData.Sunset;
	}

	private HourlyWeatherCondition GetHourlyWeatherCondition(OpenWeatherHourlyData openWeatherHourlyData, DateTime dateTime)
	{
		var condition = openWeatherHourlyData.Weather[0].ConditionCategory;
		var description = openWeatherHourlyData.Weather[0].ConditionDescription;

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