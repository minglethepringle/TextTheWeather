using TextTheWeather.Core.Entities.HourlyData;
using TextTheWeather.Core.Entities.Sun;

namespace TextTheWeather.Core.Entities.WeatherApi;

public class WeatherApiResponse
{
	public decimal Latitude { get; set; }
	public decimal Longitude { get; set; }
	public string Timezone { get; set; }
	public double TimezoneOffset { get; set; }
	public SunData SunData { get; set; }
	public List<HourlyWeatherData> HourlyWeatherData { get; set; }
}