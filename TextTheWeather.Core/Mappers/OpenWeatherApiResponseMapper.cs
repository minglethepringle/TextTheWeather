using TextTheWeather.Core.Entities.OpenWeatherApi;
using TextTheWeather.Core.Entities.Sun;
using TextTheWeather.Core.Entities.WeatherApi;
using TextTheWeather.Core.Helpers;
using TextTheWeather.Core.Mappers.Interfaces;

namespace TextTheWeather.Core.Mappers;

/**
 * Maps an OpenWeatherApiResponse object to a WeatherApiResponse object
 */
public class OpenWeatherApiResponseMapper : IMapper<OpenWeatherApiResponse, WeatherApiResponse>
{
	public WeatherApiResponse Map(OpenWeatherApiResponse from)
	{
		SunData sunData = new SunData
		{
			Sunrise = TimeOnly.FromDateTime(LocalDateTime.FromUnixTime(from.Current.Sunrise, from.TimezoneOffset)),
			Sunset = TimeOnly.FromDateTime(LocalDateTime.FromUnixTime(from.Current.Sunset, from.TimezoneOffset))
		};

		return new WeatherApiResponse
		{
			Latitude = from.Lat,
			Longitude = from.Lon,
			Timezone = from.Timezone,
			TimezoneOffset = from.TimezoneOffset,
			SunData = sunData,
			HourlyWeatherData = new OpenWeatherHourlyDataMapper(sunData).Map(from.Hourly, from.TimezoneOffset)
		};
	}
}